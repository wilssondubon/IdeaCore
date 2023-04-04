using IdeaCoreInterfaces.Application.Response;
using IdeaCoreInterfaces.Hateoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInterfaces.Hateoas
{
    public interface IHateoasListWrapper<Model> where Model : class
    {
        IEnumerable<Model>? data { get; set; }
        IList<ILink>? links { get; set; }
        IPagedResponse? paging { get; set; }
    }
}
