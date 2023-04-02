using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInterfaces.Presentation.API.Hateoas
{
    /// <summary>
    /// interfaz para un diccionario de tipo: tipo del modelo y descripciones de sus endpoints
    /// </summary>
    public interface IEndpointToModelRelation : IDictionary<Type, IEndpointDescriptorList>
    {
        /// <summary>
        /// funcion para obtener todos los endpoints relacionados a un modelo
        /// </summary>
        /// <param name="modelType">tipo del modelo</param>
        /// <returns>descripciones de endpoints</returns>
        IEndpointDescriptorList GetEndpointsForModel(Type modelType);
        /// <summary>
        /// funcion para obtener un endpoint especifico relacionado a un modelo basado en su relacion
        /// </summary>
        /// <param name="modelType">tipo del modelo</param>
        /// <param name="relacion">relacion del endpoint con el modelo</param>
        /// <returns>descripcion de endpoint</returns>
        IEndpointDescriptor GetEndPoint(Type modelType, string relacion);
        /// <summary>
        /// funcion para inicializar un nuevo tipo de modelo y poder proceder a agregar sus descripciones
        /// </summary>
        /// <param name="modelType">tipo del modelo</param>
        /// <returns>diccionario de relaciones entre modelos y endpoints</returns>
        IEndpointToModelRelation RegisterModel(Type modelType);
        /// <summary>
        /// funcion para agregar una descripcion de endpoint a un tipo de modelo dentro de un diccionario de relaciones entre modelos y endpoints
        /// </summary>
        /// <param name="relacion">relacion del endpoint con el modelo</param>
        /// <param name="accion">titulo nombre del endpoint</param>
        /// <param name="controlador">controlador al que pertenece el endpoint</param>
        /// <param name="metodo">metodo del endpoint</param>
        /// <param name="valores">tipo de funcion para llamada callback mediante el cual entra un modelo y retorna los campos que conforman una llave primaria</param>
        /// <returns>diccionario de relaciones entre modelos y endpoints</returns>
        IEndpointToModelRelation AddEndPoint(string relacion, string accion, string controlador, string metodo, Func<object, object> valores = null);
        /// <summary>
        /// termina y cierra la el registro de descripciones de endpoints para un tipo de modelo
        /// </summary>
        /// <returns>diccionario de relaciones entre modelos y endpoints</returns>
        IEndpointToModelRelation Build();
    }
}
