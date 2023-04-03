using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInterfaces.Application.Response
{
    /// <summary>
    /// tracker para una respuesta
    /// </summary>
    public interface ITrackerResponse
    {
        /// <summary>
        /// mensaje de estado de la respuesta
        /// </summary>
        string Message { get; }
        /// <summary>
        /// estatus de la respuesta
        /// </summary>
        short Status { get; }
        /// <summary>
        /// estado de exito de la solicitud
        /// </summary>
        public bool Succeeded { get; }
        /// <summary>
        /// listado de errores sucedidos en la solicitud
        /// </summary>
        public string[] Errors { get; }
    }
}
