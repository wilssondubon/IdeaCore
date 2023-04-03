using IdeaCoreInterfaces.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreModels
{
    /// <summary>
    /// tracker para una respuesta
    /// </summary>
    public class TrackerResponse : ITrackerResponse
    {
        /// <summary>
        /// estatus de la respuesta
        /// </summary>
        public short Status { get; }
        /// <summary>
        /// estado de exito de la solicitud
        /// </summary>
        public bool Succeeded { get; }
        /// <summary>
        /// mensaje de estado de la respuesta
        /// </summary>
        public string Message { get; }
        /// <summary>
        /// listado de errores sucedidos en la solicitud
        /// </summary>
        public string[] Errors { get; }
        /// <summary>
        /// inicializa un nuevo tracker
        /// </summary>
        /// <param name="status">estatus de la respuesta</param>
        /// <param name="message">mensaje de estado de la respuesta</param>
        public TrackerResponse(short status, string message)
        {
            Status = status;
            Message = message;
        }
        /// <summary>
        /// inicializa un nuevo tracker
        /// </summary>
        /// <param name="status">status de la respuesta</param>
        /// <param name="message">mensaje de estado de la respuesta</param>
        /// <param name="errors">lista de errores sucedidos</param>
        /// <param name="succeded">estado de exito de la solicitud</param>
        public TrackerResponse(short status, string message, string[] errors, bool succeded)
        {
            Status = status;
            Message = message;
            Errors = errors;
            Succeeded = succeded;
        }
        /// <summary>
        /// inicializa un nuevo tracker con estatus 1
        /// mensaje OK
        /// exito de la solicitud true
        /// errors null
        /// </summary>
        public TrackerResponse()
        {
            Status = 200;
            Message = "OK";
            Succeeded = true;
            Errors = null;
        }
        public TrackerResponse(short status)
        {
            Status = status;
            Message = "OK";
            Succeeded = true;
            Errors = null;
        }
    }
}
