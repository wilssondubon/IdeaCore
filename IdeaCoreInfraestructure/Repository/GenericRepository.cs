using IdeaCoreInterfaces.Infraestructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInfraestructure.Repository
{
    /// <summary>
    /// clase para un repositorio generico
    /// </summary>
    /// <typeparam name="TEntity">Entidad a la que consultara el repositorio</typeparam>
    public partial class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Array de funciones Include
        /// </summary>
        private Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[]? _includesPaths;
        /// <summary>
        /// dbcontext a utilizar
        /// </summary>
        internal DbContext context;
        /// <summary>
        /// dbset de la entidad
        /// </summary>
        internal DbSet<TEntity> dbSet;
        /// <summary>
        /// dbset de la entidad
        /// </summary>
        public DbSet<TEntity> DbSet => dbSet;
        /// <summary>
        /// inicializa un nuevo repositorio generico
        /// </summary>
        /// <param name="ctx">dbcontext</param>
        public GenericRepository(DbContext ctx)
        {
            context = ctx;
            dbSet = context.Set<TEntity>();
        }
        /// <summary>
        /// inicializa un nuevo repositorio generico
        /// </summary>
        /// <param name="ctx">dbcontext</param>
        /// <param name="includesPaths">Array de funciones Include</param>
        public GenericRepository(DbContext ctx, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[]? includesPaths)
        {
            context = ctx;
            dbSet = context.Set<TEntity>();
            _includesPaths = includesPaths;
        }
        /// <summary>
        /// estas propiedades de navegaciones se incluiran en cada entidad retraida por el repositorio
        /// repositorio.setNavigationsProperties(t=>t.include(e=>e.navigationProperty));
        /// </summary>
        /// <param name="includesPaths">Array de funciones Include</param>
        public void setNavigationsProperties(params Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] includesPaths)
        {
            _includesPaths = includesPaths;
        }
        /// <summary>
        /// obtiene una coleccion de las propiedades de navegacion de la entidad
        /// </summary>
        /// <returns>coleccion de propiedades de navegacion de la entidad</returns>
        public virtual IEnumerable<string> GetNavigationProperties() => context.Model.GetEntityTypes().Select(t => new
        {
            Name = t.ClrType.Name,
            NavigationProperties = t.GetNavigations().Select(x => x.PropertyInfo.Name)
        }).Where(t => t.Name == typeof(TEntity).Name).SelectMany(t => t.NavigationProperties).ToList();
        /// <summary>
        /// obtiene una coleccion de las propiedades que forman la llave primaria de la entidad
        /// </summary>
        /// <returns>coleccion de propiedades que forman la llave primaria de la entidad</returns>
        public virtual IEnumerable<string> GetPrimaryKeyProperties()
        {
            return context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties.Select(t => t.Name).ToList();
        }
    }
}
