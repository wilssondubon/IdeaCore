using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreInterfaces.Aplication.Models
{
    /// <summary>
    /// Representa una lista de solo lectura con valores de paginacion
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedReadOnlyList<out T> : IReadOnlyList<T>
    {
        /// <summary>
        /// numero de pagina
        /// </summary>
        int PageNumber { get; set; }
        /// <summary>
        /// total de paginas
        /// </summary>
        int TotalPages { get; set; }
        /// <summary>
        /// total de registros por pagina
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// total de registros
        /// </summary>
        int TotalRecords { get; set; }
    }
}
