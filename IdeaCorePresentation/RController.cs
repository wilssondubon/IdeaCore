using IdeaCoreInterfaces.Application;
using IdeaCoreInterfaces.Application.Response;
using IdeaCoreInterfaces.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCorePresentation
{
    /// <summary>
    /// Controlador con funciones comunes de un Read
    /// </summary>
    /// <typeparam name="Model">tipo del modelo en el que se basara el controlador</typeparam>
    public class RController<Model, Entity> : Controller<Model> where Model : class where Entity : class
    {
        /// <summary>
        /// servicio generico de tipo Read
        /// </summary>
        protected readonly IRService<Model, Entity> _service;
        protected virtual async Task<IActionResult> ExecuteAction(Func<Task<IServiceResponse<Model>>> serviceAction)
        {
            try
            {
                var payload = await serviceAction();
                if (payload.Status == 200)
                {
                    return Ok(payload);
                }
                return StatusCode(payload.Status, payload);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected virtual async Task<IActionResult> ExecuteAction(Func<Task<IServiceResponseList<Model>>> serviceAction)
        {
            try
            {
                var payload = await serviceAction();
                if (payload.Status == 200)
                {
                    return Ok(payload);
                }
                return StatusCode(payload.Status, payload);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// inicializa el controlador
        /// </summary>
        /// <param name="logger">servicio para escribir logs</param>
        /// <param name="service">servicio generico de tipo Read</param>
        public RController(ILogger logger, IRService<Model, Entity> service) : base(logger)
        {
            _service = service;
        }
        public virtual async Task<IActionResult> FindByID(params object[] keyvalues)
            => await ExecuteAction(async () => await _service.GetById(keyvalues));
        public virtual async Task<IActionResult> Get(object o)
            => await ExecuteAction(async () => await _service.Get(o));
        public virtual async Task<IActionResult> GetFirst()
            => await ExecuteAction(async () => await _service.GetFirst());
        public virtual async Task<IActionResult> GetFirst(Expression<Func<Model, bool>> filter)
            => await ExecuteAction(async () => await _service.GetFirst(filter));
        public virtual async Task<IActionResult> GetFirst(Expression<Func<Entity, bool>> filter)
            => await ExecuteAction(async () => await _service.GetFirst(filter));
        public virtual async Task<IActionResult> GetFirst(Expression<Func<Model, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
           => await ExecuteAction(async () => await _service.GetFirst(filter, orderBy));
        public virtual async Task<IActionResult> GetFirst(Expression<Func<Entity, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
           => await ExecuteAction(async () => await _service.GetFirst(filter, orderBy));
        public virtual async Task<IActionResult> GetFirst(Expression<Func<Model, bool>> filter, string orderBy)
           => await ExecuteAction(async () => await _service.GetFirst(filter, orderBy));
        public virtual async Task<IActionResult> GetFirst(Expression<Func<Entity, bool>> filter, string orderBy)
           => await ExecuteAction(async () => await _service.GetFirst(filter, orderBy));
        public virtual async Task<IActionResult> GetFirst(Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
           => await ExecuteAction(async () => await _service.GetFirst(orderBy));
        public virtual async Task<IActionResult> GetFirst(string orderBy)
           => await ExecuteAction(async () => await _service.GetFirst(orderBy));
        public virtual async Task<IActionResult> GetFirst(IDictionary<string, string> filter)
           => await ExecuteAction(async () => await _service.GetFirst(filter));
        public virtual async Task<IActionResult> GetFirst(IDictionary<string, string> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
           => await ExecuteAction(async () => await _service.GetFirst(filter, orderBy));
        public virtual async Task<IActionResult> GetFirst(IDictionary<string, string> filter, string orderBy)
           => await ExecuteAction(async () => await _service.GetFirst(filter, orderBy));
        public virtual async Task<IActionResult> GetByID(object id)
           => await ExecuteAction(async () => await _service.GetByIDAsync(id));
        public virtual async Task<IActionResult> GetByID(params object[] keyvalues)
           => await ExecuteAction(async () => await _service.GetByIDAsync(keyvalues));
        public virtual async Task<IActionResult> Get()
           => await ExecuteAction(async () => await _service.Get());
        public virtual async Task<IActionResult> Get(IPaginationFilter paginationFilter)
           => await ExecuteAction(async () => await _service.Get(paginationFilter));
        public virtual async Task<IActionResult> Get(IFilterQueryParams queryParams)
           => await ExecuteAction(async () => await _service.Get(queryParams));
        public virtual async Task<IActionResult> Get(string orderBy)
           => await ExecuteAction(async () => await _service.Get(orderBy));
        public virtual async Task<IActionResult> Get(string orderBy, IPaginationFilter paginationFilter)
           => await ExecuteAction(async () => await _service.Get(orderBy, paginationFilter));
        public virtual async Task<IActionResult> Get(Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
           => await ExecuteAction(async () => await _service.Get(orderBy));
        public virtual async Task<IActionResult> Get(Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter)
           => await ExecuteAction(async () => await _service.Get(orderBy, paginationFilter));
        public virtual async Task<IActionResult> Get(Expression<Func<Model, bool>> filter)
           => await ExecuteAction(async () => await _service.Get(filter));
        public virtual async Task<IActionResult> Get(Expression<Func<Entity, bool>> filter)
           => await ExecuteAction(async () => await _service.Get(filter));
        public virtual async Task<IActionResult> Get(Expression<Func<Model, bool>> filter, IPaginationFilter paginationFilter)
           => await ExecuteAction(async () => await _service.Get(filter, paginationFilter));
        public virtual async Task<IActionResult> Get(Expression<Func<Entity, bool>> filter, IPaginationFilter paginationFilter)
           => await ExecuteAction(async () => await _service.Get(filter, paginationFilter));
        public virtual async Task<IActionResult> Get(Expression<Func<Model, bool>> filter, IFilterQueryParams queryParams)
           => await ExecuteAction(async () => await _service.Get(filter, queryParams));
        public virtual async Task<IActionResult> Get(Expression<Func<Entity, bool>> filter, IFilterQueryParams queryParams)
           => await ExecuteAction(async () => await _service.Get(filter, queryParams));
        public virtual async Task<IActionResult> Get(Expression<Func<Model, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
           => await ExecuteAction(async () => await _service.Get(filter, orderBy));
        public virtual async Task<IActionResult> Get(Expression<Func<Entity, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
           => await ExecuteAction(async () => await _service.Get(filter, orderBy));
        public virtual async Task<IActionResult> Get(Expression<Func<Model, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter)
           => await ExecuteAction(async () => await _service.Get(filter, orderBy, paginationFilter));
        public virtual async Task<IActionResult> Get(Expression<Func<Entity, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter)
           => await ExecuteAction(async () => await _service.Get(filter, orderBy, paginationFilter));
        public virtual async Task<IActionResult> Get(Expression<Func<Model, bool>> filter, string orderBy)
           => await ExecuteAction(async () => await _service.Get(filter, orderBy));
        public virtual async Task<IActionResult> Get(Expression<Func<Entity, bool>> filter, string orderBy)
           => await ExecuteAction(async () => await _service.Get(filter, orderBy));
        public virtual async Task<IActionResult> Get(Expression<Func<Model, bool>> filter, string orderBy, IPaginationFilter paginationFilter)
           => await ExecuteAction(async () => await _service.Get(filter, orderBy, paginationFilter));
        public virtual async Task<IActionResult> Get(Expression<Func<Entity, bool>> filter, string orderBy, IPaginationFilter paginationFilter)
           => await ExecuteAction(async () => await _service.Get(filter, orderBy, paginationFilter));
        public virtual async Task<IActionResult> Get(IDictionary<string, string> filter)
           => await ExecuteAction(async () => await _service.Get(filter));
        public virtual async Task<IActionResult> Get(IDictionary<string, string> filter, IPaginationFilter paginationFilter)
           => await ExecuteAction(async () => await _service.Get(filter, paginationFilter));
        public virtual async Task<IActionResult> Get(IDictionary<string, string> filter, IFilterQueryParams queryParams)
           => await ExecuteAction(async () => await _service.Get(filter, queryParams));
        public virtual async Task<IActionResult> Get(IDictionary<string, string> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
           => await ExecuteAction(async () => await _service.Get(filter, orderBy));
        public virtual async Task<IActionResult> Get(IDictionary<string, string> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter)
           => await ExecuteAction(async () => await _service.Get(filter, orderBy, paginationFilter));
        public virtual async Task<IActionResult> Get(IDictionary<string, string> filter, string orderBy)
           => await ExecuteAction(async () => await _service.Get(filter, orderBy));
        public virtual async Task<IActionResult> Get(IDictionary<string, string> filter, string orderBy, IPaginationFilter paginationFilter)
           => await ExecuteAction(async () => await _service.Get(filter, orderBy, paginationFilter));
        /// <summary>
        /// funcion para devolver el nombre de la accion del controlador que funciona como una forma de obtener un unico elemento por su llave principal
        /// </summary>
        /// <returns>nombre la accion</returns>
        /// <exception cref="NotImplementedException">no Implementada</exception>
        protected override string GetByIdActionName()
        {
            throw new NotImplementedException();
        }
    }
}
