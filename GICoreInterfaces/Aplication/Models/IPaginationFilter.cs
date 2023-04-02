using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInterfaces.Aplication.Models
{
    /// <summary>
    /// interfaz para definir los valores de paginacion
    /// </summary>
    public interface IPaginationFilter
    {
        /// <summary>
        /// numero de pagina que se quiere consultar
        /// </summary>
        int PageNumber { get; set; }
        /// <summary>
        /// cantidad de registros que se contienen en la pagina
        /// </summary>
        int PageSize { get; set; }
    }
}
