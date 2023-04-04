using IdeaCoreHateoas.Contracts;
using IdeaCoreInterfaces.Hateoas;
using IdeaCoreModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreHateoas.Extensions
{
    public static class IHateoasListWrapperExtensions
    {
        public static IHateoasListWrapper AddSelfLink(this IHateoasListWrapper wrapper, HttpContext context)
        {
            if (wrapper.links == null)
                wrapper.links = new List<ILink>();

            string baseUri = string.Concat(context.Request.Scheme, "://", context.Request.Host.ToUriComponent());
            string selfUriWithQuery = new Uri(string.Concat(baseUri, context.Request.Path.Value, context.Request.QueryString)).ToString();
            wrapper.links.Add(new Link(new Uri(selfUriWithQuery), "self", "GET"));

            return wrapper;
        }

        public static IHateoasListWrapper AddLink(this IHateoasListWrapper wrapper, Link link)
        {
            if (wrapper.links == null)
                wrapper.links = new List<ILink>();

            wrapper.links.Add(link);

            return wrapper;
        }
    }
}
