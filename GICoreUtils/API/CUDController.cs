using GICoreInterfaces.Aplication.Services.Common;
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
    /// controlador basico con funciones de Create, Update y Delete
    /// </summary>
    /// <typeparam name="Model">tipo del modelo en el que se basara el controlador</typeparam>
    public class CUDController<Model, Entity> : Controller<Model> where Model : class where Entity : class
    {
        /// <summary>
        /// servicio generico de tipo CUD
        /// </summary>
        protected readonly ICUDService<Model, Entity> _service;
        /// <summary>
        /// inicializa el controlador
        /// </summary>
        /// <param name="logger">servicio para escribir logs</param>
        /// <param name="service">servicio generico de tipo CRUD</param>
        public CUDController(ILogger logger, ICUDService<Model, Entity> service) : base(logger)
        {
            _service = service;
        }
        /// <summary>
        /// funcion que ejecuta una insercion
        /// </summary>
        /// <param name="data">modelo para guardar</param>
        /// <returns>resultado de una accion</returns>
        public virtual async Task<IActionResult> SaveNew(Model data)
        {
            try
            {
                var payload = await _service.Crear(data);
                if (payload.Status == 201)
                {
                    return CreatedAtAction(GetByIdActionName(), _service.ModelKeyParameters(payload.data), payload);
                }

                return StatusCode(payload.Status, payload);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// funcion que ejecuta una insercion basada en un diccionario de nombre de propiedad y valor de la propiedad
        /// </summary>
        /// <param name="data">diccionario de nombre de propiedad y valor de la propiedad</param>
        /// <returns>resultado de una accion</returns>
        public virtual async Task<IActionResult> SaveNew(IDictionary<string, object> data)
        {
            try
            {
                var objeto = _service.MapModelFromDictionary(data);

                var payload = await _service.Crear(objeto);

                if (payload.Status == 201)
                {
                    return CreatedAtAction(GetByIdActionName(), _service.ModelKeyParameters(payload.data), payload);
                }

                return StatusCode(payload.Status, payload);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// funcion que ejecuta una actualizacion
        /// </summary>
        /// <param name="data">modelo para actualizar</param>
        /// <returns>resultado de una accion</returns>
        public virtual async Task<IActionResult> Update(Model data)
        {
            try
            {
                var payload = await _service.Actualizar(data);

                if (payload.Status == 200)
                {
                    return Ok(payload);
                }
                return StatusCode(payload.Status, payload);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// funcion que ejecuta una actualizacion basada en un diccionario de nombre de propiedad y valor de la propiedad
        /// </summary>
        /// <param name="data">diccionario de nombre de propiedad y valor de la propiedad</param>
        /// <returns>resultado de una accion</returns>
        public virtual async Task<IActionResult> Update(IDictionary<string, object> data)
        {
            try
            {
                var objeto = _service.MapModelFromDictionary(data);

                var payload = await _service.Actualizar(objeto);

                if (payload.Status == 200)
                {
                    return Ok(payload);
                }
                return StatusCode(payload.Status, payload);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// funcion que ejecuta una eliminacion basada en un diccionario de nombre de propiedad y valor de la propiedad que conforman por lo menos una llave principal
        /// </summary>
        /// <param name="data">diccionario de nombre de propiedad y valor de la propiedad que conforman por lo menos una llave principal</param>
        /// <returns>resultado de una accion</returns>
        public virtual async Task<IActionResult> RemoveEntity(IDictionary<string, string> filter)
        {
            try
            {
                var modelo = await _service.GetById(filter.Values.ToArray());

                if (modelo.Status != 1)
                    return BadRequest(modelo);

                var payload = await _service.Remove(filter.Values.ToArray());

                return StatusCode(payload.Status, payload);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// funcion que ejecuta una eliminacion basada en un diccionario de nombre de propiedad y valor de la propiedad que conforman por lo menos una llave principal
        /// </summary>
        /// <param name="data">diccionario de nombre de propiedad y valor de la propiedad que conforman por lo menos una llave principal</param>
        /// <returns>resultado de una accion</returns>
        public virtual async Task<IActionResult> RemoveEntity(params object[] keyvalues)
        {
            try
            {
                var modelo = await _service.GetById(keyvalues);

                if (modelo.Status != 1)
                    return BadRequest(modelo);

                var payload = await _service.Remove(keyvalues);

                return StatusCode(payload.Status, payload);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
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
