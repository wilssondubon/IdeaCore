using GICoreInterfaces.Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreInfraestructure.Repository
{
    /// <summary>
    /// clase para una clase que contiene un diccionario de repositorios
    /// </summary>
    public class DynamicRepositoryCollection : IDynamicRepositoryCollection
    {
        /// <summary>
        /// diccionario de repositorios
        /// </summary>
        public IDictionary<string, object> repositories { get; private set; }
        /// <summary>
        /// funcion para agregar un nuevo elemento al diccionario de repositorios
        /// </summary>
        /// <param name="Key">el nombre del tipo de la entidad en la que se basa el repositorio</param>
        /// <param name="value">la instancia del repositorio</param>
        public void Add(string Key, object value)
        {
            if (repositories == null)
                repositories = new Dictionary<string, object>();

            if (repositories.Where(t => t.Key == Key).FirstOrDefault().Equals(default(KeyValuePair<string, object>)))
                repositories.Add(Key, value);
        }
        /// <summary>
        /// inicializa la coleccion dinamica de repositorios con un nuevo diccionario
        /// </summary>
        public DynamicRepositoryCollection()
        {
            repositories = new Dictionary<string, object>();
        }
    }
}
