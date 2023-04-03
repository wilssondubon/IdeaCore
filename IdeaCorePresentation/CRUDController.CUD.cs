using IdeaCorePresentation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCorePresentation
{
    public partial class CRUDController<Model, Entity> : Controller<Model> where Model : class where Entity : class
    {
        /// <summary>
        /// controlador con funciones que sirven para hacer Create, Update, Delete
        /// </summary>
        protected readonly CUDController<Model, Entity> cudController;
        protected virtual async Task<IActionResult> ExecuteCUDAction(Func<Task<IActionResult>> controllerAction)
        {
            cudController.ControllerContext = ControllerContext;
            return await controllerAction();
        }
        /// <summary>
        /// funcion que ejecuta una insercion
        /// </summary>
        /// <param name="data">modelo para insertar</param>
        /// <returns>resultado de una accion</returns>
        protected virtual async Task<IActionResult> _SaveNew(Model data)
            => await ExecuteCUDAction(() => cudController.SaveNew(data));
        /// <summary>
        /// funcion que ejecuta una insercion basada en un diccionario de nombre de propiedad y valor de la propiedad
        /// </summary>
        /// <param name="data">diccionario de nombre de propiedad y valor de la propiedad</param>
        /// <returns>resultado de una accion</returns>
        protected virtual async Task<IActionResult> _SaveNew(IDictionary<string, object> data)
            => await ExecuteCUDAction(() => cudController.SaveNew(data));
        /// <summary>
        /// funcion que ejecuta una actualizacion
        /// </summary>
        /// <param name="data">modelo para actualizar</param>
        /// <returns>resultado de una accion</returns>
        protected virtual async Task<IActionResult> _Update(Model data)
            => await ExecuteCUDAction(() => cudController.Update(data));
        /// <summary>
        /// funcion que ejecuta una actualizacion basada en un diccionario de nombre de propiedad y valor de la propiedad
        /// </summary>
        /// <param name="data">diccionario de nombre de propiedad y valor de la propiedad</param>
        /// <returns>resultado de una accion</returns>
        protected virtual async Task<IActionResult> _Update(IDictionary<string, object> data)
            => await ExecuteCUDAction(() => cudController.Update(data));
        /// <summary>
        /// funcion que ejecuta una eliminacion basada en un diccionario de nombre de propiedad y valor de la propiedad que conforman por lo menos una llave principal
        /// </summary>
        /// <param name="data">diccionario de nombre de propiedad y valor de la propiedad que conforman por lo menos una llave principal</param>
        /// <returns>resultado de una accion</returns>
        protected virtual async Task<IActionResult> _RemoveEntity(IDictionary<string, string> filter)
            => await ExecuteCUDAction(() => cudController.RemoveEntity(filter));
        /// <summary>
        /// funcion que ejecuta una eliminacion basada en un diccionario de nombre de propiedad y valor de la propiedad que conforman por lo menos una llave principal
        /// </summary>
        /// <param name="data">array de valores que conforman por lo menos una llave principal</param>
        /// <returns>resultado de una accion</returns>
        protected virtual async Task<IActionResult> _RemoveEntity(params object[] keyvalues)
            => await ExecuteCUDAction(() => cudController.RemoveEntity(keyvalues));
    }
}
