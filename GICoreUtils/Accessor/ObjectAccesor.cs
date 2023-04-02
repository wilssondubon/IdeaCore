using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreUtils.Accessor
{
    /// <summary>
    /// clase para ayudar a acceder a las propiedades de una clase por nombre
    /// contiene funciones para obtener valores y definir valores dentro de la clase de manera dinamica
    /// </summary>
    public class ObjectAccesor
    {
        /// <summary>
        /// objeto al que se accedera
        /// </summary>
        private readonly object Component;
        /// <summary>
        /// inicializa la clase para acceder a las propiedades de un objeto
        /// </summary>
        /// <param name="pComponent">objeto al que se accedera</param>
        public ObjectAccesor(object pComponent) => Component = pComponent;
        /// <summary>
        /// obtiene el valor de una propiedad
        /// </summary>
        /// <param name="PropertyName">nombre de la propiedad</param>
        /// <returns>valor de la propiedad</returns>
        public object Get(string PropertyName) => new PropertyAccessor(Component.GetType(), PropertyName).GetValue(Component);
        /// <summary>
        /// define un nuevo valor para una propiedad
        /// </summary>
        /// <param name="PropertyName">nombre de la propiedad</param>
        /// <param name="value">nuevo valor a definir</param>
        public void Set(string PropertyName, object value) => new PropertyAccessor(Component.GetType(), PropertyName).SetValue(Component, value);
    }
}
