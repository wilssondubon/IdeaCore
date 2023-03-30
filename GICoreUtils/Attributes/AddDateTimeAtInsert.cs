using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreUtils.Attributes
{
    /// <summary>
    /// atributo para controlar si un campo de un modelo requerira definirse con valor de fecha actual a la hora de una operacion de insercion
    /// </summary>
    public class AddDateTimeAtInsert : Attribute
    {
        /// <summary>
        /// define si se colocara la fecha
        /// </summary>
        public bool AddDateTime { get; set; }
        /// <summary>
        /// define el nombre del campo
        /// </summary>
        public string DateTimeName { get; set; }
        /// <summary>
        /// inicializa el atributo con valor verdadero para AddDateTime
        /// </summary>
        /// <param name="pDateTimeName">nombre del campo</param>
        public AddDateTimeAtInsert(string pDateTimeName)
        {
            AddDateTime = true;
            DateTimeName = pDateTimeName;
        }
    }
}
