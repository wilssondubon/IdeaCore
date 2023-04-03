using IdeaCoreInterfaces.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCorePresentation
{
    public partial class CRUDController<Model, Entity> : Controller<Model> where Model : class where Entity : class
    {
        /// <summary>
        /// controlador con funciones que sirven para hacer Read
        /// </summary>
        protected readonly RController<Model, Entity> rController;
        protected virtual async Task<IActionResult> ExecuteReadAction(Func<Task<IActionResult>> controllerAction)
        {
            rController.ControllerContext = ControllerContext;
            return await controllerAction();
        }
        protected virtual async Task<IActionResult> _FindByID(params object[] keyvalues)
            => await ExecuteReadAction(() => rController.FindByID(keyvalues));
        protected virtual async Task<IActionResult> _Get(object o)
            => await ExecuteReadAction(() => rController.Get(o));
        protected virtual async Task<IActionResult> _GetFirst()
            => await ExecuteReadAction(() => rController.GetFirst());
        protected virtual async Task<IActionResult> _GetFirst(Expression<Func<Model, bool>> filter)
            => await ExecuteReadAction(() => rController.GetFirst(filter));
        protected virtual async Task<IActionResult> _GetFirst(Expression<Func<Entity, bool>> filter)
            => await ExecuteReadAction(() => rController.GetFirst(filter));
        protected virtual async Task<IActionResult> _GetFirst(Expression<Func<Model, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteReadAction(() => rController.GetFirst(filter, orderBy));
        protected virtual async Task<IActionResult> _GetFirst(Expression<Func<Entity, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteReadAction(() => rController.GetFirst(filter, orderBy));
        protected virtual async Task<IActionResult> _GetFirst(Expression<Func<Model, bool>> filter, string orderBy)
            => await ExecuteReadAction(() => rController.GetFirst(filter, orderBy));
        protected virtual async Task<IActionResult> _GetFirst(Expression<Func<Entity, bool>> filter, string orderBy)
            => await ExecuteReadAction(() => rController.GetFirst(filter, orderBy));
        protected virtual async Task<IActionResult> _GetFirst(Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteReadAction(() => rController.GetFirst(orderBy));
        protected virtual async Task<IActionResult> _GetFirst(string orderBy)
            => await ExecuteReadAction(() => rController.GetFirst(orderBy));
        protected virtual async Task<IActionResult> _GetFirst(IDictionary<string, string> filter)
            => await ExecuteReadAction(() => rController.GetFirst(filter));
        protected virtual async Task<IActionResult> _GetFirst(IDictionary<string, string> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteReadAction(() => rController.GetFirst(filter, orderBy));
        protected virtual async Task<IActionResult> _GetFirst(IDictionary<string, string> filter, string orderBy)
            => await ExecuteReadAction(() => rController.GetFirst(filter, orderBy));
        protected virtual async Task<IActionResult> _GetByID(object id)
            => await ExecuteReadAction(() => rController.GetByID(id));
        protected virtual async Task<IActionResult> _GetByID(params object[] keyvalues)
            => await ExecuteReadAction(() => rController.GetByID(keyvalues));
        protected virtual async Task<IActionResult> _Get()
            => await ExecuteReadAction(() => rController.Get());
        protected virtual async Task<IActionResult> _Get(IPaginationFilter paginationFilter)
            => await ExecuteReadAction(() => rController.Get(paginationFilter));
        protected virtual async Task<IActionResult> _Get(IFilterQueryParams queryParams)
            => await ExecuteReadAction(() => rController.Get(queryParams));
        protected virtual async Task<IActionResult> _Get(string orderBy)
            => await ExecuteReadAction(() => rController.Get(orderBy));
        protected virtual async Task<IActionResult> _Get(string orderBy, IPaginationFilter paginationFilter)
            => await ExecuteReadAction(() => rController.Get(orderBy, paginationFilter));
        protected virtual async Task<IActionResult> _Get(Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteReadAction(() => rController.Get(orderBy));
        protected virtual async Task<IActionResult> _Get(Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter)
            => await ExecuteReadAction(() => rController.Get(orderBy, paginationFilter));
        protected virtual async Task<IActionResult> _Get(Expression<Func<Model, bool>> filter)
            => await ExecuteReadAction(() => rController.Get(filter));
        protected virtual async Task<IActionResult> _Get(Expression<Func<Entity, bool>> filter)
            => await ExecuteReadAction(() => rController.Get(filter));
        protected virtual async Task<IActionResult> _Get(Expression<Func<Model, bool>> filter, IPaginationFilter paginationFilter)
            => await ExecuteReadAction(() => rController.Get(filter, paginationFilter));
        protected virtual async Task<IActionResult> _Get(Expression<Func<Entity, bool>> filter, IPaginationFilter paginationFilter)
            => await ExecuteReadAction(() => rController.Get(filter, paginationFilter));
        protected virtual async Task<IActionResult> _Get(Expression<Func<Model, bool>> filter, IFilterQueryParams queryParams)
            => await ExecuteReadAction(() => rController.Get(filter, queryParams));
        protected virtual async Task<IActionResult> _Get(Expression<Func<Entity, bool>> filter, IFilterQueryParams queryParams)
            => await ExecuteReadAction(() => rController.Get(filter, queryParams));
        protected virtual async Task<IActionResult> _Get(Expression<Func<Model, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteReadAction(() => rController.Get(filter, orderBy));
        protected virtual async Task<IActionResult> _Get(Expression<Func<Entity, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteReadAction(() => rController.Get(filter, orderBy));
        protected virtual async Task<IActionResult> _Get(Expression<Func<Model, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter)
            => await ExecuteReadAction(() => rController.Get(filter, orderBy, paginationFilter));
        protected virtual async Task<IActionResult> _Get(Expression<Func<Entity, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter)
            => await ExecuteReadAction(() => rController.Get(filter, orderBy, paginationFilter));
        protected virtual async Task<IActionResult> _Get(Expression<Func<Model, bool>> filter, string orderBy)
            => await ExecuteReadAction(() => rController.Get(filter, orderBy));
        protected virtual async Task<IActionResult> _Get(Expression<Func<Entity, bool>> filter, string orderBy)
            => await ExecuteReadAction(() => rController.Get(filter, orderBy));
        protected virtual async Task<IActionResult> _Get(Expression<Func<Model, bool>> filter, string orderBy, IPaginationFilter paginationFilter)
            => await ExecuteReadAction(() => rController.Get(filter, orderBy, paginationFilter));
        protected virtual async Task<IActionResult> _Get(Expression<Func<Entity, bool>> filter, string orderBy, IPaginationFilter paginationFilter)
            => await ExecuteReadAction(() => rController.Get(filter, orderBy, paginationFilter));
        protected virtual async Task<IActionResult> _Get(IDictionary<string, string> filter)
            => await ExecuteReadAction(() => rController.Get(filter));
        protected virtual async Task<IActionResult> _Get(IDictionary<string, string> filter, IPaginationFilter paginationFilter)
            => await ExecuteReadAction(() => rController.Get(filter, paginationFilter));
        protected virtual async Task<IActionResult> _Get(IDictionary<string, string> filter, IFilterQueryParams queryParams)
            => await ExecuteReadAction(() => rController.Get(filter, queryParams));
        protected virtual async Task<IActionResult> _Get(IDictionary<string, string> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteReadAction(() => rController.Get(filter, orderBy));
        protected virtual async Task<IActionResult> _Get(IDictionary<string, string> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter)
            => await ExecuteReadAction(() => rController.Get(filter, orderBy, paginationFilter));
        protected virtual async Task<IActionResult> _Get(IDictionary<string, string> filter, string orderBy)
            => await ExecuteReadAction(() => rController.Get(filter, orderBy));
        protected virtual async Task<IActionResult> _Get(IDictionary<string, string> filter, string orderBy, IPaginationFilter paginationFilter)
            => await ExecuteReadAction(() => rController.Get(filter, orderBy, paginationFilter));
    }
}
