using IdeaCoreInterfaces.Aplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreUtils.Extensions
{
    /// <summary>
    /// extensiones para filtros de paginacion
    /// </summary>
    public static class PaginationExtensions
    {
        /// <summary>
        /// obtiene una lista paginada
        /// </summary>
        /// <typeparam name="T">tipo de la entidad</typeparam>
        /// <param name="paginationFilter">extiende IPaginationFilter</param>
        /// <param name="queryable">lista sin paginar</param>
        /// <returns>Lista Paginada</returns>
        public static List<T> GetPaginatedList<T>(this IPaginationFilter paginationFilter, IEnumerable<T> queryable)
        {
            return paginationFilter != null ? queryable.Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize).Take(paginationFilter.PageSize).ToList() : queryable.ToList();
        }
    }
}
