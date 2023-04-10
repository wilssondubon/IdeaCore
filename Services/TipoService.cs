using AutoMapper;
using DTOs;
using Entities;
using GICoreServices;
using IdeaCoreInterfaces.Hateoas;
using IdeaCoreInterfaces.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ITipoService
    { }

    public class TipoService : CommonService<Tipo, TipoDTO>, ITipoService
    {
        public TipoService(IUnitOfWork unitOfWork, IMapper mapper, IHateoasListWrapperService hateoasListWrapperService) : base(unitOfWork, mapper, hateoasListWrapperService) { 

        }
    }
}
