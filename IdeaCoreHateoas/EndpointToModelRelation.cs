using IdeaCoreHateoas.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreHateoas
{
    /// <summary>
    /// clase para un diccionario de tipo: tipo del modelo y descripciones de sus endpoints
    /// </summary>
    public class EndpointToModelRelation : Dictionary<Type, IEndpointDescriptorList>, IEndpointToModelRelation
    {
        /// <summary>
        /// tipo del modelo al que se le describiran sus endpoints
        /// </summary>
        internal Type ModelType;
        /// <summary>
        /// lista de endpoints
        /// </summary>
        internal EndpointDescriptorList Endpoints;
        /// <summary>
        /// inicializa el diccionario
        /// </summary>
        public EndpointToModelRelation() : base()
        {
        }
        /// <summary>
        /// inicializa el diccionario con valores de un diccionario existente
        /// </summary>
        /// <param name="values">diccionario existente</param>
        public EndpointToModelRelation(IDictionary<Type, IEndpointDescriptorList> values) : base(values)
        {
        }
        /// <summary>
        /// inicializa el diccionario con descripcion de endpoints ya existentes
        /// </summary>
        /// <param name="relation">descripciones de endpoints</param>
        public EndpointToModelRelation(EndpointToModelRelation relation) : base(relation)
        {
            ModelType = relation.ModelType;
            Endpoints = relation.Endpoints;
        }
        // <summary>
        /// funcion para inicializar un nuevo tipo de modelo y poder proceder a agregar sus descripciones
        /// </summary>
        /// <param name="modelType">tipo del modelo</param>
        /// <returns>diccionario de relaciones entre modelos y endpoints</returns>
        public IEndpointToModelRelation RegisterModel(Type modelType)
        {
            if (Endpoints != null && Endpoints.Count > 0)
            {
                Add(ModelType, Endpoints);
                Endpoints = null;
            }

            ModelType = modelType;

            return this;
        }
        /// <summary>
        /// funcion para agregar una descripcion de endpoint a un tipo de modelo dentro de un diccionario de relaciones entre modelos y endpoints
        /// </summary>
        /// <param name="relacion">relacion del endpoint con el modelo</param>
        /// <param name="accion">titulo nombre del endpoint</param>
        /// <param name="controlador">controlador al que pertenece el endpoint</param>
        /// <param name="metodo">metodo del endpoint</param>
        /// <param name="valores">tipo de funcion para llamada callback mediante el cual entra un modelo y retorna los campos que conforman una llave primaria</param>
        /// <returns>diccionario de relaciones entre modelos y endpoints</returns>
        public IEndpointToModelRelation AddEndPoint(string relacion, string accion, string controlador, string metodo, Func<object, object> valores = null)
        {
            if (Endpoints == null)
                Endpoints = new EndpointDescriptorList();

            Endpoints.Add(relacion, accion, controlador, metodo, valores);

            return this;
        }
        /// <summary>
        /// termina y cierra la el registro de descripciones de endpoints para un tipo de modelo
        /// </summary>
        /// <returns>diccionario de relaciones entre modelos y endpoints</returns>
        public IEndpointToModelRelation Build()
        {
            if (Endpoints != null && Endpoints.Count > 0)
            {
                Add(ModelType, Endpoints);
                Endpoints = null;
            }

            ModelType = null;

            return this;
        }
        /// <summary>
        /// funcion para obtener todos los endpoints relacionados a un modelo
        /// </summary>
        /// <param name="modelType">tipo del modelo</param>
        /// <returns>descripciones de endpoints</returns>
        public IEndpointDescriptorList GetEndpointsForModel(Type modelType)
        {
            return this.Where(t => t.Key == modelType).Select(t => t.Value).FirstOrDefault();
        }
        /// <summary>
        /// funcion para obtener un endpoint especifico relacionado a un modelo basado en su relacion
        /// </summary>
        /// <param name="modelType">tipo del modelo</param>
        /// <param name="relacion">relacion del endpoint con el modelo</param>
        /// <returns>descripcion de endpoint</returns>
        public IEndpointDescriptor GetEndPoint(Type modelType, string relacion)
        {
            return this.Where(t => t.Key == modelType).Select(t => t.Value).FirstOrDefault().Where(t => t.Relacion == relacion).FirstOrDefault();
        }

    }
}
