using GICoreInterfaces.Aplication.Services.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreUtils.API
{
    /// <summary>
    /// Controlador con funciones comunes de un Crud
    /// </summary>
    /// <typeparam name="Model">tipo del modelo en el que se basa el controlador</typeparam>
    public partial class CRUDController<Model, Entity> : Controller<Model> where Model : class where Entity : class
    {
        /// <summary>
        /// servicio con operaciones de tipo crud que se utilizara en el controlador 
        /// </summary>
        protected readonly ICRUDService<Model, Entity> _service;
        /// <summary>
        /// inicializa un controller con funciones comunes de un crud con llamadas a un servicio de tipo CRUD
        /// </summary>
        /// <param name="logger">logger para escribir logs</param>
        /// <param name="service">servicio que se utilizara en el controlador</param>
        public CRUDController(ILogger logger, ICRUDService<Model, Entity> service) : base(logger)
        {
            _service = service;
            rController = new RController<Model, Entity>(logger, service);
            cudController = new CUDController<Model, Entity>(logger, service);
        }
        /// <summary>
        /// funcion para devolver el nombre de la accion del controlador que funciona como una forma de obtener un unico elemento por su llave principal
        /// </summary>
        /// <returns>nombre la accion</returns>
        /// <exception cref="NotImplementedException">no Implementada</exception>
        protected override string GetByIdActionName()
        {
            throw new NotImplementedException();
        }
    }
}
