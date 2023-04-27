using IdeaCoreHateoas.Extensions;
using IdeaCoreInterfaces.Common;
using IdeaCoreInterfaces.Hateoas;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreHateoas
{
    public class HateoasListWrapperService : IHateoasListWrapperService
    {
        public IHttpContextAccessor HttpContextAccessor { get; }

        public HateoasListWrapperService(IHttpContextAccessor accesor)
        {
            HttpContextAccessor = accesor;
        }

        public IHateoasListWrapper<Model> Wrap<Model>(IEnumerable<Model> embed) where Model : class
        {
            return new HateoasListWrapper<Model>(embed).AddSelfLink(HttpContextAccessor.HttpContext);
        }

        public IHateoasListWrapper<Model> AddSelfLink<Model>(IHateoasListWrapper<Model> wrapper) where Model : class
        {
            return wrapper.AddSelfLink(HttpContextAccessor.HttpContext);
        }
        public IHateoasListWrapper<Model> AddPagination<Model>(IHateoasListWrapper<Model> wrapper, int totalRecords, IPaginationFilter paginationFilter = null) where Model : class
        {
            return wrapper.AddPagination(HttpContextAccessor.HttpContext, totalRecords, paginationFilter);
        }

        public IHateoasListWrapper<Model> AddLink<Model>(IHateoasListWrapper<Model> wrapper, ILink link) where Model : class
        {
            return wrapper.AddLink(link);
        }

        public bool isHateoasRequest()
        {
            StringValues acceptHeader;
            bool getResult = HttpContextAccessor.HttpContext.Request.Headers.TryGetValue("Accept", out acceptHeader);

            MediaTypeHeaderValue AcceptmediaType = MediaTypeHeaderValue.Parse(acceptHeader.ToString());

            if (getResult && !AcceptmediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase))
                return false;

            return true;
        }
    }

}
