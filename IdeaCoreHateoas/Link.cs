using IdeaCoreHateoas.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreHateoas
{
    /// <summary>
    /// clase de un modelo de link hateos
    /// </summary>
    public class Link : ILink
    {
        /// <summary>
        /// url
        /// </summary>
        public Uri Href { get; set; }
        /// <summary>
        /// relacion que la url posee con respecto a la solicitud presente
        /// </summary>
        public string Rel { get; set; }
        /// <summary>
        /// verbo de la url para una solicitud
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// inicializa la clase
        /// </summary>
        public Link()
        {
        }
        /// <summary>
        /// inicializa la clase
        /// </summary>
        /// <param name="href">url</param>
        /// <param name="rel">relacion que la url posee con respecto a la solicitud presente</param>
        /// <param name="method">verbo de la url para una solicitud</param>
        public Link(Uri href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}
