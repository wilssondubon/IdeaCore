using GICoreInterfaces.Aplication.Models.Hateoas;
using GICoreInterfaces.Aplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreInterfaces.Aplication.Services.Common.Response
{
    /// <summary>
    /// interfaz de respuesta de un servicio que devuelve una lista
    /// </summary>
    /// <typeparam name="Model">modelo en el que se basa la respuesta</typeparam>
    public interface IServiceResponseList<Model> : ITrackerResponse where Model : class
    {
        /// <summary>
        /// lista de elementos del tipo del modelo en el que se basa la respuesta
        /// </summary>
        IEnumerable<Model> data { get; }
        /// <summary>
        /// respuesta de paginacion
        /// </summary>
        IPagedResponse? paging { get; set; }
        /// <summary>
        /// links de la respuesta (hateos)
        /// </summary>
        IList<ILink>? links { get; set; }
        /// <summary>
        /// funcion para crear una respuesta paginada
        /// </summary>
        /// <param name="paginationFilter">valores de paginacion</param>
        /// <returns>respuesta paginada</returns>
        IServiceResponseList<Model> SetPagination(IPaginationFilter paginationFilter = null);
    }
}
