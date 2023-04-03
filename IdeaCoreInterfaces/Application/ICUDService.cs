using IdeaCoreInterfaces.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInterfaces.Application
{
    /// <summary>
    /// interfaz para un servicio de operaciones Create, Update, Delete
    /// </summary>
    /// <typeparam name="Model">tipo del modelo en el que se basara un controlador</typeparam>
    public interface ICUDService<Model, Entity> : ICommonService<Model, Entity> where Model : class where Entity : class
    {
        /// <summary>
        /// ejecuta una actualizacion en un elemento basado en un modelo
        /// </summary>
        /// <param name="data">modelo de donde se tomaran los valores a actualizar</param>
        /// <returns>Una tarea que representa una operacion asincrona. la tarea contiene una respuesta basada en el modelo</returns>
        Task<IServiceResponse<Model>> Actualizar(Model data);
        /// <summary>
        /// ejecuta una insercion de un elemento basado en un modelo
        /// </summary>
        /// <param name="data">modelo de donde se tomaran los valores a insertar</param>
        /// <returns>Una tarea que representa una operacion asincrona. la tarea contiene una respuesta basada en el modelo</returns>
        Task<IServiceResponse<Model>> Crear(Model data);
        /// <summary>
        /// ejecuta una eliminacion de un elemento basado en un modelo
        /// </summary>
        /// <param name="keyvalues">representa un array de todos los campos que componen el primary key</param>
        /// <returns>Una tarea que representa una operacion asincrona. la tarea contiene una respuesta basada en el modelo</returns>
        Task<IServiceResponse<Model>> Remove(params object[] keyvalues);
    }
}
