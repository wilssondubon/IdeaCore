using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInterfaces.Infraestructure
{
    /// <summary>
    /// interfaz para la creacion de una unidad de trabajo para la infraestructura de la aplicacion
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// coleccion de repositorios
        /// </summary>
        IDynamicRepositoryCollection Repositories { get; }
        /// <summary>
        /// funcion para agregar un repositorio generico basado en un tipo de entidad
        /// </summary>
        /// <typeparam name="T">tipo de la entidad</typeparam>
        /// <returns>devuelve un repositorio generico</returns>
        IGenericRepository<T> NewRepository<T>(Func<IQueryable<T>, IIncludableQueryable<T, object>>[]? includesPaths = null) where T : class;
        /// <summary>
        /// funcion para agregar un repositorio generico basado en un tipo de entidad
        /// </summary>
        /// <typeparam name="T">tipo de la entidad</typeparam>
        void AddRepository<T>(Func<IQueryable<T>, IIncludableQueryable<T, object>>[]? includesPaths = null) where T : class;
        /// <summary>
        /// funcion para agregar un repositorio generico basado en un tipo de entidad a una lista dinamica de repositorios
        /// </summary>
        /// <typeparam name="T">tipo de la entidad</typeparam>
        /// <param name="dynamicRepository">lista de dinamica de repositorios</param>
        /// <returns>devuelve un repositorio generico</returns>
        IGenericRepository<T> AddRepository<T>(IDynamicRepositoryCollection dynamicRepository, Func<IQueryable<T>, IIncludableQueryable<T, object>>[]? includesPaths = null) where T : class;
        /// <summary>
        /// para destruir la unidad de trabajo cuando la solicitud haya finalizado
        /// </summary>
        void Dispose();
        /// <summary>
        /// funcion para ejecutar una solicitud a un procedimiento almacenado que no devuelva cualquier tipo de entidad, usa FromSqlInterpolated
        /// </summary>
        /// <typeparam name="TEntity">tipo de la entidad a ser devueta </typeparam>
        /// <param name="spName">nombre del procedimiento almacenado</param>
        /// <param name="parameters">parametros para el procedimiento almacenado</param>
        /// <param name="filter">filtro para los datos devueltos</param>
        /// <param name="orderBy">orden para los datos devueltos</param>
        /// <param name="includeProperties">listado de propiedades de navegacion a ser incluidas en la respuesta</param>
        /// <returns>listado de consulta con la respuesta del procedimiento almacenado</returns>
        IQueryable<TEntity> Exec<TEntity>(
          string spName,
          SqlParameter[] parameters,
          Expression<Func<TEntity, bool>>? filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
          string includeProperties = "")
          where TEntity : class;
        /// <summary>
        /// funcion para ejecutar una solicitud a un procedimiento almacenado que no devuelva cualquier tipo de entidad, usa FromSqlRaw
        /// </summary>
        /// <typeparam name="TEntity">tipo de la entidad a ser devueta </typeparam>
        /// <param name="spName">nombre del procedimiento almacenado</param>
        /// <param name="parameters">parametros para el procedimiento almacenado</param>
        /// <param name="filter">filtro para los datos devueltos</param>
        /// <param name="orderBy">orden para los datos devueltos</param>
        /// <param name="includeProperties">listado de propiedades de navegacion a ser incluidas en la respuesta</param>
        /// <returns>listado de consulta con la respuesta del procedimiento almacenado</returns>
        IQueryable<TEntity> Query<TEntity>(
          string spName,
          SqlParameter[] parameters,
          Expression<Func<TEntity, bool>>? filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
          string includeProperties = "")
          where TEntity : class;
        /// <summary>
        /// funcion que ejecuta una instruccion de guardado hacia el dbcontext
        /// </summary>
        void Save();
        /// <summary>
        /// funcion que ejecuta una instruccion de guardado hacia el dbcontext de forma asincrona
        /// </summary>
        /// <returns>una tarea que representa una llamada asincrona que contiene el estatus del guardado</returns>
        Task<int> SaveChangesAsync();
    }
}
