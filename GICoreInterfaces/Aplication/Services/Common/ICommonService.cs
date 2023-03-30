using GICoreInterfaces.Aplication.Services.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreInterfaces.Aplication.Services.Common
{
    /// <summary>
    /// interfaz para operaciones comunes de un servicio
    /// </summary>
    /// <typeparam name="Model">tipo del modelo en el que se basara un servicio</typeparam>
    public interface ICommonService<Model, Entity> where Model : class where Entity : class
    {
        /// <summary>
        /// convierte una entidad en el modelo
        /// </summary>
        /// <typeparam name="T">tipo de la entidad</typeparam>
        /// <param name="o">entidad a convertirse</param>
        /// <returns>objeto de clase del modelo</returns>
        Model MapModel<T>(T o) where T : class;
        /// <summary>
        /// convierte un diccionario de tipo nombre de propiedad y valor de la propiedad en un modelo
        /// </summary>
        /// <param name="parameters">diccionario de tipo nombre de propiedad y valor de la propiedad</param>
        /// <returns>objeto de clase del modelo</returns>
        Model MapModelFromDictionary(IDictionary<string, object> parameters);
        /// <summary>
        /// convierte una entidad de un tipo definido generico en el modelo
        /// </summary>
        /// <typeparam name="T">tipo de la entidad</typeparam>
        /// <param name="o">entidad a convertirse</param>
        /// <returns>objeto de clase del modelo</returns>
        Model MapModelFromDynamic<T>(T o) where T : class;
        /// <summary>
        /// convierte una coleccion de entidades en una coleccion del modelo
        /// </summary>
        /// <typeparam name="T">tipo de la entidad</typeparam>
        /// <param name="o">entidades a convertirse</param>
        /// <returns></returns>
        IEnumerable<Model> MapModels<T>(T o) where T : class;
        /// <summary>
        /// devuelve un diccionario de tipo nombre de propiedad y valor de la propiedad con las propiedades que forman parte de la llave principal de la entidad
        /// </summary>
        /// <param name="source">modelo de donde se tomaran los valores de las propiedades</param>
        /// <returns>diccionario de tipo nombre de propiedad y valor de la propiedad</returns>
        object ModelKeyParameters(Model source);
        /// <summary>
        /// Devuelve un elemento tipo respuesta basado en el Primary Key de una entidad
        /// </summary>
        /// <param name="keyvalues">representa un array de todos los campos que componen el primary key</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene un modelo de tipo respuesta.</returns>
        Task<IServiceResponse<Model>> GetById(params object[] keyvalues);
    }
}
