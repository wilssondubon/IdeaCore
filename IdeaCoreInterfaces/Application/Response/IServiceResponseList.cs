using IdeaCoreInterfaces.Common;
using IdeaCoreInterfaces.Hateoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInterfaces.Application.Response
{
    /// <summary>
    /// interfaz de respuesta de un servicio que devuelve una lista
    /// </summary>
    /// <typeparam name="Model">modelo en el que se basa la respuesta</typeparam>
    public interface IServiceResponseList<Model> : IHateoasListWrapper<Model>, ITrackerResponse where Model : class
    {
        /// <summary>
        /// lista de elementos del tipo del modelo en el que se basa la respuesta
        /// </summary>
        IEnumerable<Model>? data { get; set; }
        /// <summary>
        /// respuesta de paginacion
        /// </summary>
        IPagedResponse? paging { get; set; }
        /// <summary>
        /// links de la respuesta (hateos)
        /// </summary>
        IList<ILink>? links { get; set; }
    }
}
