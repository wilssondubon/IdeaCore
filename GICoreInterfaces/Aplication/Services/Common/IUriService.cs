using IdeaCoreInterfaces.Aplication.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInterfaces.Aplication.Services.Common
{
    /// <summary>
    /// interfaz para el servicio que se conectara al httpcontext de una peticion
    /// </summary>
    public interface IUriService
    {
        /// <summary>
        /// uri base
        /// </summary>
        string baseUri { get; }
        /// <summary>
        /// uri de la solicitud
        /// </summary>
        string selfUri { get; }
        /// <summary>
        /// uri de la solicitud + lo contenido en el query string
        /// </summary>
        string selfUriWithQuery { get; }
        /// <summary>
        /// contexto de la solicitud
        /// </summary>
        HttpContext httpContext { get; set; }
        /// <summary>
        /// solicitud
        /// </summary>
        HttpRequest Request { get; }
        /// <summary>
        /// devuelve una uri con los valores de paginacion en el query string
        /// </summary>
        /// <param name="filter">valores de paginacion</param>
        /// <returns>nueva uri</returns>
        Uri GetPaginationPageUri(IPaginationFilter filter);
        /// <summary>
        /// devuelve true si el media type del request es hateoas
        /// </summary>
        /// <returns>true o false</returns>
        bool isHateoasRequest();
    }
}
