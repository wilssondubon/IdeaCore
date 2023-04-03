using IdeaCoreHateoas.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreHateoas
{
    /// <summary>
    /// clase para describir las propiedades de un endpoint en particular
    /// </summary>
    internal class EndpointDescriptor : IEndpointDescriptor
    {
        /// <summary>
        /// relacion del endpoint hacia el modelo
        /// </summary>
        public string Relacion { get; }
        /// <summary>
        /// titulo o nombre del endpoint
        /// </summary>
        public string Accion { get; }
        /// <summary>
        /// controlador al que pertenece el endpoint
        /// </summary>
        public string Controlador { get; }
        /// <summary>
        /// verbo del endopoint
        /// </summary>
        public string Metodo { get; }
        /// <summary>
        /// tipo de funcion para llamada callback mediante el cual entra un modelo y retorna los campos que conforman una llave primaria
        /// </summary>
        public Func<object, object> Valores { get; }
        /// <summary>
        /// inicializa la descripcion del endpoint
        /// </summary>
        /// <param name="relacion">relacion del endpoint hacia el modelo</param>
        /// <param name="accion">titulo o nombre del endpoint</param>
        /// <param name="controlador">controlador al que pertenece el endpoint</param>
        /// <param name="metodo">verbo del endopoint</param>
        /// <param name="valores">tipo de funcion para llamada callback mediante el cual entra un modelo y retorna los campos que conforman una llave primaria</param>
        public EndpointDescriptor(string relacion, string accion, string controlador, string metodo, Func<object, object> valores = null)
        {
            Relacion = relacion;
            Accion = accion;
            Controlador = controlador;
            Metodo = metodo;
            Valores = valores;
        }
    }
}
