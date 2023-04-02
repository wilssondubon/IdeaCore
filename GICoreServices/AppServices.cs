using AutoMapper;
using GICoreServices;
using IdeaCoreInterfaces.Aplication.Services.Common;
using IdeaCoreInterfaces.Aplication.Services.Generics;
using IdeaCoreInterfaces.Infraestructure.UnitOfWork;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreServices
{
    /// <summary>
    /// contenedor de servicios genericos para la aplicacion
    /// </summary>
    public class AppServices : IAppServices
    {
        /// <summary>
        /// unidad de trabajo
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// automapper
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// servicio que se conecta al httpcontext de la solicitud
        /// </summary>
        private readonly IUriService _uriService;
        /// <summary>
        /// Coleccion de Servicio Genericos
        /// </summary>
        public IList<IBasicService>? Services { get; private set; }
        /// <summary>
        /// Agrega y retorna un Servicio Generico basado en los tipos genericos de modelo y entidad
        /// </summary>
        /// <typeparam name="TModel">Tipo del modelo</typeparam>
        /// <typeparam name="TEntity">Tipo de la entidad</typeparam>
        /// <returns>retorna un servicio generico basado en el modelo</returns>
        public ICRUDService<TModel, TEntity> Service<TModel, TEntity>() where TModel : class where TEntity : class
        {
            if (Services is null)
                Services = new List<IBasicService>();

            var service = new CommonService<TModel, TEntity>(_unitOfWork, _mapper, _uriService);

            if (!Services.Contains(service))
            {
                Services.Add(service);
            }

            return service;
        }
        /// <summary>
        /// Agrega y retorna un Servicio Generico basado en los tipos genericos de modelo y entidad
        /// </summary>
        /// <typeparam name="TModel">Tipo del modelo</typeparam>
        /// <typeparam name="TEntity">Tipo de la entidad</typeparam>
        /// <param name="includeNavigationProperties">propiedades de navegaciones que se incluiran en cada elemento retraido</param>
        /// <returns>retorna un servicio generico basado en el modelo</returns>
        public ICRUDService<TModel, TEntity> Service<TModel, TEntity>(params Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] includeNavigationProperties) where TModel : class where TEntity : class
        {
            if (Services is null)
                Services = new List<IBasicService>();

            var service = new CommonService<TModel, TEntity>(_unitOfWork, _mapper, _uriService, includeNavigationProperties);

            if (!Services.Contains(service))
            {
                Services.Add(service);
            }

            return service;
        }
        /// <summary>
        /// inicializa el appservices
        /// </summary>
        /// <param name="unitOfWork">unidad de trabajo</param>
        /// <param name="mapper">automapper</param>
        /// <param name="uriService">servicio que se conecta al httpcontext de la solicitud</param>
        public AppServices(IUnitOfWork unitOfWork, IMapper mapper, IUriService uriService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uriService = uriService;
        }
    }
}
