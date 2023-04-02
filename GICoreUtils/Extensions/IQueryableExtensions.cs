using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IdeaCoreModels.FilterQueryString;
using IdeaCoreUtils.Extensions;
using IdeaCoreModels.EnumerableClasses;
using IdeaCoreInterfaces.Aplication.Models;

namespace IdeaCoreUtils.Extensions
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
        /// <summary>
        /// Ejecuta un query sobre una instruccion linq basado en un string para ordenar el resultado
        /// </summary>
        /// <typeparam name="TEntity">tipo de la entidad</typeparam>
        /// <param name="source">extiende IQueryable</param>
        /// <param name="orderByQueryString">string con las propiedades para ordenar el queryable</param>
        /// <returns>coleccion de consultas</returns>
        public static IQueryable<TEntity> ApplySort<TEntity>(this IQueryable<TEntity> source, string orderByQueryString)
        {
            if (!source.Any())
                return source;
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source;
            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var parameter = Expression.Parameter(typeof(TEntity), "p");
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Trim().Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                var sortingOrder = param.ToLower().EndsWith(" desc") ? "OrderByDescending" : "OrderBy";
                var propertyReference = Expression.Property(parameter, objectProperty);
                var sortExpression = Expression.Call(
                 typeof(Queryable),
                 sortingOrder,
                 new Type[] { typeof(TEntity), objectProperty.PropertyType },
                 source.Expression, Expression.Quote(Expression.Lambda(Expression.MakeMemberAccess(parameter, objectProperty), parameter)));
                source = source.Provider.CreateQuery<TEntity>(sortExpression) as IOrderedQueryable<TEntity>;
            }
            return source;
        }
        public static async Task<PagedList<TEntity>> ToListAsync<TEntity>(this IQueryable<TEntity> source, IPaginationFilter paginationFilter)
        {
            List<TEntity> list = new List<TEntity>();
            var count = await source.CountAsync();

            if (paginationFilter == null)
                paginationFilter = new PaginationFilter(1, count);

            if (count > paginationFilter.PageSize)
                list = await source.Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize).Take(paginationFilter.PageSize).ToListAsync();
            else
                list = await source.ToListAsync();

            return new PagedList<TEntity>(list, count, paginationFilter.PageNumber, paginationFilter.PageSize); ;
        }
    }
}
