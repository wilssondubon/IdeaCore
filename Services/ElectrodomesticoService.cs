using AutoMapper;
using DTOs;
using Entities;
using GICoreServices;
using IdeaCoreInterfaces.Hateoas;
using IdeaCoreInterfaces.Infraestructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IElectrodomesticoService
    { }
    public class ElectrodomesticoService : CommonService<ElectrodomesticoDTO, Electrodomestico>, IElectrodomesticoService
    {
        public ElectrodomesticoService(IUnitOfWork unitOfWork, IMapper mapper, IHateoasListWrapperService hateoasListWrapperService) : base(unitOfWork, mapper, hateoasListWrapperService)
        {
            setNavigationsProperties(t => t.Include(i => i.IdTipoNavigation));
        }
    }
}
