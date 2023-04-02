using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IdeaCoreUtils.Attributes;
using IdeaCoreUtils.Accessor;
using IdeaCoreInterfaces.Aplication.Services.Common.Response;
using IdeaCoreInterfaces.Aplication.Services.Common;

namespace GICoreServices
{
    public partial class CommonService<Model, Entity> : ICUDService<Model, Entity> where Model : class where Entity : class
    {
        /// <summary>
        /// ejecuta una insercion de un elemento basado en un modelo
        /// </summary>
        /// <param name="data">modelo de donde se tomaran los valores a insertar</param>
        /// <returns>Una tarea que representa una operacion asincrona. la tarea contiene una respuesta basada en el modelo</returns>
        public async Task<IServiceResponse<Model>> Crear(Model data)
        {
            try
            {
                Entity newobjeto = CreateNew(_mapper.Map<Entity>(data));
                _repositorio.Insert(newobjeto);
                int num = await _unitOfWork.SaveChangesAsync();
                return Response(201, newobjeto);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// ejecuta una actualizacion en un elemento basado en un modelo
        /// </summary>
        /// <param name="data">modelo de donde se tomaran los valores a actualizar</param>
        /// <returns>Una tarea que representa una operacion asincrona. la tarea contiene una respuesta basada en el modelo</returns>
        public async Task<IServiceResponse<Model>> Actualizar(Model data)
        {
            try
            {
                Entity objeto = await _repositorio.Get(data);
                if (objeto == null)
                    return noFoundError();
                objeto = Merge(objeto, data);
                _repositorio.Update(objeto);
                int num = await _unitOfWork.SaveChangesAsync();
                return Response(objeto);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// ejecuta una eliminacion de un elemento basado en un modelo
        /// </summary>
        /// <param name="keyvalues">representa un array de todos los campos que componen el primary key</param>
        /// <returns>Una tarea que representa una operacion asincrona. la tarea contiene una respuesta basada en el modelo</returns>
        public async Task<IServiceResponse<Model>> Remove(params object[] keyvalues)
        {
            try
            {
                Entity objeto = await _repositorio.FindByID(keyvalues);
                if (objeto == null)
                    return noFoundError();
                _repositorio.Delete(objeto);
                int num = await _unitOfWork.SaveChangesAsync();
                return Response(204, objeto);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// toma un modelo y una entidad para darle nuevos valores a la entidad basandose en los valores del modelo
        /// </summary>
        /// <param name="destination">entidad a ser modificada</param>
        /// <param name="source">modelo de donde se tomaran los valores</param>
        /// <returns>entidad modificada</returns>
        protected Entity Merge(Entity destination, Model source)
        {
            NoNavigationsOnCreateUpdate customAttribute = (NoNavigationsOnCreateUpdate)Attribute.GetCustomAttribute(typeof(Model), typeof(NoNavigationsOnCreateUpdate));
            ObjectAccesor objectAccesor1 = new ObjectAccesor(destination);
            if (customAttribute != null && customAttribute.NoNavigations)
            {
                string[] array = _repositorio.GetNavigationProperties().ToArray();
                if (array != null && array.Length != 0)
                {
                    foreach (string PropertyName in array)
                        objectAccesor1.Set(PropertyName, null);
                }
            }
            string[] array1 = typeof(Model).GetMembers().Where(m => Attribute.GetCustomAttribute(m, typeof(MergeOnUpdate)) != null && ((MergeOnUpdate)Attribute.GetCustomAttribute(m, typeof(MergeOnUpdate))).Merge).Select(m => m.Name).ToArray();
            ObjectAccesor objectAccesor2 = new ObjectAccesor(source);
            foreach (string PropertyName in array1)
                objectAccesor1.Set(PropertyName, objectAccesor2.Get(PropertyName));
            return destination;
        }
        /// <summary>
        /// funcion mediante la cual se procesan una entidad para generar los valores para su insercion y quitar las propiedades que no son pertinentes basandose en los atributos del modelo
        /// </summary>
        /// <param name="destination">entidad a ser trabajada</param>
        /// <returns>entidad trabajada</returns>
        protected Entity CreateNew(Entity destination)
        {
            NoNavigationsOnCreateUpdate customAttribute1 = (NoNavigationsOnCreateUpdate)Attribute.GetCustomAttribute(typeof(Model), typeof(NoNavigationsOnCreateUpdate));
            ObjectAccesor objectAccesor = new ObjectAccesor(destination);
            string[] array1 = _repositorio.GetNavigationProperties().ToArray();
            if (customAttribute1 != null && customAttribute1.NoNavigations && array1 != null && array1.Length != 0)
            {
                foreach (string PropertyName in array1)
                    objectAccesor.Set(PropertyName, null);
            }
            var array2 = typeof(Model).GetMembers().Where(m => Attribute.GetCustomAttribute(m, typeof(LastOnInsert)) != null && ((LastOnInsert)Attribute.GetCustomAttribute(m, typeof(LastOnInsert))).IsLast).Select(m => new
            {
                Member = m,
                Attribute = (LastOnInsert)Attribute.GetCustomAttribute(m, typeof(LastOnInsert))
            }).ToArray();
            if (array2 != null && array2.Length != 0)
            {
                var data = array2[0];
                ParameterExpression parameterExpression = Expression.Parameter(typeof(Entity), "ent");
                MemberExpression memberExpression = Expression.Property(parameterExpression, typeof(Entity).GetProperty(data.Member.Name));
                long num = _repositorio.DbSet.Select(Expression.Lambda<Func<Entity, long>>(Expression.Convert(memberExpression, typeof(long)), parameterExpression)).Max();
                long skip = data.Attribute.Skip;
                objectAccesor.Set(data.Member.Name, Convert.ChangeType((num + skip), memberExpression.Type));
            }
            var array3 = typeof(Model).GetMembers().Where(m => Attribute.GetCustomAttribute(m, typeof(LastOnInsertComposed)) != null && ((LastOnInsertComposed)Attribute.GetCustomAttribute(m, typeof(LastOnInsertComposed))).IsLast).Select(m => new
            {
                Member = m,
                Attribute = (LastOnInsertComposed)Attribute.GetCustomAttribute(m, typeof(LastOnInsertComposed))
            }).ToArray();
            if (array3 != null && array3.Length != 0)
            {
                var theLast = array3[0];
                ParameterExpression param = Expression.Parameter(typeof(Entity), "ent");
                MemberExpression memberExpression = Expression.Property(param, typeof(Entity).GetProperty(theLast.Member.Name));
                Expression<Func<Entity, long>> selector = Expression.Lambda<Func<Entity, long>>(Expression.Convert(memberExpression, typeof(long)), param);
                var array4 = (typeof(Model).GetMembers()).Where(m => Attribute.GetCustomAttribute(m, typeof(UseToCalculateMax)) != null && ((UseToCalculateMax)Attribute.GetCustomAttribute(m, typeof(UseToCalculateMax))).UseToCalculate && ((UseToCalculateMax)Attribute.GetCustomAttribute(m, typeof(UseToCalculateMax))).MaxProperty == theLast.Member.Name).Select(m => new
                {
                    Member = m,
                    Attribute = (UseToCalculateMax)Attribute.GetCustomAttribute(m, typeof(UseToCalculateMax)),
                    MemberExpression = Expression.Property(param, typeof(Entity).GetProperty(m.Name))
                }).ToArray();
                if (array4 != null && array4.Length != 0)
                {
                    long num = this._repositorio.DbSet.Where(Expression.Lambda<Func<Entity, bool>>(array4.Select((p, i) => Expression.Equal(p.MemberExpression, Expression.Convert(Expression.PropertyOrField(Expression.Constant(new
                    {
                        id = new PropertyAccessor(destination.GetType(), p.Member.Name).GetValue(destination)
                    }), "id"), p.MemberExpression.Type))).Aggregate(new Func<BinaryExpression, BinaryExpression, BinaryExpression>(Expression.AndAlso)), param)).Select(selector).Max();
                    long skip = theLast.Attribute.Skip;
                    objectAccesor.Set(theLast.Member.Name, Convert.ChangeType((num + skip), memberExpression.Type));
                }
                else
                {
                    long num = _repositorio.DbSet.Select(selector).Max();
                    long skip = theLast.Attribute.Skip;
                    objectAccesor.Set(theLast.Member.Name, Convert.ChangeType((num + skip), memberExpression.Type));
                }
            }
            AddDateTimeAtInsert customAttribute2 = (AddDateTimeAtInsert)Attribute.GetCustomAttribute(typeof(Model), typeof(AddDateTimeAtInsert));
            if (customAttribute2 != null)
                objectAccesor.Set(customAttribute2.DateTimeName, DateTime.Now);
            return destination;
        }
    }
}
