using IdeaCoreInterfaces.Common;
using IdeaCoreUtils.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreUtils.Extensions
{
    public static class IQueryableExtensions
    {
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
            var count = source.Count();

            if (paginationFilter == null)
                paginationFilter = new PaginationFilter(1, count);

            if (count > paginationFilter.PageSize)
                list = await Task.FromResult(source.Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize).Take(paginationFilter.PageSize).ToList());
            else
                list = await Task.FromResult(source.ToList());

            return new PagedList<TEntity>(list, count, paginationFilter.PageNumber, paginationFilter.PageSize); ;
        }
    }
}
