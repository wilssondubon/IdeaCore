﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreInterfaces.Aplication.Services.Common.Response
{
    public interface IResponse
    {
        string[] Errors { get; }
        string Message { get; }
        short Status { get; }
        bool Succeeded { get; }
    }
}
