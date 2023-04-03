using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreHateoas.Contracts
{
    public interface IHateoasAPI
    {
        IEndpointToModelRelation Get();
    }
}
