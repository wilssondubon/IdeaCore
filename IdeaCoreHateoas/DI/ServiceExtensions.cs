using IdeaCoreHateoas.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace IdeaCoreHateoas.DI
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddHateoasMapper<TProfile, THateoasAPI>(this IServiceCollection services) where TProfile : HateoasMapperProfile, new() where THateoasAPI : IHateoasAPI, new()
        {
            Func<IServiceProvider, TProfile> newHateoasMapperProfile = (serviceProvider) =>
            {
                IEndpointToModelRelation relations = new EndpointToModelRelation(new THateoasAPI().Get());
                IHttpContextAccessor accesor = serviceProvider.GetService<IHttpContextAccessor>();
                LinkGenerator generator = serviceProvider.GetService<LinkGenerator>();
                HateoasProfileParameters parameters = new HateoasProfileParameters(relations, accesor, generator);

                Type type = typeof(TProfile);
                ConstructorInfo ctor = type.GetConstructor(new[] { typeof(HateoasProfileParameters) });
                return (TProfile)ctor.Invoke(new object[] { parameters });
            };


            services.AddAutoMapper((serviceProvider, mapperConfiguration) => mapperConfiguration.AddProfile(
                newHateoasMapperProfile(serviceProvider)),
                 typeof(TProfile).Assembly);

            return services;
        }
    }
}
