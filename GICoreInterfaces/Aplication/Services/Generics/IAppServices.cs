using GICoreInterfaces.Aplication.Services.Common;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreInterfaces.Aplication.Services.Generics
{
    /// <summary>
    /// contenedor de servicios genericos para la aplicacion
    /// </summary>
    public interface IAppServices
    {
        /// <summary>
        /// Coleccion de Servicio Genericos
        /// </summary>
        IList<IBasicService>? Services { get; }
        /// <summary>
        /// Agrega y retorna un Servicio Generico basado en los tipos genericos de modelo y entidad
        /// </summary>
        /// <typeparam name="TModel">Tipo del modelo</typeparam>
        /// <typeparam name="TEntity">Tipo de la entidad</typeparam>
        /// <returns>retorna un servicio generico basado en el modelo</returns>
        ICRUDService<TModel, TEntity> Service<TModel, TEntity>() where TModel : class where TEntity : class;
        /// <summary>
        /// Agrega y retorna un Servicio Generico basado en los tipos genericos de modelo y entidad
        /// </summary>
        /// <typeparam name="TModel">Tipo del modelo</typeparam>
        /// <typeparam name="TEntity">Tipo de la entidad</typeparam>
        /// <param name="includeNavigationProperties">propiedades de navegaciones que se incluiran en cada elemento retraido</param>
        /// <returns>retorna un servicio generico basado en el modelo</returns>
        ICRUDService<TModel, TEntity> Service<TModel, TEntity>(params Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] includeNavigationProperties) where TModel : class where TEntity : class;
    }
}
