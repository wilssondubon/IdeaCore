using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreUtils.Attributes
{
    /// <summary>
    /// atributo que indica si un modelo debe descartar sus propiedades de navegacion ante una operacion de insercion o modificacion
    /// </summary>
    public class NoNavigationsOnCreateUpdate : Attribute
    {
        /// <summary>
        /// si la propiedad debe descartar sus propiedades de navegacion
        /// </summary>
        public bool NoNavigations { get; set; }
        /// <summary>
        /// inicializa el atributo
        /// </summary>
        /// <param name="pNoNavigations">si descarta sus propiedades de navegacion</param>
        public NoNavigationsOnCreateUpdate(bool pNoNavigations) => NoNavigations = pNoNavigations;
        /// <summary>
        /// inicializa el atributo con valor verdadero para descartar las propiedades de navegacion
        /// </summary>
        public NoNavigationsOnCreateUpdate() => NoNavigations = true;
    }
}
