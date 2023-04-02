using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreUtils.Attributes
{
    /// <summary>
    /// atributo que define si una propiedad debe ser tomada en cuenta a la hora de calcular un maximo para otra propiedad 
    /// en una operacion de insercion
    /// </summary>
    public class UseToCalculateMax : Attribute
    {
        /// <summary>
        /// si debe ser tomada en cuenta
        /// </summary>
        public bool UseToCalculate { get; set; }
        /// <summary>
        /// nombre de la propiedad a tomar en cuenta
        /// </summary>
        public string MaxProperty { get; set; }
        /// <summary>
        /// inicializa el atributo
        /// </summary>
        /// <param name="pMaxProperty">nombre de la propiedad a tomar en cuenta</param>
        public UseToCalculateMax(string pMaxProperty)
        {
            UseToCalculate = true;
            MaxProperty = pMaxProperty;
        }
    }
}
