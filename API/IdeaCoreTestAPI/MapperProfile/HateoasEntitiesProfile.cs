using DTOs;
using Entities;
using IdeaCoreHateoas;
using IdeaCoreInterfaces.Hateoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreTestAPI.MapperProfile
{
    public class HateoasEntitiesProfile : HateoasMapperProfile
    {
        public HateoasEntitiesProfile() { }

        public HateoasEntitiesProfile(IHateoasProfileParameters parameters): base(parameters) {

            CreateHateoasMap<Tipo, TipoDTO>()
                .ReverseMap();

            CreateHateoasMap<Electrodomestico, ElectrodomesticoDTO>()
                .ForMember(dest=> dest.Tipo, act=>act.MapFrom(src=> src.IdTipoNavigation))
                .ReverseMap();
        }
    }
}
