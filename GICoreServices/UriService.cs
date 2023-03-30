using GICoreInterfaces.Aplication.Models;
using GICoreInterfaces.Aplication.Services.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreServices
{
    /// <summary>
    /// clase para el servicio que se conectara al httpcontext de una peticion
    /// </summary>
    public class UriService : IUriService
    {
        /// <summary>
        /// uri base
        /// </summary>
        public string baseUri { get; }
        /// <summary>
        /// uri de la solicitud
        /// </summary>
        public string selfUri { get; private set; }
        /// <summary>
        /// uri de la solicitud + lo contenido en el query string
        /// </summary>
        public string selfUriWithQuery { get; }
        /// <summary>
        /// contexto de la solicitud
        /// </summary>
        public HttpContext httpContext { get; set; }
        /// <summary>
        /// solicitud
        /// </summary>
        public HttpRequest Request { get; }
        /// <summary>
        /// inicializa el servicio
        /// </summary>
        /// <param name="context">httpcontext de la solicitud</param>
        public UriService(HttpContext context, HttpRequest request)
        {
            httpContext = context;
            Request = request;
            baseUri = string.Concat(httpContext.Request.Scheme, "://", httpContext.Request.Host.ToUriComponent());
            selfUri = new Uri(string.Concat(baseUri, httpContext.Request.Path.Value)).ToString();
            selfUriWithQuery = new Uri(string.Concat(baseUri, httpContext.Request.Path.Value, httpContext.Request.QueryString)).ToString();
        }
        /// <summary>
        /// devuelve una uri con los valores de paginacion en el query string
        /// </summary>
        /// <param name="filter">valores de paginacion</param>
        /// <returns>nueva uri</returns>
        public Uri GetPaginationPageUri(IPaginationFilter filter)
        {
            var modifiedUri = QueryHelpers.AddQueryString(selfUri, "PageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "PageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }
        /// <summary>
        /// devuelve true si el media type del request es hateoas
        /// </summary>
        /// <returns>true o false</returns>
        public bool isHateoasRequest()
        {
            StringValues acceptHeader;
            bool getResult = Request.Headers.TryGetValue("Accept", out acceptHeader);

            MediaTypeHeaderValue AcceptmediaType = MediaTypeHeaderValue.Parse(acceptHeader.ToString());

            if (getResult && !AcceptmediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase))
                return false;

            return true;
        }
    }
}
