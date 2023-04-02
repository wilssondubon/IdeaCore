using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreUtils.Accessor
{
    /// <summary>
    /// clase para ayudar a acceder dinamicamente a una propiedad de un objeto
    /// </summary>
    public class PropertyAccessor
    {
        /// <summary>
        /// tipo del delegado para definir nuevo valor a una propiedad
        /// </summary>
        private SetValueHandler setValueHandler;
        /// <summary>
        /// tipo del delegado para obtener el valor de una propiedad
        /// </summary>
        private GetValueHandler getValueHandler;
        /// <summary>
        /// inicializa la clase con el tipo del objeto y el nombre de la propiedad a acceder
        /// </summary>
        /// <param name="ownerType">tipo del objeto</param>
        /// <param name="propertyName">nombre de la propiedad a acceder</param>
        public PropertyAccessor(Type ownerType, string propertyName)
        {
            PropertyInfo property = ownerType.GetProperty(propertyName);
            if (property.CanRead)
                getValueHandler = CreateGetValueHandler(property);
            if (!property.CanWrite)
                return;
            setValueHandler = CreateSetValueHandler(property);
        }
        /// <summary>
        /// crea un delegado para obtener el valor de la propiedad
        /// </summary>
        /// <param name="propertyInfo">informacion reflectiva de la propiedad</param>
        /// <returns>delegado</returns>
        protected virtual GetValueHandler CreateGetValueHandler(
          PropertyInfo propertyInfo)
        {
            MethodInfo getMethod = propertyInfo.GetGetMethod();
            DynamicMethod dynamicMethod = new DynamicMethod("GetValue", typeof(object), new Type[1]
            {
        typeof (object)
            }, typeof(PropertyAccessor), true);
            ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Call, getMethod);
            Type returnType = getMethod.ReturnType;
            if (returnType.IsValueType)
                ilGenerator.Emit(OpCodes.Box, returnType);
            ilGenerator.Emit(OpCodes.Ret);
            return dynamicMethod.CreateDelegate(typeof(GetValueHandler)) as GetValueHandler;
        }
        /// <summary>
        /// obtiene el valor de la propiedad
        /// </summary>
        /// <param name="component">objeto de donde se tomara la propiedad</param>
        /// <returns>el valor de la propiedad</returns>
        /// <exception cref="InvalidOperationException">devuelve error si no hay un delegado para obtener el valor de la propiedad  o no esta inicializado</exception>
        public object GetValue(object component)
        {
            if (getValueHandler == null)
                throw new InvalidOperationException();
            return getValueHandler(component);
        }
        /// <summary>
        /// crea un delegado para definir el valor de la propiedad
        /// </summary>
        /// <param name="propertyInfo">informacion reflectiva de la propiedad</param>
        /// <returns>delegado</returns>
        protected virtual SetValueHandler CreateSetValueHandler(
      PropertyInfo propertyInfo)
        {
            MethodInfo setMethod = propertyInfo.GetSetMethod(false);
            DynamicMethod dynamicMethod = new DynamicMethod("SetValue", typeof(void), new Type[2]
            {
        typeof (object),
        typeof (object)
            }, typeof(PropertyAccessor), true);
            ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldarg_1);
            Type parameterType = setMethod.GetParameters()[0].ParameterType;
            if (parameterType.IsValueType)
                ilGenerator.Emit(OpCodes.Unbox_Any, parameterType);
            ilGenerator.Emit(OpCodes.Call, setMethod);
            ilGenerator.Emit(OpCodes.Ret);
            return dynamicMethod.CreateDelegate(typeof(SetValueHandler)) as SetValueHandler;
        }
        /// <summary>
        /// define el valor de la propiedad
        /// </summary>
        /// <param name="component">objeto de donde se tomara la propiedad</param>
        /// <param name="value">nueva valor para la propiedad</param>
        /// <exception cref="InvalidOperationException">devuelve error si no hay un delegado para definir el valor de la propiedad o no esta inicializado</exception>
        public void SetValue(object component, object value)
        {
            if (setValueHandler == null)
                throw new InvalidOperationException();
            setValueHandler(component, value);
        }
        /// <summary>
        /// delegado para definir el valor de la propiedad
        /// </summary>
        /// <param name="component">objeto de donde se tomara la propiedad</param>
        /// <param name="value">nuevo valor para la propiedad</param>
        protected delegate void SetValueHandler(object component, object value);
        /// <summary>
        /// delegado para obtener el valor de la propiedad
        /// </summary>
        /// <param name="component">objeto de donde se tomara la propiedad</param>
        /// <returns>el valor de la propiedad</returns>
        protected delegate object GetValueHandler(object component);
    }
}
