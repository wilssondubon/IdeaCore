using GICoreInterfaces.Aplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreModels.EnumerableClasses
{
    public class PagedList<T> : List<T>, IPagedReadOnlyList<T>
    {
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public PagedList() : base() { }
        public PagedList(IEnumerable<T> currentPage, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = Convert.ToInt32(Math.Ceiling((double)count / pageSize));
            PageSize = pageSize;
            TotalRecords = count;
            AddRange(currentPage);
        }
    }
}
