using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreApplication.Attributes
{
    /// <summary>
    /// atributo que define si una propiedad debe obtener un nuevo valor a la hora de una operacion de update
    /// </summary>
    public class MergeOnUpdate : Attribute
    {
        /// <summary>
        /// define si la propiedad debe obtener un nuevo valor
        /// </summary>
        public bool Merge { get; set; }
        /// <summary>
        /// inicializa el atributo
        /// </summary>
        /// <param name="pMerge">si es o no una propiedad actualizable</param>
        public MergeOnUpdate(bool pMerge) => Merge = pMerge;
        /// <summary>
        /// inicializa el atributo con la propiedad actualizable por default
        /// </summary>
        public MergeOnUpdate() => Merge = true;
    }
}
