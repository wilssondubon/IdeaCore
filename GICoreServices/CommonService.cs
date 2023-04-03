using AutoMapper;
using IdeaCoreModels.FilterQueryString;
using IdeaCoreModels.Response;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeaCoreInterfaces.Aplication.Services.Common;
using IdeaCoreInterfaces.Aplication.Services.Common.Response;
using IdeaCoreInterfaces.Infraestructure.UnitOfWork;
using IdeaCoreInterfaces.Infraestructure.Repository;
using IdeaCoreInterfaces.Aplication.Models;
using IdeaCoreHateoas.Contracts;
using IdeaCoreHateoas;

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
        protected readonly IUriService _uriService;
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
        public CommonService(IUnitOfWork unitOfWork, IMapper mapper, IUriService uriService, Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>[]? includeNavigationProperties = null)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._uriService = uriService;
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
            public Model data => this.Item1;
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

            private readonly IUriService _uriService;
            /// <summary>
            /// lista de modelos en lo que se basa la respuesta
            /// </summary>
            public IEnumerable<Model> data => this.Item1;
            /// <summary>
            /// opciones de paginado
            /// </summary>
            public IPagedResponse? paging { get; set; }
            /// <summary>
            /// links de la lista y la paginacion
            /// </summary>
            public IList<ILink>? links { get; set; }
            /// <summary>
            /// asigna los valores de paginacion y crea los links correspondientes
            /// </summary>
            /// <param name="paginationFilter">opciones de paginacion</param>
            /// <returns>respuesta paginada</returns>
            public IServiceResponseList<Model> SetPagination(IPaginationFilter paginationFilter = null)
            {
                IServiceResponseList<Model> response = this;

                if (paginationFilter != null && _totalRecords > paginationFilter.PageSize)
                {
                    int roundedTotalPages = Convert.ToInt32(Math.Ceiling((double)_totalRecords / paginationFilter.PageSize));

                    response.paging = new PagedResponse
                    {
                        PageNumber = paginationFilter.PageNumber,
                        PageSize = paginationFilter.PageSize,
                        TotalPages = roundedTotalPages,
                        TotalRecords = _totalRecords,
                    };

                    _uriService.httpContext.Response.Headers.Add("X-Paging-PageNo", response.paging.PageNumber.ToString());
                    _uriService.httpContext.Response.Headers.Add("X-Paging-PageSize", response.paging.PageSize.ToString());
                    _uriService.httpContext.Response.Headers.Add("X-Paging-PageCount", response.paging.TotalPages.ToString());
                    _uriService.httpContext.Response.Headers.Add("X-Paging-TotalRecordCount", response.paging.TotalRecords.ToString());

                    if (response.links == null)
                        response.links = new List<ILink>();


                    response.links.Add(
                        new Link(_uriService.GetPaginationPageUri(new PaginationFilter(1, response.paging.PageSize)),
                        "first",
                        "GET")
                    );

                    if (response.paging.PageNumber - 1 >= 1 && response.paging.PageNumber <= response.paging.TotalPages)
                    {
                        response.links.Add(
                            new Link(_uriService.GetPaginationPageUri(new PaginationFilter(response.paging.PageNumber - 1, response.paging.PageSize)),
                            "prev",
                            "GET")
                        );
                    }

                    if (response.paging.PageNumber >= 1 && response.paging.PageNumber < response.paging.TotalPages)
                    {
                        response.links.Add(
                            new Link(_uriService.GetPaginationPageUri(new PaginationFilter(response.paging.PageNumber + 1, response.paging.PageSize)),
                            "next",
                            "GET")
                        );
                    }

                    response.links.Add(
                        new Link(_uriService.GetPaginationPageUri(new PaginationFilter(response.paging.TotalPages, response.paging.PageSize)),
                        "last",
                        "GET")
                    );
                }
                else
                {
                    response.paging = null;
                }

                return response;
            }
            /// <summary>
            /// crea una respueta basada en una lista de entidades que mapearan a una lista de modelos
            /// </summary>
            /// <param name="o">lista de entidades</param>
            /// <param name="t">tracker de la respuesta</param>
            /// <param name="mapper">mapper que se encargara de transforma de entidades a modelos</param>
            /// <param name="uriService">servicio que accede al httpcontext de la peticion</param>
            /// <param name="totalRecords">total de elementos en la respuesta</param>
            public ServiceResponseList(IEnumerable<Entity> o, ITrackerResponse t, IMapper mapper, IUriService uriService, int totalRecords)
              : base(mapper.Map<IEnumerable<Model>>(o), t)
            {
                _uriService = uriService;
                _totalRecords = totalRecords;

                if (!uriService.isHateoasRequest())
                    return;

                if (links == null)
                    links = new List<ILink>();

                links.Add(
                       new Link(new Uri(_uriService.selfUriWithQuery),
                       "self",
                       "GET")
                   );
            }
            /// <summary>
            /// crea una respueta basada en una lista de entidades que mapearan a una lista de modelos
            /// </summary>
            /// <param name="o">lista de entidades</param>
            /// <param name="t">tracker de la respuesta</param>
            /// <param name="mapper">mapper que se encargara de transforma de entidades a modelos</param>
            /// <param name="uriService">servicio que accede al httpcontext de la peticion</param>
            public ServiceResponseList(IPagedReadOnlyList<Entity> o, ITrackerResponse t, IMapper mapper, IUriService uriService)
              : base(mapper.Map<IEnumerable<Model>>(o), t)
            {
                _uriService = uriService;
                _totalRecords = o.TotalRecords;

                if (!uriService.isHateoasRequest())
                    return;

                if (links == null)
                    links = new List<ILink>();

                links.Add(
                       new Link(new Uri(_uriService.selfUriWithQuery),
                       "self",
                       "GET")
                   );

                SetPagination(new PaginationFilter(o.PageNumber, o.PageSize));
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
