using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreApplication.Attributes
{
    /// <summary>
    /// atributo que define si el valor de una propiedad de un modelo debe definirse con el valor maximo + Skip tomando alguna otra propiedad
    /// marcada con el atributo UseToCalculateMax a la hora de una operacion de insercion
    /// </summary>
    public class LastOnInsertComposed : Attribute
    {
        /// <summary>
        /// define si debera calcularse el valor maximo
        /// </summary>
        public bool IsLast { get; set; }
        /// <summary>
        /// define la cantidad mas alla del maximo que se le asignara a la propiedad
        /// </summary>
        public long Skip { get; set; }
        /// <summary>
        /// inicializa el atributo con un valor de Skip 1
        /// </summary>
        /// <param name="pIsLast"></param>
        public LastOnInsertComposed(bool pIsLast)
        {
            IsLast = pIsLast;
            Skip = 1L;
        }
        /// <summary>
        /// inicializa el atributo
        /// </summary>
        /// <param name="pIsLast"></param>
        /// <param name="pSkip"></param>
        public LastOnInsertComposed(bool pIsLast, long pSkip)
        {
            IsLast = pIsLast;
            Skip = pSkip;
        }
        /// <summary>
        /// inicializa el atributo con un valor de Skip definido
        /// </summary>
        /// <param name="pSkip">valor de skip</param>
        public LastOnInsertComposed(long pSkip)
        {
            IsLast = true;
            Skip = pSkip;
        }
        /// <summary>
        /// inicializa el atributo con valor de skip 1
        /// </summary>
        public LastOnInsertComposed()
        {
            IsLast = true;
            Skip = 1L;
        }
    }
}
