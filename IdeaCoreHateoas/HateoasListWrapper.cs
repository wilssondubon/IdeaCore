using IdeaCoreHateoas.Contracts;
using IdeaCoreInterfaces.Hateoas;
using IdeaCoreModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreHateoas
{
    public class HateoasListWrapper: IHateoasListWrapper
    {
        public object data { get; }
        public List<ILink>? links { get; set; }
        public HateoasListWrapper(object embed) {
            data = embed;
        }
    }
}
