using DTOs;
using IdeaCoreHateoas;
using IdeaCoreInterfaces.Hateoas;

namespace IdeaCoreTestAPI.hateoas
{
    public class HateoasAPI : IHateoasAPI
    {
        public IEndpointToModelRelation Get()
        {
            IEndpointToModelRelation relations = new EndpointToModelRelation();

            relations = relations.RegisterModel(typeof(TipoDTO))
                .AddEndPoint("self", "GetById", "Tipo", "GET", (model) =>
                {
                    TipoDTO o = (TipoDTO)model;
                    return new
                    {
                        IdTipo = o.IdTipo
                    };
                })
                .Build();


            return relations;
        }
    }
}
