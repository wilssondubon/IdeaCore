using GICoreInterfaces.Aplication.Models;
using GICoreInterfaces.Infraestructure.Repository;
using GICoreUtils.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GICoreInfraestructure.Repository
{
    public partial class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Devuelve un elemento Entidad basado en el Primary Key
        /// </summary>
        /// <param name="keyvalues">representa un array de todos los campos que componen el primary key</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        public virtual async Task<TEntity> FindByID(params object[] keyvalues) => await DbSet.FilterByPrimaryKey(context, keyvalues).IncludeNavigations(_includesPaths).FirstOrDefaultAsync();
        /// <summary>
        /// Devuelve un elemento Entidad basado en el Primary Key que se extrae del objeto pasado por parametros
        /// </summary>
        /// <param name="o">Objeto que contiene al menos las propiedades para formar el Primary Key de la entidad</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        public virtual async Task<TEntity> Get(object o)
        {
            return await dbSet.FilterByObjectWithPrimaryKey(context, o).IncludeNavigations(_includesPaths).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Obtiene el primer elemento de una coleccion de entidades basado en un predicado lambda
        /// </summary>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        public virtual async Task<TEntity> GetFirst()
        {
            return await dbSet.IncludeNavigations(_includesPaths).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Obtiene el primer elemento de una coleccion de entidades basado en un predicado lambda
        /// </summary>
        /// <param name="filter">expresion de predicado lambda</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        public virtual async Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> source = dbSet;
            if (filter != null)
                source = source.Where(filter).IncludeNavigations(_includesPaths);

            return await source.FirstOrDefaultAsync();
        }
        /// <summary>
        /// Obtiene el primer elemento de una coleccion de entidades basado en un predicado lambda
        /// </summary>
        /// <param name="filter">expresion de predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderan el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        public virtual async Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            IQueryable<TEntity> source = dbSet;
            if (filter != null)
                source = source.Where(filter).IncludeNavigations(_includesPaths);

            if (orderBy != null)
                source = orderBy(source);

            return await source.FirstOrDefaultAsync();
        }
        /// <summary>
        /// Obtiene el primer elemento de una coleccion de entidades basado en un predicado lambda
        /// </summary>
        /// <param name="filter">expresion de predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderan el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        public virtual async Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> filter, string orderBy)
        {
            IQueryable<TEntity> source = dbSet;
            if (filter != null)
                source = source.Where(filter).IncludeNavigations(_includesPaths);

            if (!string.IsNullOrWhiteSpace(orderBy))
                source = source.ApplySort(orderBy);

            return await source.FirstOrDefaultAsync();
        }
        /// <summary>
        /// Obtiene el primer elemento de una coleccion de entidades basado en un diccionario que se convierte en un predicado lambda
        /// </summary>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        public virtual async Task<TEntity> GetFirst(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            IQueryable<TEntity> source = dbSet.IncludeNavigations(_includesPaths);

            if (orderBy != null)
                source = orderBy(source);

            return await source.FirstOrDefaultAsync();
        }
        /// <summary>
        /// Obtiene el primer elemento de una coleccion de entidades basado en un diccionario que se convierte en un predicado lambda
        /// </summary>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        public virtual async Task<TEntity> GetFirst(string orderBy)
        {
            IQueryable<TEntity> source = dbSet.IncludeNavigations(_includesPaths);

            if (!string.IsNullOrWhiteSpace(orderBy))
                source = source.ApplySort(orderBy);

            return await source.FirstOrDefaultAsync();
        }
        /// <summary>
        /// Obtiene el primer elemento de una coleccion de entidades basado en un diccionario que se convierte en un predicado lambda
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        public virtual async Task<TEntity> GetFirst(IDictionary<string, string> filter)
        {
            IQueryable<TEntity> source = dbSet.FilterByKeyValueCollection(context, filter).IncludeNavigations(_includesPaths);

            return await source.FirstOrDefaultAsync();
        }
        /// <summary>
        /// Obtiene el primer elemento de una coleccion de entidades basado en un diccionario que se convierte en un predicado lambda
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        public virtual async Task<TEntity> GetFirst(IDictionary<string, string> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            IQueryable<TEntity> source = dbSet.FilterByKeyValueCollection(context, filter).IncludeNavigations(_includesPaths);

            if (orderBy != null)
                source = orderBy(source);

            return await source.FirstOrDefaultAsync();
        }
        /// <summary>
        /// Obtiene el primer elemento de una coleccion de entidades basado en un diccionario que se convierte en un predicado lambda
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        public virtual async Task<TEntity> GetFirst(IDictionary<string, string> filter, string orderBy)
        {
            IQueryable<TEntity> source = dbSet.FilterByKeyValueCollection(context, filter).IncludeNavigations(_includesPaths);

            if (!string.IsNullOrWhiteSpace(orderBy))
                source = source.ApplySort(orderBy);

            return await source.FirstOrDefaultAsync();
        }
        /// <summary>
        /// Devuelve un elemento Entidad basado en el Primary Key {id}
        /// </summary>
        /// <param name="id">es una representacion del campo que sirve como primary key de la entidad</param>
        /// <returns>devuelve una entidad</returns>
        public virtual TEntity GetByID(object id) => dbSet.Find(id);
        /// <summary>
        /// Devuelve un elemento Entidad basado en el Primary Key {id}
        /// </summary>
        /// <param name="id">es una representacion del campo que sirve como primary key de la entidad</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        public virtual async Task<TEntity> GetByIDAsync(object id) => await dbSet.FindAsync(id);
        /// <summary>
        /// Devuelve un elemento Entidad basado en el Primary Key
        /// </summary>
        /// <param name="keyvalues">representa un array de todos los campos que componen el primary key</param>
        /// <returns>devuelve una entidad</returns>
        public virtual TEntity GetByID(params object[] keyvalues) => dbSet.Find(keyvalues);
        /// <summary>
        /// Devuelve un elemento Entidad basado en el Primary Key
        /// </summary>
        /// <param name="keyvalues">representa un array de todos los campos que componen el primary key</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una entidad.</returns>
        public virtual async Task<TEntity> GetByIDAsync(params object[] keyvalues) => await dbSet.FindAsync(keyvalues);
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IReadOnlyList<TEntity>> Get()
        {
            return await dbSet.IncludeNavigations(_includesPaths).ToListAsync();
        }
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>lista paginada</returns>
        public virtual async Task<IPagedReadOnlyList<TEntity>> Get(IPaginationFilter paginationFilter)
        {
            return await dbSet.IncludeNavigations(_includesPaths).ToListAsync(paginationFilter);
        }
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IReadOnlyList<TEntity>> Get(string orderBy)
        {
            return await dbSet.ApplySort(orderBy).IncludeNavigations(_includesPaths).ToListAsync();
        }
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IPagedReadOnlyList<TEntity>> Get(string orderBy, IPaginationFilter paginationFilter)
        {
            return await dbSet.ApplySort(orderBy).IncludeNavigations(_includesPaths).ToListAsync(paginationFilter);
        }
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IReadOnlyList<TEntity>> Get(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            IQueryable<TEntity> source = dbSet;
            return orderBy != null ? await orderBy(source).IncludeNavigations(_includesPaths).ToListAsync() : await source.IncludeNavigations(_includesPaths).ToListAsync();
        }
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IPagedReadOnlyList<TEntity>> Get(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, IPaginationFilter paginationFilter)
        {
            IQueryable<TEntity> source = dbSet;
            return orderBy != null ? await orderBy(source).IncludeNavigations(_includesPaths).ToListAsync(paginationFilter) : await source.IncludeNavigations(_includesPaths).ToListAsync(paginationFilter);
        }
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">expresion de predicado lambda para filtrar la coleccion de Entidades</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IReadOnlyList<TEntity>> Get(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> source = dbSet;
            if (filter != null)
                source = source.Where(filter);
            return await source.IncludeNavigations(_includesPaths).ToListAsync();
        }
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">expresion de predicado lambda para filtrar la coleccion de Entidades</param>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IPagedReadOnlyList<TEntity>> Get(Expression<Func<TEntity, bool>> filter, IPaginationFilter paginationFilter)
        {
            IQueryable<TEntity> source = dbSet;
            if (filter != null)
                source = source.Where(filter);
            return await source.IncludeNavigations(_includesPaths).ToListAsync(paginationFilter);
        }
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">expresion de predicado lambda para filtrar la coleccion de Entidades</param>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IReadOnlyList<TEntity>> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            IQueryable<TEntity> source = dbSet;
            if (filter != null)
                source = source.Where(filter);
            return orderBy != null ? await orderBy(source).IncludeNavigations(_includesPaths).ToListAsync() : await source.IncludeNavigations(_includesPaths).ToListAsync();
        }
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">expresion de predicado lambda para filtrar la coleccion de Entidades</param>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IPagedReadOnlyList<TEntity>> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, IPaginationFilter paginationFilter)
        {
            IQueryable<TEntity> source = dbSet;
            if (filter != null)
                source = source.Where(filter);
            return orderBy != null ? await orderBy(source).IncludeNavigations(_includesPaths).ToListAsync(paginationFilter) : await source.IncludeNavigations(_includesPaths).ToListAsync(paginationFilter);
        }
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">expresion de predicado lambda para filtrar la coleccion de Entidades</param>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IReadOnlyList<TEntity>> Get(Expression<Func<TEntity, bool>> filter, string orderBy)
        {
            IQueryable<TEntity> source = dbSet;
            if (filter != null)
                source = source.Where(filter);
            return await source.ApplySort(orderBy).IncludeNavigations(_includesPaths).ToListAsync();
        }
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">expresion de predicado lambda para filtrar la coleccion de Entidades</param>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IPagedReadOnlyList<TEntity>> Get(Expression<Func<TEntity, bool>> filter, string orderBy, IPaginationFilter paginationFilter)
        {
            IQueryable<TEntity> source = dbSet;
            if (filter != null)
                source = source.Where(filter);
            return await source.ApplySort(orderBy).IncludeNavigations(_includesPaths).ToListAsync(paginationFilter);
        }
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IReadOnlyList<TEntity>> Get(IDictionary<string, string> filter)
        {
            IQueryable<TEntity> source = DbSet.FilterByKeyValueCollection(context, filter).IncludeNavigations(_includesPaths);
            return await source.ToListAsync();
        }
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IPagedReadOnlyList<TEntity>> Get(IDictionary<string, string> filter, IPaginationFilter paginationFilter)
        {
            IQueryable<TEntity> source = DbSet.FilterByKeyValueCollection(context, filter).IncludeNavigations(_includesPaths);
            return await source.ToListAsync(paginationFilter);
        }
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IReadOnlyList<TEntity>> Get(IDictionary<string, string> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            IQueryable<TEntity> source = DbSet.FilterByKeyValueCollection(context, filter).IncludeNavigations(_includesPaths);
            return orderBy != null ? await orderBy(source).ToListAsync() : await source.ToListAsync();
        }
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IPagedReadOnlyList<TEntity>> Get(IDictionary<string, string> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, IPaginationFilter paginationFilter)
        {
            IQueryable<TEntity> source = DbSet.FilterByKeyValueCollection(context, filter).IncludeNavigations(_includesPaths);
            return orderBy != null ? await orderBy(source).ToListAsync(paginationFilter) : await source.ToListAsync(paginationFilter);
        }
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IReadOnlyList<TEntity>> Get(IDictionary<string, string> filter, string orderBy)
        {
            IQueryable<TEntity> source = DbSet.FilterByKeyValueCollection(context, filter).IncludeNavigations(_includesPaths);
            return await source.ApplySort(orderBy).IncludeNavigations(_includesPaths).ToListAsync();
        }
        /// <summary>
        /// Obtiene una coleccion de entidades
        /// </summary>
        /// <param name="filter">diccionario con valores de nombre de propiedad y valor a filtrar que se convertira en un predicado lambda</param>
        /// <param name="orderBy">parametro opcional para orderar el query principal antes de obtener el primero de la lista</param>
        /// <param name="paginationFilter">filtro de paginacion</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IPagedReadOnlyList<TEntity>> Get(IDictionary<string, string> filter, string orderBy, IPaginationFilter paginationFilter)
        {
            IQueryable<TEntity> source = DbSet.FilterByKeyValueCollection(context, filter).IncludeNavigations(_includesPaths);
            return await source.ApplySort(orderBy).IncludeNavigations(_includesPaths).ToListAsync(paginationFilter);
        }
        /// <summary>
        /// Devuelve un valor maximo de alguna de las propiedades de una entidad contenida en una coleccion
        /// </summary>
        /// <typeparam name="T">el tipo de dato que se busca retornar</typeparam>
        /// <param name="maxexpression">expresion para indicar la propiedad de la que se busca el maximo</param>
        /// <param name="predicate">expresion de predicado lambda para filtrar la coleccion de Entidades</param>
        /// <returns>devuelve una valor maximo</returns>
        public virtual T Max<T>(Expression<Func<TEntity, T>> maxexpression, Expression<Func<TEntity, bool>>? predicate = null)
        {
            T? obj;
            try
            {
                obj = predicate != null ? dbSet.Where(predicate).Max(maxexpression) : dbSet.Max(maxexpression);
            }
            catch
            {
                obj = default(T);
            }
            return obj;
        }
    }
}
