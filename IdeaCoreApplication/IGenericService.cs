using IdeaCoreInterfaces.Application;
using IdeaCoreInterfaces.Application.Response;
using IdeaCoreInterfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreApplication
{
    public interface IGenericService<Model, Entity> : ICommonService<Model, Entity> where Model : class where Entity : class
    {
        /// <summary>
        /// ejecuta la actualizacion de un elemento basado en un modelo
        /// </summary>
        /// <param name="data">elemento del que se tomaran los valores para actualizar</param>
        /// <returns>Una tarea que representa una operacion asincrona. la tarea contiene un objecto de clase respuesta basado en el modelo</returns>
        Task<IServiceResponse<Model>> Actualizar(Model data);
        /// <summary>
        /// ejecuta la insersion de un elemento basado en un modelo
        /// </summary>
        /// <param name="data">lemento del que se tomaran los valores para insertar</param>
        /// <returns>Una tarea que representa una operacion asincrona. la tarea contiene un objecto de clase respuesta basado en el modelo</returns>
        Task<IServiceResponse<Model>> Crear(Model data);
        //// <summary>
        /// Obtiene una coleccion de objectos de clase listas de respuestas basadas en el modelo
        /// </summary>
        /// <param name="filterparams">valores opcionales para paginar y ordenar la respuesta</param>
        /// <returns>Una tarea que representa una operacion asincrona. la tarea contiene una coleccion de objectos de clase listas de respuestas basadas en el modelo</returns>
        Task<IServiceResponseList<Model>> GetBy(IFilterQueryParams filterparams = null);
        /// <summary>
        /// Obtiene una coleccion de objectos filtrados de clase listas de respuestas basadas en el modelo
        /// </summary>
        /// <param name="filter">expresion de predicado lambda para filtrar la coleccion de respuestas</param>
        /// <param name="filterparams">valores opcionales para paginar y ordenar la respuesta</param>
        /// <returns>Una tarea que representa una operacion asincrona. la tarea contiene una coleccion de objectos de clase listas de respuestas basadas en el modelo</returns>
        Task<IServiceResponseList<Model>> GetBy(Expression<Func<Model, bool>> filter, IFilterQueryParams filterparams = null);
        /// <summary>
        /// Obtiene una coleccion de objectos filtrados de clase listas de respuestas basadas en el modelo
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <param name="filterparams">valores opcionales para paginar y ordenar la respuesta</param>
        /// <returns>Una tarea que representa una operacion asincrona. la tarea contiene devuelve una coleccion de objectos de clase listas de respuestas basadas en el modelo</returns>
        Task<IServiceResponseList<Model>> GetBy(IDictionary<string, string> filter, IFilterQueryParams filterparams = null);
        /// <summary>
        /// ejecuta una eliminacion de un elemento basado en un modelo
        /// </summary>
        /// <param name="keyvalues">representa un array de todos los campos que componen el primary key</param>
        /// <returns>Una tarea que representa una operacion asincrona. la tarea contiene una respuesta basada en el modelo</returns>
        Task<IServiceResponse<Model>> Remove(params object[] keyvalues);
    }
}
