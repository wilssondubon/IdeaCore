using IdeaCoreInterfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreModels
{
    /// <summary>
    /// Parametros completos para paginacion y ordenamiento
    /// </summary>
    public class FilterQueryParams : PaginationFilter, IFilterQueryParams
    {
        /// <summary>
        /// los nombres de los campos por los que se ordenara la respuesta y su orden --desc-- --asc--
        /// </summary>
        public string OrderBy { get; set; } = string.Empty;
        public FilterQueryParams() : base()
        {
            OrderBy = "";
        }
        /// <summary>
        /// inicializa los valores del filtro
        /// </summary>
        /// <param name="pageNumber">numero de pagina que se quiere consultar</param>
        /// <param name="pageSize">cantidad de registros que se contienen en la pagina</param>
        public FilterQueryParams(int pageNumber, int pageSize) : base(pageNumber, pageSize)
        {
        }
        /// <summary>
        /// inicializa los valores del filtro
        /// </summary>
        /// <param name="pageNumber">numero de pagina que se quiere consultar</param>
        /// <param name="pageSize">cantidad de registros que se contienen en la pagina</param>
        /// <param name="orderby">campos sobre los que se ordenara el resultado</param>
        public FilterQueryParams(int pageNumber, int pageSize, string orderby) : base(pageNumber, pageSize)
        {
            OrderBy = orderby;
        }
    }
}
