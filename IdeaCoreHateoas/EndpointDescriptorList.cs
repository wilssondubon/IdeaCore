using IdeaCoreInterfaces.Hateoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreHateoas
{
    /// <summary>
    /// lista de descriptores de endpoint {IEndpointDescriptor}
    /// </summary>
    internal class EndpointDescriptorList : List<IEndpointDescriptor>, IEndpointDescriptorList
    {
        /// <summary>
        /// inicializa las descripciones de endpoints
        /// </summary>
        public EndpointDescriptorList() : base() { }
        /// <summary>
        /// funcion para agregar un nuevo descriptor a la lista
        /// </summary>
        /// <param name="relacion">relacion del endpoint con el modelo</param>
        /// <param name="accion">titulo del endpoint</param>
        /// <param name="controlador">controlador al que pertenece el endpoint</param>
        /// <param name="metodo">verbo del endpoint</param>
        /// <param name="valores">tipo de funcion para llamada callback mediante el cual entra un modelo y retorna los campos que conforman una llave primaria</param>
        public void Add(string relacion, string accion, string controlador, string metodo, Func<object, object> valores = null)
        {
            this.Add(new EndpointDescriptor(relacion, accion, controlador, metodo, valores));
        }
    }
}
