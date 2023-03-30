﻿using GICoreInterfaces.Aplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreModels.FilterQueryString
{
    /// <summary>
    /// clase para definir los valores de paginacion
    /// </summary>
    public class PaginationFilter : IPaginationFilter
    {
        /// <summary>
        /// numero de pagina que se quiere consultar
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// cantidad de registros que se contienen en la pagina
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// inicializa los valores de paginacion
        /// PageNumber = 1
        /// PageSize = 50
        /// </summary>
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 50;
        }
        /// <summary>
        /// inicializa los valores de paginacion
        /// </summary>
        /// <param name="pageNumber">numero de pagina que se quiere consultar</param>
        /// <param name="pageSize">cantidad de registros que se contienen en la pagina</param>
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize;
        }
    }
}
