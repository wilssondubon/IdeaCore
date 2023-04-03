using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInterfaces.Application.Response
{
    /// <summary>
    /// interfaz de respuesta de un servicio basada en un modelo
    /// </summary>
    /// <typeparam name="Model"></typeparam>
    public interface IServiceResponse<Model> : ITrackerResponse where Model : class
    {
        /// <summary>
        /// modelo en el que se basa la respuesta
        /// </summary>
        Model data { get; }
    }
}
