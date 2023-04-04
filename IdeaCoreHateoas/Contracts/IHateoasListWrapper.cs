using IdeaCoreInterfaces.Hateoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreHateoas.Contracts
{
    public interface IHateoasListWrapper
    {
        object data { get; }
        List<ILink>? links { get; set; }
    }
}
