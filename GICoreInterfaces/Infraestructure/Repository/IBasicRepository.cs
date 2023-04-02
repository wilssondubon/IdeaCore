using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInterfaces.Infraestructure.Repository
{
    public interface IBasicRepository
    {
        /// <summary>
        /// Ejecuta una eliminacion
        /// </summary>
        /// <param name="id">es una representacion del campo que sirve como primary key de la entidad</param>
        void Delete(object id);
        /// <summary>
        /// Ejecuta una eliminacion
        /// </summary>
        /// <param name="keyvalues">representa un array de todos los campos que componen el primary key</param>
        void Delete(params object[] keyvalues);
        /// <summary>
        /// obtiene una coleccion de las propiedades de navegacion de la entidad
        /// </summary>
        /// <returns>coleccion de propiedades de navegacion de la entidad</returns>
        IEnumerable<string> GetNavigationProperties();
        /// <summary>
        /// obtiene una coleccion de las propiedades que forman la llave primaria de la entidad
        /// </summary>
        /// <returns>coleccion de propiedades que forman la llave primaria de la entidad</returns>
        IEnumerable<string> GetPrimaryKeyProperties();
    }
}
