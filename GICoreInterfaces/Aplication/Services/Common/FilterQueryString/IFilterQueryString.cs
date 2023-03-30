﻿using GICoreInterfaces.Aplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreInterfaces.Aplication.Services.Common.FilterQueryString
{
    public interface IFilterQueryParams : IPaginationFilter
    {
        string OrderBy { get; set; }
    }
}