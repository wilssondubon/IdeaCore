using IdeaCoreInterfaces.Hateoas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreHateoas
{
    public class HateoasProfileParameters : IHateoasProfileParameters
    {
        public IEndpointToModelRelation Endpoints { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public LinkGenerator LinkGenerator { get; set; }

        public HateoasProfileParameters(IEndpointToModelRelation endpoints, IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            Endpoints = endpoints;
            HttpContextAccessor = httpContextAccessor;
            LinkGenerator = linkGenerator;
        }

        public HateoasProfileParameters() { }
    }
}
