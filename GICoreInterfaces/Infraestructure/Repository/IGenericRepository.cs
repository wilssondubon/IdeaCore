using IdeaCoreInterfaces.Aplication.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInterfaces.Infraestructure.Repository
{
    /// <summary>
    /// interaz para un repositorio generico
    /// </summary>
    /// <typeparam name="TEntity">Entidad a la que consultara el repositorio</typeparam>
    public interface IGenericRepository<TEntity> : IBasicRepository where TEntity : class
    {
        /// <summary>
        /// dbset de la entidad
        /// </summary>
        DbSet<TEntity> DbSet { get; }
        /// <summary>
        /// estas propiedades de navegaciones se incluiran en cada entidad retraida por el repositorio
        /// repositorio.setNavigationsProperties(t=>t.include(e=>e.navigationProperty));
        /// </summary>
        /// <param name="includesPaths">Array de funciones Include</param>
        void setNavigationsProperties(params Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] includesPaths);
        /// <summary>
        /// Devuelve un elemento Entidad basado en el Primary Key
        /// </summary>
        /// <param name="keyvalues">representa un array de todos los campos que componen el primary key</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        Task<TEntity> FindByID(params object[] keyvalues);
        /// <summary>
        /// Devuelve un elemento Entidad basado en el Primary Key que se extrae del objeto pasado por parametros
        /// </summary>
        /// <param name="o">Objeto que contiene al menos las propiedades para formar el Primary Key de la entidad</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        Task<TEntity> Get(object o);
        /// <summary>
        /// Obtiene el primer elemento de una coleccion de entidades
        /// </summary>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        Task<TEntity> GetFirst();
        /// <summary>
        /// Obtiene el primer elemento de una coleccion de entidades basado en un predicado lambda
        /// </summary>
        /// <param name="filter">expresion de predicado lambda</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> filter);
        /// <summary>
        /// Obtiene el primer elemento de una coleccion de entidades basado en un predicado lambda
        /// </summary>
        /// <param name="filter">expresion de predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderan el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        /// <summary>
        /// Obtiene el primer elemento de una coleccion de entidades basado en un predicado lambda
        /// </summary>
        /// <param name="filter">expresion de predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderan el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> filter, string orderBy);
        // <summary>
        /// Obtiene el primer elemento de una coleccion de entidades basado en un predicado lambda
        /// </summary>
        /// <param name="orderBy">parametro opcional para orderan el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        Task<TEntity> GetFirst(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        /// <summary>
        /// Obtiene el primer elemento de una coleccion de entidades
        /// </summary>
        /// <param name="orderBy">parametro opcional para orderan el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        Task<TEntity> GetFirst(string orderBy);
        /// <summary>
        /// Obtiene el primer elemento de una coleccion de entidades basado en un diccionario que se convierte en un predicado lambda
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        Task<TEntity> GetFirst(IDictionary<string, string> filter);
        /// <summary>
        /// Obtiene el primer elemento de una coleccion de entidades basado en un diccionario que se convierte en un predicado lambda
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        Task<TEntity> GetFirst(IDictionary<string, string> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        /// <summary>
        /// Obtiene el primer elemento de una coleccion de entidades basado en un diccionario que se convierte en un predicado lambda
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        Task<TEntity> GetFirst(IDictionary<string, string> filter, string orderBy);
        /// <summary>
        /// Devuelve un elemento Entidad basado en el Primary Key {id}
        /// </summary>
        /// <param name="id">es una representacion del campo que sirve como primary key de la entidad</param>
        /// <returns>devuelve una entidad</returns>
        TEntity GetByID(object id);
        /// <summary>
        /// Devuelve un elemento Entidad basado en el Primary Key {id}
        /// </summary>
        /// <param name="id">es una representacion del campo que sirve como primary key de la entidad</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        Task<TEntity> GetByIDAsync(object id);
        /// <summary>
        /// Devuelve un elemento Entidad basado en el Primary Key
        /// </summary>
        /// <param name="keyvalues">representa un array de todos los campos que componen el primary key</param>
        /// <returns>devuelve una entidad</returns>
        TEntity GetByID(params object[] keyvalues);
        /// <summary>
        /// Devuelve un elemento Entidad basado en el Primary Key
        /// </summary>
        /// <param name="keyvalues">representa un array de todos los campos que componen el primary key</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        Task<TEntity> GetByIDAsync(params object[] keyvalues);
        /// <summary>
        /// Devuelve un valor maximo de alguna de las propiedades de una entidad contenida en una coleccion
        /// </summary>
        /// <typeparam name="T">el tipo de dato que se busca retornar</typeparam>
        /// <param name="maxexpression">expresion para indicar la propiedad de la que se busca el maximo</param>
        /// <param name="predicate">expresion de predicado lambda para filtrar la coleccion de Entidades</param>
        /// <returns>devuelve una valor maximo</returns>
        T Max<T>(Expression<Func<TEntity, T>> maxexpression, Expression<Func<TEntity, bool>>? predicate = null);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IReadOnlyList<TEntity>> Get();
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>lista paginada</returns>
        Task<IPagedReadOnlyList<TEntity>> Get(IPaginationFilter paginationFilter);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="orderBy">parametro opcional para orderar el query</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IReadOnlyList<TEntity>> Get(string orderBy);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IPagedReadOnlyList<TEntity>> Get(string orderBy, IPaginationFilter paginationFilter);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="orderBy">parametro opcional para orderar el query</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IReadOnlyList<TEntity>> Get(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IPagedReadOnlyList<TEntity>> Get(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, IPaginationFilter paginationFilter);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">expresion de predicado lambda para filtrar la coleccion de Entidades</param>
        /// <param name="orderBy">parametro opcional para orderar el query</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IReadOnlyList<TEntity>> Get(Expression<Func<TEntity, bool>> filter);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">expresion de predicado lambda para filtrar la coleccion de Entidades</param>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IPagedReadOnlyList<TEntity>> Get(Expression<Func<TEntity, bool>> filter, IPaginationFilter paginationFilter);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">expresion de predicado lambda para filtrar la coleccion de Entidades</param>
        /// <param name="orderBy">parametro opcional para orderar el query</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IReadOnlyList<TEntity>> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">expresion de predicado lambda para filtrar la coleccion de Entidades</param>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IPagedReadOnlyList<TEntity>> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, IPaginationFilter paginationFilter);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">expresion de predicado lambda para filtrar la coleccion de Entidades</param>
        /// <param name="orderBy">parametro opcional para orderar el query</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IReadOnlyList<TEntity>> Get(Expression<Func<TEntity, bool>> filter, string orderBy);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">expresion de predicado lambda para filtrar la coleccion de Entidades</param>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IPagedReadOnlyList<TEntity>> Get(Expression<Func<TEntity, bool>> filter, string orderBy, IPaginationFilter paginationFilter);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderar el query</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IReadOnlyList<TEntity>> Get(IDictionary<string, string> filter);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IPagedReadOnlyList<TEntity>> Get(IDictionary<string, string> filter, IPaginationFilter paginationFilter);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderar el query</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IReadOnlyList<TEntity>> Get(IDictionary<string, string> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IPagedReadOnlyList<TEntity>> Get(IDictionary<string, string> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, IPaginationFilter paginationFilter);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderar el query</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IReadOnlyList<TEntity>> Get(IDictionary<string, string> filter, string orderBy);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IPagedReadOnlyList<TEntity>> Get(IDictionary<string, string> filter, string orderBy, IPaginationFilter paginationFilter);
        /// <summary>
        /// Obtiene una coleccion de entidades mediante un procedimiento almacenado por medio de FromSqlRaw
        /// </summary>
        /// <param name="spName">nombre del procedimiento almacenado</param>
        /// <param name="parameters">coleccion de parametros para pasar al procedimiento almacenado</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IReadOnlyList<TEntity>> Query(string spName, SqlParameter[] parameters);
        /// <summary>
        /// Obtiene una coleccion de entidades mediante un procedimiento almacenado por medio de FromSqlInterpolated
        /// </summary>
        /// <param name="spName">nombre del procedimiento almacenado</param>
        /// <param name="parameters">coleccion de parametros para pasar al procedimiento almacenado</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        Task<IReadOnlyList<TEntity>> Exec(string spName, SqlParameter[] parameters);
        /// <summary>
        /// Ejecuta una insercion
        /// </summary>
        /// <param name="entity">la entidad que se guardara</param>
        void Insert(TEntity entity);
        /// <summary>
        /// Ejecuta una actualizacion a un registro 
        /// </summary>
        /// <param name="entityToUpdate">la entidad que se modificara</param>
        void Update(TEntity entityToUpdate);
        /// <summary>
        /// Ejecuta una eliminacion
        /// </summary>
        /// <param name="entityToDelete">la entidad que se eliminara</param>
        void Delete(TEntity entityToDelete);
    }
}
