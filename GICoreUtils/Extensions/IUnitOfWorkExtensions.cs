using GICoreInterfaces.Infraestructure.Repository;
using GICoreInterfaces.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreUtils.Extensions
{
    /// <summary>
    /// extensiones para la unidad de trabajo
    /// </summary>
    public static class IUnitOfWorkExtensions
    {
        /// <summary>
        /// agrega un repositorio generico basado en el tipo de la entidad
        /// </summary>
        /// <typeparam name="T">tipo de la entidad</typeparam>
        /// <param name="unitOfWork">unidad de trabajo donde se agregara</param>
        /// <returns>el repositorio agregado</returns>
        public static IGenericRepository<T> Repositories<T>(this IUnitOfWork unitOfWork) where T : class
        {
            var repositories = unitOfWork.Repositories.repositories;
            var repository = repositories.Where(t => t.Key == typeof(T).Name).FirstOrDefault();

            if (repository.Equals(default(KeyValuePair<string, object>)))
                return unitOfWork.AddRepository<T>(unitOfWork.Repositories);

            return (IGenericRepository<T>)repository.Value;
        }
    }
}
