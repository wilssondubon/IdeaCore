using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeaCoreApplication.Models;
using IdeaCoreInterfaces.Infraestructure;
using IdeaCoreInterfaces.Common;
using IdeaCoreInterfaces.Hateoas;
using IdeaCoreInterfaces.Application;
using IdeaCoreInterfaces.Application.Response;
using IdeaCoreModels;

namespace GICoreServices
{
    /// <summary>
    /// servicio de uso comun con funcionalidades de uso comun
    /// </summary>
    /// <typeparam name="Model">tipo de modelo que retornaran las respueta del servicio</typeparam>
    /// <typeparam name="Entity">tipo de la entidad que se requerira consultar al repositorio</typeparam>
    public partial class CommonService<Model, Entity> : ICRUDService<Model, Entity> where Model : class where Entity : class
    {
        /// <summary>
        /// unidad de trabajo de la infraestructura de la aplicacion
        /// </summary>
        protected readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// automapper
        /// </summary>
        protected readonly IMapper _mapper;
        /// <summary>
        /// repositorio principal del servicio
        /// </summary>
        protected IGenericRepository<Entity>? _repositorio;
        /// <summary>
        /// servicio que se conecta al httpcontext de la solicitud
        /// </summary>
        protected readonly IHateoasListWrapperService _hateoasListWrapperService;
        /// <summary>
        /// funciones include para incluirse en cada solicitud de elementos al repositorio
        /// </summary>
        protected Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>[]? _includesPaths;
        /// <summary>
        /// estas propiedades de navegaciones se incluiran en cada entidad retraida
        /// </summary>
        /// <param name="includeNavigationProperties">Array de funciones Include</param>
        protected void setNavigationsProperties(params Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>[] includeNavigationProperties)
        {
            _includesPaths = includeNavigationProperties;
        }
        /// <summary>
        /// inicializa un servicio de uso comun
        /// </summary>
        /// <param name="unitOfWork">unidad de trabajo de la infraestructura de la aplicacion</param>
        /// <param name="mapper">automapper</param>
        /// <param name="uriService">servicio que se conecta al httpcontext de la solicitud</param>
        public CommonService(IUnitOfWork unitOfWork, IMapper mapper, IHateoasListWrapperService hateoasListWrapperService, Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>[]? includeNavigationProperties = null)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._hateoasListWrapperService = hateoasListWrapperService;
            this._repositorio = unitOfWork.AddRepository<Entity>(unitOfWork.Repositories, includeNavigationProperties);
        }
        /// <summary>
        /// modelo de respuesta basado en un modelo
        /// </summary>
        public class ServiceResponse : Response<Model>, IServiceResponse<Model>
        {
            /// <summary>
            /// el modelo en el que se basa la respuesta
            /// </summary>
            //public new Model data { get; set; }
            /// <summary>
            /// crea una nueva respuesta con un tracker
            /// </summary>
            /// <param name="o">modelo en el que se basara la respuesta</param>
            /// <param name="t">tracker para la respuesta</param>
            public ServiceResponse(Model o, ITrackerResponse t)
              : base(o, t)
            {
            }
            /// <summary>
            /// crea una respueta basada en una entidad que mapeara a un modelo
            /// </summary>
            /// <param name="o">entidad que se mapeara a un modelo</param>
            /// <param name="t">tracker para la respuesta</param>
            /// <param name="mapper">mapper para mapear la entidad hacia el modelo</param>
            public ServiceResponse(Entity o, ITrackerResponse t, IMapper mapper)
              : base(mapper.Map<Model>(o), t)
            {
            }
            /// <summary>
            /// respuesta de error
            /// </summary>
            /// <param name="error">titulo del error</param>
            /// <param name="errors">lista de errores sucedidos</param>
            /// <param name="succeded">estado de la solicitud</param>
            //public ServiceResponse(string error, string[] errors, bool succeded)
            //  : base(error, errors, succeded)
            //{
            //}
            /// <summary>
            /// respuesta de error
            /// </summary>
            /// <param name="code">codigo de error</param>
            /// <param name="error">titulo del error</param>
            /// <param name="errors">lista de errores sucedidos</param>
            /// <param name="succeded">estado de la solicitud</param>
            public ServiceResponse(short code, string error, string[] errors, bool succeded)
              : base(code, error, errors, succeded)
            {
            }
        }
        /// <summary>
        /// modelo de respuesta basado en una lista de modelos
        /// </summary>
        public class ServiceResponseList : Response<IEnumerable<Model>>, IServiceResponseList<Model>
        {
            private readonly int _totalRecords;

