using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreInterfaces.Presentation.API.Hateoas
{
    /// <summary>
    /// interfaz para describir las propiedades de un endpoint en particular
    /// </summary>
    public interface IEndpointDescriptor
    {
        /// <summary>
        /// titulo o nombre del endpoint
        /// </summary>
        string Accion { get; }
        /// <summary>
        /// controlador al que pertenece el endpoint
        /// </summary>
        string Controlador { get; }
        /// <summary>
        /// verbo del endopoint
        /// </summary>
        string Metodo { get; }
        /// <summary>
        /// relacion del endpoint hacia el modelo
        /// </summary>
        string Relacion { get; }
        /// <summary>
        /// tipo de funcion para llamada callback mediante el cual entra un modelo y retorna los campos que conforman una llave primaria
        /// </summary>
        Func<object, object> Valores { get; }
    }
}
