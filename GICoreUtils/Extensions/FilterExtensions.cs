using IdeaCoreUtils.Accessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreUtils.Extensions
{
    /// <summary>
    /// extensiones para obtener diferentes valores referentes a una consulta
    /// </summary>
    public static class FilterExtensions
    {
        /// <summary>
        /// obtiene las propiedades de navegacion de un tipo de entidad
        /// </summary>
        /// <typeparam name="T">tipo de la entidad</typeparam>
        /// <param name="dbContext">extiende el dbcontext</param>
        /// <returns>devuelve las propiedades de navegacion desde la metadata de entity framework</returns>
        public static IEnumerable<INavigation> GetNavigationProperties<T>(this DbContext dbContext)
        {
            return dbContext.Model.FindEntityType(typeof(T)).GetNavigations();
        }
        /// <summary>
        /// obtiene las propiedades que conforman una llave primaria de un tipo de entidad
        /// </summary>
        /// <typeparam name="T">tipo de la entidad</typeparam>
        /// <param name="dbContext">extiende el dbcontext</param>
        /// <returns>devuelve las propiedades que conforman una llave primaria desde la metadata de entity framework</returns>
        public static IReadOnlyList<IProperty> GetPrimaryKeyProperties<T>(this DbContext dbContext)
        {
            return dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties;
        }
        /// <summary>
        /// crea un predicado lambda que incluye todas las propiedades que conforman un primary key
        /// </summary>
        /// <typeparam name="T">Tipo de la entidad</typeparam>
        /// <param name="dbContext">extiende un dbcontext</param>
        /// <param name="o">objeto del tipo de la entidad</param>
        /// <returns>predicado lambda</returns>
        public static Expression<Func<T, bool>> FilterByObjectWithPrimaryKeyPredicate<T>(this DbContext dbContext, object o)
        {
            IReadOnlyList<IProperty> primaryKeyProperties = dbContext.GetPrimaryKeyProperties<T>();
            ParameterExpression parameter = Expression.Parameter(typeof(T), "e");
            Func<IProperty, int, BinaryExpression> selector = (p, i) => Expression.Equal(Expression.Property(parameter, p.Name), Expression.Convert(Expression.PropertyOrField(Expression.Constant(new
            {
                id = Convert.ChangeType(new PropertyAccessor(o.GetType(), p.Name).GetValue(o), p.ClrType)
            }), "id"), p.ClrType));
            return Expression.Lambda<Func<T, bool>>(primaryKeyProperties.Select(selector).Aggregate(new Func<BinaryExpression, BinaryExpression, BinaryExpression>(Expression.AndAlso)), parameter);
        }
        /// <summary>
        /// crea un predicado lambda que incluye todas las propiedades que contenga el diccionario dentro sus Keys
        /// </summary>
        /// <typeparam name="T">tipo de la entidad</typeparam>
        /// <param name="dbContext">extiende el dbcontext</param>
        /// <param name="filter">diccionario de tipo nombre de la propiedad y valor de la propiedad</param>
        /// <returns>predicado lambda</returns>
        public static Expression<Func<T, bool>> FilterByKeyValueCollectionPredicate<T>(this DbContext dbContext, IDictionary<string, string> filter)
        {
            IEnumerable<IProperty> AllProperties = dbContext.Model.FindEntityType(typeof(T)).GetProperties();
            ParameterExpression parameter = Expression.Parameter(typeof(T), "e"); ;
            return Expression.Lambda<Func<T, bool>>(filter.Keys.Select(t => new
            {
                key = t,
                property = AllProperties.Where(p => p.Name.ToLower() == t.ToLower()).FirstOrDefault()
            }).Select(t => t.property).Where(t => t != null).Select((p, i) => Expression.Equal(Expression.Property(parameter, p.Name), Expression.Convert(Expression.PropertyOrField(Expression.Constant(
                new
                {
                    id = Convert.ChangeType(filter.Where(t => t.Key.ToLower() == p.Name.ToLower()).Select(t => t.Value).FirstOrDefault(), p.ClrType)
                }), "id"), p.ClrType))).Aggregate(new Func<BinaryExpression, BinaryExpression, BinaryExpression>(Expression.AndAlso)), parameter);
        }
        /// <summary>
        /// crea un predicado lambda que incluye todas las propiedades que contenga los valores de tipo id
        /// </summary>
        /// <typeparam name="T">tipo de la entidad</typeparam>
        /// <param name="dbContext">extiende el dbcontext</param>
        /// <param name="id">valores de tipo id {primary key}</param>
        /// <returns>predicado lambda</returns>
        public static Expression<Func<T, bool>> FilterByPrimaryKeyPredicate<T>(this DbContext dbContext, params object[] id)
        {
            IReadOnlyList<IProperty> primaryKeyProperties = dbContext.GetPrimaryKeyProperties<T>();
            ParameterExpression parameter = Expression.Parameter(typeof(T), "e");
            Func<IProperty, int, BinaryExpression> selector = (p, i) => Expression.Equal(Expression.Property(parameter, p.Name), Expression.Convert(Expression.PropertyOrField(Expression.Constant(new
            {
                id = Convert.ChangeType(id[i], p.ClrType)
            }), nameof(id)), p.ClrType));
            return Expression.Lambda<Func<T, bool>>(primaryKeyProperties.Select(selector).Aggregate(new Func<BinaryExpression, BinaryExpression, BinaryExpression>(Expression.AndAlso)), parameter);
        }
        /// <summary>
        /// crea un predicado lambda que incluye todas las propiedades que contenga los valores de tipo id
        /// </summary>
        /// <typeparam name="T">tipo de la entidad</typeparam>
        /// <param name="context">extiende el dbcontext</param>
        /// <returns>predicado lambda</returns>
        public static Expression<Func<T, object[]>> GetPrimaryKeyExpression<T>(this DbContext context)
        {
            IReadOnlyList<IProperty> primaryKeyProperties = context.GetPrimaryKeyProperties<T>();
            ParameterExpression parameter = Expression.Parameter(typeof(T), "e");
            Func<IProperty, int, UnaryExpression> selector = (p, i) => Expression.Convert(Expression.Property(parameter, p.Name), typeof(object));
            return Expression.Lambda<Func<T, object[]>>(Expression.NewArrayInit(typeof(object), primaryKeyProperties.Select(selector).ToArray()), parameter);
        }
        /// <summary>
        /// filtra un objeto por sus propiedades de tipo id {llave principal}
        /// </summary>
        /// <typeparam name="TEntity">tipo de la entidad</typeparam>
        /// <param name="dbSet">extiende el dbset</param>
        /// <param name="context">el dbcontext en el que se asigna el dbset</param>
        /// <param name="id">propiedades de tipo id</param>
        /// <returns>coleccion de consulta de la entidad</returns>
        public static IQueryable<TEntity> FilterByPrimaryKey<TEntity>(this DbSet<TEntity> dbSet, DbContext context, params object[] id) where TEntity : class
        {
            return dbSet.AsQueryable().FilterByPrimaryKey(context, id);
        }

    }
}