            private readonly IHateoasListWrapperService _hateoasListWrapperService;
            /// <summary>
            /// opciones de paginado
            /// </summary>
            public IPagedResponse? paging { get; set; }
            /// <summary>
            /// links de la lista y la paginacion
            /// </summary>
            public IList<ILink>? links { get; set; }
            /// <summary>
            /// crea una respueta basada en una lista de entidades que mapearan a una lista de modelos
            /// </summary>
            /// <param name="o">lista de entidades</param>
            /// <param name="t">tracker de la respuesta</param>
            /// <param name="mapper">mapper que se encargara de transforma de entidades a modelos</param>
            /// <param name="uriService">servicio que accede al httpcontext de la peticion</param>
            /// <param name="totalRecords">total de elementos en la respuesta</param>
            public ServiceResponseList(IEnumerable<Entity> o, ITrackerResponse t, IMapper mapper,  IHateoasListWrapperService hateoasListWrapperService, int totalRecords)
              : base(mapper.Map<IEnumerable<Model>>(o), t)
            {
                _hateoasListWrapperService = hateoasListWrapperService;
                _totalRecords = totalRecords;

                if (!_hateoasListWrapperService.isHateoasRequest())
                    return;

                var hateoasListWrapper = _hateoasListWrapperService.Wrap(data);
                links = hateoasListWrapper.links;
            }
            /// <summary>
            /// crea una respueta basada en una lista de entidades que mapearan a una lista de modelos
            /// </summary>
            /// <param name="o">lista de entidades</param>
            /// <param name="t">tracker de la respuesta</param>
            /// <param name="mapper">mapper que se encargara de transforma de entidades a modelos</param>
            /// <param name="uriService">servicio que accede al httpcontext de la peticion</param>
            public ServiceResponseList(IPagedReadOnlyList<Entity> o, ITrackerResponse t, IMapper mapper, IHateoasListWrapperService hateoasListWrapperService)
              : base(mapper.Map<IEnumerable<Model>>(o), t)
            {
                _hateoasListWrapperService = hateoasListWrapperService;
                _totalRecords = o.TotalRecords;

                if (!_hateoasListWrapperService.isHateoasRequest())
                    return;

                var hateoasListWrapper = _hateoasListWrapperService.Wrap(data);
                hateoasListWrapper = _hateoasListWrapperService.AddPagination(hateoasListWrapper, _totalRecords, new FilterQueryParams(o.PageNumber, o.PageSize));
                links = hateoasListWrapper.links;
                paging = hateoasListWrapper.paging;
            }
            /// <summary>
            /// respuesta de error
            /// </summary>
            /// <param name="error">titulo del error</param>
            /// <param name="errors">listado de errores sucedidos</param>
            /// <param name="succeded">estado de la solicitud</param>
            //public ServiceResponseList(string error, string[] errors, bool succeded)
            //  : base(error, errors, succeded)
            //{
            //}
            /// <summary>
            /// respuesta de error
            /// </summary>
            /// <param name="code">codigo del error</param>
            /// <param name="error">titulo del error</param>
            /// <param name="errors">listado de errores sucedidos</param>
            /// <param name="succeded">estado de la solicitud</param>
            public ServiceResponseList(short code, string error, string[] errors, bool succeded)
              : base(code, error, errors, succeded)
            {
            }
            public ServiceResponseList() : base() { }
        }
    }
}
