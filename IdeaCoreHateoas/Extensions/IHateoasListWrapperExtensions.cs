using IdeaCoreInterfaces.Common;
using IdeaCoreInterfaces.Hateoas;
using IdeaCoreModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreHateoas.Extensions
{
    public static class IHateoasListWrapperExtensions
    {
        private static string getSelfUri(HttpContext context)
        {
            var baseUri = string.Concat(context.Request.Scheme, "://", context.Request.Host.ToUriComponent());
            return new Uri(string.Concat(baseUri, context.Request.Path.Value)).ToString();
        }

        private static string getSelfUriWithQuery(HttpContext context) {
            var baseUri = string.Concat(context.Request.Scheme, "://", context.Request.Host.ToUriComponent());
            return new Uri(string.Concat(baseUri, context.Request.Path.Value, context.Request.QueryString)).ToString();
        }

        public static IHateoasListWrapper<Model> AddSelfLink<Model>(this IHateoasListWrapper<Model> wrapper, HttpContext context) where Model : class
        {
            if (wrapper.links == null)
                wrapper.links = new List<ILink>();

            string selfUriWithQuery = getSelfUriWithQuery(context);
            wrapper.links.Add(new Link(new Uri(selfUriWithQuery), "self", "GET"));

            return wrapper;
        }

        public static IHateoasListWrapper<Model> AddLink<Model>(this IHateoasListWrapper<Model> wrapper, ILink link) where Model : class
        {
            if (wrapper.links == null)
                wrapper.links = new List<ILink>();

            wrapper.links.Add(link);

            return wrapper;
        }

        public static IHateoasListWrapper<Model> AddPagination<Model>(this IHateoasListWrapper<Model> wrapper, HttpContext context, int totalRecords, IPaginationFilter paginationFilter = null) where Model : class
        {

            if (paginationFilter != null && totalRecords > paginationFilter.PageSize)
            {
                int roundedTotalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / paginationFilter.PageSize));

                wrapper.paging = new PagedResponse
                {
                    PageNumber = paginationFilter.PageNumber,
                    PageSize = paginationFilter.PageSize,
                    TotalPages = roundedTotalPages,
                    TotalRecords = totalRecords,
                };

                context.Response.Headers.Add("X-Paging-PageNo", wrapper.paging.PageNumber.ToString());
                context.Response.Headers.Add("X-Paging-PageSize", wrapper.paging.PageSize.ToString());
                context.Response.Headers.Add("X-Paging-PageCount", wrapper.paging.TotalPages.ToString());
                context.Response.Headers.Add("X-Paging-TotalRecordCount", wrapper.paging.TotalRecords.ToString());

                if (wrapper.links == null)
                    wrapper.links = new List<ILink>();

                Func<IPaginationFilter, Uri> GetPaginationPageUri = (filter) =>
                {
                    var modifiedUri = QueryHelpers.AddQueryString(getSelfUri(context), "PageNumber", filter.PageNumber.ToString());
                    modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "PageSize", filter.PageSize.ToString());
                    return new Uri(modifiedUri);
                };

                wrapper.links.Add(
                    new Link(GetPaginationPageUri(new FilterQueryParams(1, wrapper.paging.PageSize)),
                    "first",
                    "GET")
                );

                if (wrapper.paging.PageNumber - 1 >= 1 && wrapper.paging.PageNumber <= wrapper.paging.TotalPages)
                {
                    wrapper.links.Add(
                        new Link(GetPaginationPageUri(new FilterQueryParams(wrapper.paging.PageNumber - 1, wrapper.paging.PageSize)),
                        "prev",
                        "GET")
                    );
                }

                if (wrapper.paging.PageNumber >= 1 && wrapper.paging.PageNumber < wrapper.paging.TotalPages)
                {
                    wrapper.links.Add(
                        new Link(GetPaginationPageUri(new FilterQueryParams(wrapper.paging.PageNumber + 1, wrapper.paging.PageSize)),
                        "next",
                        "GET")
                    );
                }

                wrapper.links.Add(
                    new Link(GetPaginationPageUri(new FilterQueryParams(wrapper.paging.TotalPages, wrapper.paging.PageSize)),
                    "last",
                    "GET")
                );
            }
            else
            {
                wrapper.paging = null;
            }

            return wrapper;
        }
    }
}
