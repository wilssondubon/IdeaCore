using IdeaCoreInterfaces.Application.Response;
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
    public class HateoasListWrapper<Model>: IHateoasListWrapper<Model> where Model : class
    {
        public IEnumerable<Model>? data { get; set; }
        public IList<ILink>? links { get; set; }
        public IPagedResponse? paging { get; set; }
        public HateoasListWrapper(IEnumerable<Model> embed) {
            data = embed;
        }
    }
}
