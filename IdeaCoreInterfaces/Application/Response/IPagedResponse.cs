using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInterfaces.Application.Response
{
    /// <summary>
    /// interfaz que define una respuesta para una lista paginada
    /// </summary>
    public interface IPagedResponse
    {
        /// <summary>
        /// numero de pagina actual
        /// </summary>
        int PageNumber { get; set; }
        /// <summary>
        /// cantidad de registros en una pagina
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// total de paginas
        /// </summary>
        int TotalPages { get; set; }
        /// <summary>
        /// total de registros en las paginas
        /// </summary>
        int TotalRecords { get; set; }
    }
}
