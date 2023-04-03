using IdeaCoreInfraestructure.Extensions;
using IdeaCoreInterfaces.Infraestructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInfraestructure.Repository
{
    public partial class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Obtiene una coleccion de entidades mediante un procedimiento almacenado por medio de FromSqlRaw
        /// </summary>
        /// <param name="spName">nombre del procedimiento almacenado</param>
        /// <param name="parameters">coleccion de parametros para pasar al procedimiento almacenado</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IReadOnlyList<TEntity>> Query(string spName, SqlParameter[] parameters)
        {
            IQueryable<TEntity> source;
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
                source = dbSet.FromSqlRaw(spName, parameters);
            }
            else
                source = dbSet.FromSqlRaw(spName);

            if (_includesPaths is not null)
                source = source.IncludeNavigations(_includesPaths);

            return await source.ToListAsync();
        }
        /// <summary>
        /// Obtiene una coleccion de entidades mediante un procedimiento almacenado por medio de FromSqlInterpolated
        /// </summary>
        /// <param name="spName">nombre del procedimiento almacenado</param>
        /// <param name="parameters">coleccion de parametros para pasar al procedimiento almacenado</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene una coleccion de entidades.</returns>
        public virtual async Task<IReadOnlyList<TEntity>> Exec(string spName, SqlParameter[] parameters)
        {
            FormattableString sql = FormattableStringFactory.Create("Exec {0} ", spName);
            IQueryable<TEntity> source;
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
                source = dbSet.FromSqlInterpolated(sql);
            }
            else
                source = dbSet.FromSqlInterpolated(sql);

            if (_includesPaths is not null)
                source = source.IncludeNavigations(_includesPaths);

            return await source.ToListAsync();
        }
        /// <summary>
        /// Ejecuta una insercion
        /// </summary>
        /// <param name="entity">la entidad que se guardara</param>
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            context.Entry(entity).State = EntityState.Added;
        }
        /// <summary>
        /// Ejecuta una actualizacion a un registro 
        /// </summary>
        /// <param name="entityToUpdate">la entidad que se modificara</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        /// <summary>
        /// Ejecuta una eliminacion
        /// </summary>
        /// <param name="entityToDelete">la entidad que se eliminara</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
                dbSet.Attach(entityToDelete);
            dbSet.Remove(entityToDelete);
        }
        /// <summary>
        /// Ejecuta una eliminacion
        /// </summary>
        /// <param name="id">es una representacion del campo que sirve como primary key de la entidad</param>
        public virtual void Delete(object id) => Delete(dbSet.Find(id));
        /// <summary>
        /// Ejecuta una eliminacion
        /// </summary>
        /// <param name="keyvalues">representa un array de todos los campos que componen el primary key</param>
        public virtual void Delete(params object[] keyvalues) => Delete(dbSet.Find(keyvalues));
    }
}
