using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IdeaCoreInterfaces.Common;

namespace IdeaCoreInfraestructure.Extensions
{
    /// <summary>
    /// extensiones para IQueryable
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// filtra por primary key
        /// </summary>
        /// <typeparam name="TEntity">tipo de la entidad</typeparam>
        /// <param name="queryable">extiende IQueryable</param>
        /// <param name="context">dbcontext donde se trabajara</param>
        /// <param name="id">propiedades de primary key</param>
        /// <returns>coleccion de consulta</returns>
        public static IQueryable<TEntity> FilterByPrimaryKey<TEntity>(this IQueryable<TEntity> queryable, DbContext context, object[] id)
        {
            return queryable.Where(context.FilterByPrimaryKeyPredicate<TEntity>(id));
        }
        /// <summary>
        /// filtra un objeto por su llave primaria teniendo el objeto completo
        /// </summary>
        /// <typeparam name="TEntity">tipo de la entidad</typeparam>
        /// <param name="queryable">extiende IQueryable</param>
        /// <param name="context">dbcontext donde se trabajara</param>
        /// <param name="o">objeto completo</param>
        /// <returns>coleccion de consulta</returns>
        public static IQueryable<TEntity> FilterByObjectWithPrimaryKey<TEntity>(this IQueryable<TEntity> queryable, DbContext context, object o)
        {
            return queryable.Where(context.FilterByObjectWithPrimaryKeyPredicate<TEntity>(o));
        }
        /// <summary>
        /// filtra un objeto basado en los valores de un diccionario de tipo nombre de la propiedad y valor de la propiedad
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="queryable">extiende IQueryable</param>
        /// <param name="context">dbcontext donde se trabajara</param>
        /// <param name="filter">diccionario de tipo nombre de la propiedad y valor de la propiedad</param>
        /// <returns>coleccion de consulta</returns>
        public static IQueryable<TEntity> FilterByKeyValueCollection<TEntity>(this IQueryable<TEntity> queryable, DbContext context, IDictionary<string, string> filter)
        {
            Expression<Func<TEntity, bool>> predicate = context.FilterByKeyValueCollectionPredicate<TEntity>(filter);
            if (predicate != null)
                return queryable.Where(predicate);
            return queryable.Select(t => t);
        }
        /// <summary>
        /// agrega las propiedades de navegacion a una consulta
        /// </summary>
        /// <typeparam name="TEntity">tipo de la entidad</typeparam>
        /// <param name="source">extiende IQueryable</param>
        /// <param name="include">funciones de tipo include</param>
        /// <returns>coleccion de consulta</returns>
        public static IQueryable<TEntity> IncludeNavigations<TEntity>(this IQueryable<TEntity> source, params Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] include)
        {
            IQueryable<TEntity> query = source;
            if (include is not null)
            {
                foreach (var i in include)
                {
                    query = i(query);
                }
            }
            return query;
        }
    }
}
