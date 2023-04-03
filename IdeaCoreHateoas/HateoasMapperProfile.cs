using AutoMapper;
using IdeaCoreInterfaces.Hateoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreHateoas
{
    public class HateoasMapperProfile : Profile
    {
        internal IHateoasProfileParameters HateoasParameters { get; set; }

        public HateoasMapperProfile() { }

        public HateoasMapperProfile(IHateoasProfileParameters parameters)
        {
            HateoasParameters = parameters;
        }

        public IMappingExpression<TSource, TDestination> CreateHateoasMap<TSource, TDestination>() where TDestination : class, ILinks, new()
        {
            return CreateMap<TSource, TDestination>()
                .ConstructUsing(x => new TDestination() { HateoasParameters = HateoasParameters })
                .AfterMap((src, dest) => dest.setLinks());
        }
    }
}
