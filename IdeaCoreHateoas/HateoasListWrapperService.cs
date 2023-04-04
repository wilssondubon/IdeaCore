using IdeaCoreHateoas.Contracts;
using IdeaCoreHateoas.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreHateoas
{
    public class HateoasListWrapperService : IHateoasListWrapperService
    {
        IHttpContextAccessor _httpContextAccessor;

        public HateoasListWrapperService(IHttpContextAccessor accesor)
        {
            _httpContextAccessor = accesor;
        }

        public IHateoasListWrapper Wrap(object embed)
        {
            return new HateoasListWrapper(embed).AddSelfLink(_httpContextAccessor.HttpContext);
        }
    }
}
