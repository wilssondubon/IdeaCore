using IdeaCoreInterfaces.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreUtils.Models
{
    /// <summary>
    /// clase que define una respuesta para una lista paginada
    /// </summary>
    public class PagedResponse : IPagedResponse
    {
        /// <summary>
        /// numero de pagina actual
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// cantidad de registros en una pagina
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// total de paginas
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// total de registros en las paginas
        /// </summary>
        public int TotalRecords { get; set; }
    }
}
