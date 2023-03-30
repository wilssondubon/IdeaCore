using Azure.Core;
using GICoreModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreUtils.API
{
    /// <summary>
    /// controlador basico con funciones generales
    /// </summary>
    /// <typeparam name="Model"></typeparam>
    public abstract class Controller<Model> : ControllerBase where Model : class
    {
        /// <summary>
        /// servicio para escribir logs
        /// </summary>
        protected readonly ILogger _logger;
        /// <summary>
        /// inicializa el controlador
        /// </summary>
        /// <param name="logger"></param>
        public Controller(ILogger logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// obtiene la ip del cliente que consume el api
        /// </summary>
        /// <returns>Ip del cliente</returns>
        protected virtual string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        /// <summary>
        /// retorna un mensaje de error 500 basado en una excepcion
        /// </summary>
        /// <param name="ex">excepcion</param>
        /// <returns>error 500</returns>
        protected virtual ObjectResult InternalServerError(Exception ex)
        {
            _logger.LogError("{0} {1} Error: {2}. {3}", DateTime.Now, ipAddress(), ex.Message, ex.InnerException != null ? ex.InnerException.Message : "");
            return StatusCode(500, new TrackerResponse(-1, ex.Message, ex.InnerException != null ? new string[] { ex.InnerException.Message } : new string[] { ex.Message }, false));
        }
        /// <summary>
        /// funcion para devolver el nombre de la accion del controlador que funciona como una forma de obtener un unico elemento por su llave principal
        /// </summary>
        /// <returns>nombre la accion</returns>
        protected abstract string GetByIdActionName();
    }
}
