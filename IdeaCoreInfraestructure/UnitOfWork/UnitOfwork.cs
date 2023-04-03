using IdeaCoreInfraestructure.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using IdeaCoreInterfaces.Infraestructure;

namespace IdeaCoreInfraestructure.UnitOfWork
{
    /// <summary>
    /// clase para la creacion de una unidad de trabajo para la infraestructura de la aplicacion
    /// </summary>
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        /// <summary>
        /// bandera de destruccion
        /// </summary>
        private bool disposed;
        /// <summary>
        /// dbcontext
        /// </summary>
        public DbContext dbcontext { get; }
        /// <summary>
        /// coleccion de repositorios
        /// </summary>
        public IDynamicRepositoryCollection Repositories { get; private set; }
        /// <summary>
        /// funcion para agregar un repositorio generico basado en un tipo de entidad
        /// </summary>
        /// <typeparam name="T">tipo de la entidad</typeparam>
        /// <returns>devuelve un repositorio generico</returns>
        public IGenericRepository<T> NewRepository<T>(Func<IQueryable<T>, IIncludableQueryable<T, object>>[]? includesPaths = null) where T : class
        {
            return new GenericRepository<T>(dbcontext, includesPaths);
        }
        /// <summary>
        /// funcion para agregar un repositorio generico basado en un tipo de entidad
        /// </summary>
        /// <typeparam name="T">tipo de la entidad</typeparam>
        public void AddRepository<T>(Func<IQueryable<T>, IIncludableQueryable<T, object>>[]? includesPaths = null) where T : class
        {
            if (Repositories == null)
                Repositories = new DynamicRepositoryCollection();

            Repositories.Add(typeof(T).Name, new GenericRepository<T>(dbcontext, includesPaths));
        }
        /// <summary>
        /// funcion para agregar un repositorio generico basado en un tipo de entidad a una lista dinamica de repositorios
        /// </summary>
        /// <typeparam name="T">tipo de la entidad</typeparam>
        /// <param name="dynamicRepository">lista de dinamica de repositorios</param>
        /// <returns>devuelve un repositorio generico</returns>
        public IGenericRepository<T> AddRepository<T>(IDynamicRepositoryCollection dynamicRepository, Func<IQueryable<T>, IIncludableQueryable<T, object>>[]? includesPaths = null) where T : class
        {
            if (dynamicRepository == null)
                dynamicRepository = new DynamicRepositoryCollection();

            var repository = new GenericRepository<T>(dbcontext, includesPaths);

            dynamicRepository.Add(typeof(T).Name, repository);
            return repository;
        }
        /// <summary>
        /// inicializar la unidad de trabajo
        /// </summary>
        /// <param name="db">dbcontext</param>
        public UnitOfWork(DbContext db)
        {
            dbcontext = db;

            Repositories = new DynamicRepositoryCollection();
        }
        /// <summary>
        /// funcion que ejecuta una instruccion de guardado hacia el dbcontext
        /// </summary>
        public virtual void Save() => dbcontext.SaveChanges();
        /// <summary>
        /// funcion que ejecuta una instruccion de guardado hacia el dbcontext de forma asincrona
        /// </summary>
        /// <returns>una tarea que representa una llamada asincrona que contiene el estatus del guardado</returns>
        public virtual async Task<int> SaveChangesAsync() => await dbcontext.SaveChangesAsync();
        /// <summary>
        /// para destruir la unidad de trabajo cuando la solicitud haya finalizado
        /// </summary>
        /// <param name="disposing">destruir?</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
                dbcontext.Dispose();
            disposed = true;
        }
        /// <summary>
        /// para destruir la unidad de trabajo cuando la solicitud haya finalizado
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
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
        public virtual IQueryable<TEntity> Query<TEntity>(string spName, SqlParameter[] parameters, Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "") where TEntity : class
        {
            DbSet<TEntity> source1 = dbcontext.Set<TEntity>();
            IQueryable<TEntity> source2;
            if (parameters != null && parameters.Count() > 0)
            {
                for (int index = 0; index <= parameters.Length - 1; ++index)
                {
                    string str = parameters[index].ParameterName;
                    if (!str.Contains("@"))
                        str = "@" + str;
                    if (index != parameters.Length - 1)
                        str += ", ";
                    spName = spName + " " + str;
                }
                source2 = source1.FromSqlRaw(spName, parameters);
            }
            else
                source2 = source1.FromSqlRaw(spName);
            if (filter != null)
                source2 = source2.Where(filter);
            string str1 = includeProperties;
            char[] separator = new char[1] { ',' };
            foreach (string navigationPropertyPath in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                source2 = source2.Include(navigationPropertyPath);
            return orderBy != null ? orderBy(source2) : source2;
        }
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
        public virtual IQueryable<TEntity> Exec<TEntity>(string spName, SqlParameter[] parameters, Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "") where TEntity : class
        {
            DbSet<TEntity> source1 = dbcontext.Set<TEntity>();
            FormattableString sql = FormattableStringFactory.Create("Exec {0} ", spName);
            IQueryable<TEntity> source2;
            if (parameters != null && parameters.Count() > 0)
            {
                for (int index = 0; index <= parameters.Length - 1; ++index)
                {
                    FormattableString formattableString = FormattableStringFactory.Create("{0} = {1}", parameters[index].ParameterName, parameters[index].Value);
                    if (!parameters[index].ParameterName.Contains("@"))
                        formattableString = FormattableStringFactory.Create("@{0} = {1}", parameters[index].ParameterName, parameters[index].Value);
                    if (index != parameters.Length - 1)
                        formattableString = FormattableStringFactory.Create("{0}, ", formattableString);
                    sql = FormattableStringFactory.Create("{0} {1}", sql, formattableString);
                }
                source2 = source1.FromSqlInterpolated(sql);
            }
            else
                source2 = source1.FromSqlInterpolated(sql);
            if (filter != null)
                source2 = source2.Where(filter);
            string str = includeProperties;
            char[] separator = new char[1] { ',' };
            foreach (string navigationPropertyPath in str.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                source2 = source2.Include(navigationPropertyPath);
            return orderBy != null ? orderBy(source2) : source2;
        }
    }
}
