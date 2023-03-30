using AutoMapper;
using GICoreInterfaces.Aplication.Models.Hateoas;
using GICoreInterfaces.Aplication.Services.Common;
using GICoreInterfaces.Presentation.API.Hateoas;
using GICoreUtils.Hateoas;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace GICoreUtils.Hateoas
{
    /// <summary>
    /// mappping action de automapper basada en una entidad y un modelo
    /// la clase esta hecha agregar los links de hateos basados en las relaciones
    /// entre modelos y descripciones de endpoints despues de mapear de una entidad a un modelo
    /// </summary>
    /// <typeparam name="TEntity">tipo de la entidad</typeparam>
    /// <typeparam name="TModel">tipo del modelo</typeparam>
    public class hateosMapperAction<TEntity, TModel> : IMappingAction<TEntity, TModel> where TEntity : class where TModel : class, ILinks
    {
        /// <summary>
        /// servicio que accede al httpcontext de una solicitud
        /// </summary>
        protected readonly IUriService _uriService;
        /// <summary>
        /// coleccion de relaciones entre modelos y descripciones de endpoints
        /// </summary>
        protected readonly IEndpointToModelRelation _endpoints;
        /// <summary>
        /// generador de links hateos
        /// </summary>
        protected LinkGenerator _linkGenerator;
        /// <summary>
        /// inicializa el mapper action
        /// </summary>
        /// <param name="uriService">servicio que accede al httpcontext de una solicitud</param>
        /// <param name="endpoints">coleccion de relaciones entre modelos y descripciones de endpoints</param>
        /// <param name="linkGenerator">generador de links hateos</param>
        public hateosMapperAction(
            IUriService uriService, IEndpointToModelRelation endpoints, LinkGenerator linkGenerator
            )
        {
            _uriService = uriService;
            _endpoints = endpoints;
            _linkGenerator = linkGenerator;
        }
        /// <summary>
        /// genera los links hateos
        /// </summary>
        /// <param name="source">entidad</param>
        /// <param name="destination">modelo</param>
        /// <param name="context">contexto de la accion de automapper</param>
        public virtual void Process(TEntity source, TModel destination, ResolutionContext context)
        {
            Process(destination, _endpoints, _uriService, _linkGenerator);
        }

        public static void Process(TModel destination, IEndpointToModelRelation endpoints, IUriService uriService, LinkGenerator linkGenerator)
        {
            IEndpointDescriptorList modelEndpoints = endpoints.GetEndpointsForModel(typeof(TModel));

            if (!uriService.isHateoasRequest())
                return;

            if (destination.links == null)
                destination.links = new List<ILink>();

            if (modelEndpoints != null && modelEndpoints.Count > 0)
            {
                foreach (IEndpointDescriptor endpoint in modelEndpoints)
                {
                    string uriString = linkGenerator.GetUriByAction(uriService.httpContext, endpoint.Accion, endpoint.Controlador, values: endpoint.Valores != null ? endpoint.Valores(destination) : null);
                    Uri uri = new Uri(uriString);
                    Link newLink = new Link(uri, endpoint.Relacion, endpoint.Metodo);
                    destination.links.Add(newLink);
                }
            }
        }
    }
}
