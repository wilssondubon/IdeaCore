using IdeaCoreApplication.Extensions;
using IdeaCoreApplication.Models;
using IdeaCoreInterfaces.Common;
using IdeaCoreUtils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICoreServices
{
    public partial class CommonService<Model, Entity> where Model : class where Entity : class
    {
        /// <summary>
        /// funcion (sobrecargable) donde se define el mensaje a mostrar si una consulta de un elemento retorna sin resultados
        /// </summary>
        /// <returns>mensaje de error, respuesta sin resultados</returns>
        virtual protected CommonService<Model, Entity>.ServiceResponse noFoundError()
        {
            return onError("Sin Resultados", new string[] { "No se encontro ningun elemento que coincida" }, 404, true);
        }
        /// <summary>
        /// funcion que devuelve un mensaje de error
        /// </summary>
        /// <param name="error">titulo del error</param>
        /// <param name="errors">coleccion de errores</param>
        /// <param name="code">codigo de error, defaul -1</param>
        /// <param name="succeded">estado de la solicitud, default false</param>
        /// <returns>respuesta de error</returns>
        virtual protected CommonService<Model, Entity>.ServiceResponse onError(string error, string[] errors, short code = -1, bool succeded = false)
        {
            return new CommonService<Model, Entity>.ServiceResponse(code, error, errors, succeded);
        }
        /// <summary>
        /// funcion (sobrecargable) donde se define el mensaje a mostrar si una consulta de una coleccion retorna sin resultados
        /// </summary>
        /// <returns>mensaje de error, respuesta sin resultados</returns>
        virtual protected CommonService<Model, Entity>.ServiceResponseList noListNoFoundError()
        {
            return onListError("Sin Resultados", new string[] { "No se encontraron elementos que coincidan" }, 404, true);
        }
        /// <summary>
        /// funcion que devuelve un mensaje de error en caso de respuesta de lista
        /// </summary>
        /// <param name="error">titulo del error</param>
        /// <param name="errors">coleccion de errores</param>
        /// <param name="code">codigo de error, defaul -1</param>
        /// <param name="succeded">estado de la solicitud, default false</param>
        /// <returns></returns>
        virtual protected CommonService<Model, Entity>.ServiceResponseList onListError(string error, string[] errors, short code = -1, bool succeded = false)
        {
            return new CommonService<Model, Entity>.ServiceResponseList(code, error, errors, succeded);
        }
        /// <summary>
        /// recibe una entidad y crea una respuesta mapeandola hacia un modelo
        /// </summary>
        /// <param name="o">entidad a ser procesada</param>
        /// <returns>respuesta basada en un modelo</returns>
        virtual protected Response<Model> EntityResponse(Entity o)
        {
            return new Response<Model>(_mapper.Map<Model>(o), new TrackerResponse());
        }
        /// <summary>
        /// recibe una lista de entidades y crea una respuesta mapeandolas hacia un lista de modelos
        /// </summary>
        /// <param name="o"></param>
        /// <returns>respuesta basada en una lista de modelos</returns>
        virtual protected Response<IEnumerable<Model>> EntityResponse(IEnumerable<Entity> o)
        {
            return new Response<IEnumerable<Model>>(_mapper.Map<IEnumerable<Model>>(o), new TrackerResponse());
        }
        /// <summary>
        /// crea una respuesta basada en un modelo
        /// </summary>
        /// <param name="o">objeto de tipo modelo en el que se basara la respuesta</param>
        /// <returns>respuesta</returns>
        virtual protected CommonService<Model, Entity>.ServiceResponse Response(Model o) => new CommonService<Model, Entity>.ServiceResponse(o, new TrackerResponse());
        virtual protected CommonService<Model, Entity>.ServiceResponseList Response(IEnumerable<Model> o) => new CommonService<Model, Entity>.ServiceResponseList(o, new TrackerResponse(), _hateoasListWrapperService, o.Count());
        /// <summary>
        /// recibe una lista de entidades y crea una respuesta mapeandolas hacia una lista modelo, la respuesta puede ser un error si la lista de entidades no contiene elementos
        /// </summary>
        /// <param name="o">lista de entidades</param>
        /// <param name="totalRecords">total de elementos en la lista de entidades</param>
        /// <returns>respuesta basada en una lista de modelos</returns>
        virtual protected CommonService<Model, Entity>.ServiceResponseList Response(IEnumerable<Entity> o, int totalRecords)
        {
            if (o == null)
                return noListNoFoundError();

            return new CommonService<Model, Entity>.ServiceResponseList(o, new TrackerResponse(), _mapper, _hateoasListWrapperService, totalRecords);
        }
        virtual protected CommonService<Model, Entity>.ServiceResponseList Response(IEnumerable<Model> o, int totalRecords)
        {
            if (o == null)
                return noListNoFoundError();

            return new CommonService<Model, Entity>.ServiceResponseList(o, new TrackerResponse(), _hateoasListWrapperService, totalRecords);
        }
        // <summary>
        /// recibe una lista de entidades y crea una respuesta mapeandolas hacia una lista modelo, la respuesta puede ser un error si la lista de entidades no contiene elementos
        /// </summary>
        /// <param name="o">lista de entidades</param>
        /// <returns>respuesta basada en una lista de modelos</returns>
        virtual protected CommonService<Model, Entity>.ServiceResponseList Response(IEnumerable<Entity> o)
        {
            if (o == null)
                return noListNoFoundError();

            return new CommonService<Model, Entity>.ServiceResponseList(o, new TrackerResponse(), _mapper, _hateoasListWrapperService, o.Count());
        }
        /// <summary>
        /// recibe una lista de entidades y crea una respuesta mapeandolas hacia una lista modelo, la respuesta puede ser un error si la lista de entidades no contiene elementos
        /// </summary>
        /// <param name="o">lista de entidades</param>
        /// <returns>respuesta basada en una lista de modelos</returns>
        virtual protected CommonService<Model, Entity>.ServiceResponseList Response(IPagedReadOnlyList<Entity> o)
        {
            if (o == null)
                return noListNoFoundError();

            return new CommonService<Model, Entity>.ServiceResponseList(o, new TrackerResponse(), _mapper, _hateoasListWrapperService);
        }
        virtual protected CommonService<Model, Entity>.ServiceResponseList Response(IPagedReadOnlyList<Model> o)
        {
            if (o == null)
                return noListNoFoundError();

            return new CommonService<Model, Entity>.ServiceResponseList(o, new TrackerResponse(),  _hateoasListWrapperService);
        }
        /// <summary>
        /// recibe una lista de entidades y crea una respuesta mapeandolas hacia una lista modelos paginada
        /// </summary>
        /// <param name="o">lista de entidades</param>
        /// <param name="paginationFilter">valores para la paginacion</param>
        /// <returns>respuesta basada en una lista de modelos</returns>
        virtual protected async Task<CommonService<Model, Entity>.ServiceResponseList> PaginatedResponse(IEnumerable<Entity> o, IPaginationFilter paginationFilter)
        {
            if (o == null)
                return noListNoFoundError();

            List<Entity> listAsync = paginationFilter.GetPaginatedList(o);

            return Response(listAsync, o.Count());
        }
        /// <summary>
        /// recibe una entidade y crea una respuesta mapeandola hacia un modelo, la respuesta puede ser un error si la entidad es nula
        /// </summary>
        /// <param name="o">endidad a ser trabajada</param>
        /// <returns>respuesta basada en un modelo</returns>
        virtual protected CommonService<Model, Entity>.ServiceResponse Response(Entity o)
        {
            if (o == null)
                return noFoundError();

            return new CommonService<Model, Entity>.ServiceResponse(o, new TrackerResponse(), _mapper);
        }

        virtual protected CommonService<Model, Entity>.ServiceResponse Response(short status, Entity o)
        {
            if (o == null)
                return noFoundError();

            return new CommonService<Model, Entity>.ServiceResponse(o, new TrackerResponse(status), _mapper);
        }
    }
}
