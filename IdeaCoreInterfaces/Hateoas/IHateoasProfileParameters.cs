using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInterfaces.Hateoas
{
    public interface IHateoasProfileParameters
    {
        IEndpointToModelRelation Endpoints { get; set; }
        IHttpContextAccessor HttpContextAccessor { get; set; }
        LinkGenerator LinkGenerator { get; set; }
    }
}
