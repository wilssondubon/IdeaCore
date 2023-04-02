using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInterfaces.Infraestructure.Repository
{
    /// <summary>
    /// interfaz para una clase que contiene un diccionario de repositorios
    /// </summary>
    public interface IDynamicRepositoryCollection
    {
        /// <summary>
        /// diccionario de repositorios
        /// </summary>
        IDictionary<string, object> repositories { get; }
        /// <summary>
        /// funcion para agregar un nuevo elemento al diccionario de repositorios
        /// </summary>
        /// <param name="Key">el nombre del tipo de la entidad en la que se basa el repositorio</param>
        /// <param name="value">la instancia del repositorio</param>
        void Add(string Key, object value);
    }
}
