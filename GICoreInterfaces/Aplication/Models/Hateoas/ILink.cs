using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreInterfaces.Aplication.Models.Hateoas
{
    /// <summary>
    /// interfaz de un modelo de link hateos
    /// </summary>
    public interface ILink
    {
        /// <summary>
        /// url
        /// </summary>
        Uri Href { get; set; }
        /// <summary>
        /// verbo de la url para una solicitud
        /// </summary>
        string Method { get; set; }
        /// <summary>
        /// relacion que la url posee con respecto a la solicitud presente
        /// </summary>
        string Rel { get; set; }
    }
}
