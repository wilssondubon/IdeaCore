using AutoMapper;
using IdeaCoreInterfaces.Application;
using IdeaCoreUtils.Accessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GICoreServices
{
    public partial class CommonService<Model, Entity> : ICommonService<Model, Entity> where Model : class where Entity : class
    {
        /// <summary>
        /// retorna un dictionario de tipo nombre de la propiedad y valor de la propiedad basada en las propiedades que conforman una llave principal
        /// </summary>
        /// <param name="source">modelo para extraer las propiedades</param>
        /// <returns>dictionario de tipo nombre de la propiedad y valor de la propiedad basado en las propiedades que conforman una llave principal</returns>
        public object ModelKeyParameters(Model source)
        {
            IEnumerable<string> primaryKeyProperties = _repositorio.GetPrimaryKeyProperties();
            ObjectAccesor objectAccesor = new ObjectAccesor(source);
            IDictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (string str in primaryKeyProperties)
                dictionary.Add(str, objectAccesor.Get(str));
            return dictionary;
        }
        /// <summary>
        /// Mapea una entidad a un modelo
        /// </summary>
        /// <typeparam name="T">Tipo de la entidad</typeparam>
        /// <param name="o">objeto de clase entidad</param>
        /// <returns>modelo creado</returns>
        public Model MapModel<T>(T o) where T : class => _mapper.Map<Model>(o);
        /// <summary>
        /// Mapea una entidad a un modelo de los cuales no estaban registradas en automapper
        /// </summary>
        /// <typeparam name="T">tipo de la entidad</typeparam>
        /// <param name="o">objeto de tipo entidad</param>
        /// <returns>modelo creado</returns>
        public Model MapModelFromDynamic<T>(T o) where T : class => new MapperConfiguration(cfg => { }).CreateMapper().Map<Model>(o);
        /// <summary>
        /// convierte la primera letra de un string a mayuscula
        /// </summary>
        /// <param name="input">string a a ser trabajado</param>
        /// <returns>string con la primera letra mayuscula</returns>
        /// <exception cref="ArgumentException">error si el string esta vacio</exception>
        /// <exception cref="ArgumentNullException">error si el string es null</exception>
        private string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case "":
                    throw new ArgumentException("input cannot be empty", nameof(input));
                case null:
                    throw new ArgumentNullException(nameof(input));
                default:
                    return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }
        /// <summary>
        /// Crea un modelo basado en un diccionario de tipo nombre de la propiedad y valor de la propiedad
        /// </summary>
        /// <param name="parameters">diccionario de tipo nombre de la propiedad y valor de la propiedad</param>
        /// <returns>modelo creado</returns>
        public Model MapModelFromDictionary(IDictionary<string, object> parameters)
        {
            return JsonSerializer.Deserialize<Model>(JsonSerializer.Serialize(new Dictionary<string, object>(parameters.Select(t => new KeyValuePair<string, object>(FirstCharToUpper(t.Key), t.Value)))));
        }
        /// <summary>
        /// Mapea una coleccion de entidades en una coleccion de modelos
        /// </summary>
        /// <typeparam name="T">Tipo de la entidad</typeparam>
        /// <param name="o">entidad de clase entidad</param>
        /// <returns>Enumerable de entidades de tipo modelos</returns>
        public IEnumerable<Model> MapModels<T>(T o) where T : class => this._mapper.Map<IEnumerable<Model>>((object)o);
    }
}
