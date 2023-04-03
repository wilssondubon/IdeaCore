using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IdeaCoreApplication.Visitors;
using IdeaCoreInterfaces.Application;
using IdeaCoreInterfaces.Application.Response;
using IdeaCoreInterfaces.Common;

namespace GICoreServices
{
    public partial class CommonService<Model, Entity> : IRService<Model, Entity> where Model : class where Entity : class
    {
        protected virtual async Task<T> ExecuteAndHandleError<T>(Func<Task<T>> action) where T : IResponse
        {
            try
            {
                return await action();
            }
            catch
            {
                throw;
            }
        }
        protected virtual T ExecuteAndHandleError<T>(Func<T> action) where T : IResponse
        {
            try
            {
                return action();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// transforma un predicado de un tipo base modelo a un predicado con tipo base Entidad
        /// </summary>
        /// <param name="expression">predicado de tipo base modelo</param>
        /// <returns>predicado de tipo base entidad</returns>
        protected Expression<Func<Entity, bool>> ConvertExpression(Expression<Func<Model, bool>> expression)
        {
            return new ExpressionConverter<Model, Entity>().Convert(expression);
        }
        /// <summary>
        /// Devuelve un elemento tipo respuesta basado en el Primary Key de una entidad
        /// </summary>
        /// <param name="keyvalues">representa un array de todos los campos que componen el primary key</param>
        /// <returns>Una tarea que representa una operacion asincrona. La tarea contiene un modelo de tipo respuesta.</returns>
        public virtual async Task<IServiceResponse<Model>> GetById(params object[] keyvalues)
            => await FindByID(keyvalues);
        public virtual async Task<IServiceResponse<Model>> FindByID(params object[] keyvalues)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.FindByID(keyvalues)));
        public virtual async Task<IServiceResponse<Model>> Get(object o)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(o)));
        public virtual async Task<IServiceResponse<Model>> GetFirst()
            => await ExecuteAndHandleError(async () => Response(await _repositorio.GetFirst()));
        public virtual async Task<IServiceResponse<Model>> GetFirst(Expression<Func<Model, bool>> filter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.GetFirst(ConvertExpression(filter))));
        public virtual async Task<IServiceResponse<Model>> GetFirst(Expression<Func<Entity, bool>> filter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.GetFirst(filter)));
        public virtual async Task<IServiceResponse<Model>> GetFirst(Expression<Func<Model, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.GetFirst(ConvertExpression(filter), orderBy.Compile())));
        public virtual async Task<IServiceResponse<Model>> GetFirst(Expression<Func<Entity, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
           => await ExecuteAndHandleError(async () => Response(await _repositorio.GetFirst(filter, orderBy.Compile())));
        public virtual async Task<IServiceResponse<Model>> GetFirst(Expression<Func<Model, bool>> filter, string orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.GetFirst(ConvertExpression(filter), orderBy)));
        public virtual async Task<IServiceResponse<Model>> GetFirst(Expression<Func<Entity, bool>> filter, string orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.GetFirst(filter, orderBy)));
        public virtual async Task<IServiceResponse<Model>> GetFirst(Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.GetFirst(orderBy.Compile())));
        public virtual async Task<IServiceResponse<Model>> GetFirst(string orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.GetFirst(orderBy)));
        public virtual async Task<IServiceResponse<Model>> GetFirst(IDictionary<string, string> filter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.GetFirst(filter)));
        public virtual async Task<IServiceResponse<Model>> GetFirst(IDictionary<string, string> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.GetFirst(filter, orderBy.Compile())));
        public virtual async Task<IServiceResponse<Model>> GetFirst(IDictionary<string, string> filter, string orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.GetFirst(filter, orderBy)));
        public virtual IServiceResponse<Model> GetByID(object id)
            => ExecuteAndHandleError(() => Response(_repositorio.GetByID(id)));
        public virtual async Task<IServiceResponse<Model>> GetByIDAsync(object id)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.GetByIDAsync(id)));
        public virtual IServiceResponse<Model> GetByID(params object[] keyvalues)
            => ExecuteAndHandleError(() => Response(_repositorio.GetByID(keyvalues)));
        public virtual async Task<IServiceResponse<Model>> GetByIDAsync(params object[] keyvalues)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.GetByIDAsync(keyvalues)));
        public virtual async Task<IServiceResponseList<Model>> Get()
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get()));
        public virtual async Task<IServiceResponseList<Model>> Get(IPaginationFilter paginationFilter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(paginationFilter)));
        public virtual async Task<IServiceResponseList<Model>> Get(IFilterQueryParams queryParams)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(queryParams.OrderBy, queryParams)));
        public virtual async Task<IServiceResponseList<Model>> Get(string orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(orderBy)));
        public virtual async Task<IServiceResponseList<Model>> Get(string orderBy, IPaginationFilter paginationFilter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(orderBy, paginationFilter)));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(orderBy.Compile())));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(orderBy.Compile(), paginationFilter)));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Model, bool>> filter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(ConvertExpression(filter))));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(filter)));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Model, bool>> filter, IPaginationFilter paginationFilter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(ConvertExpression(filter), paginationFilter)));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, IPaginationFilter paginationFilter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(filter, paginationFilter)));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Model, bool>> filter, IFilterQueryParams queryParams)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(ConvertExpression(filter), queryParams.OrderBy, queryParams)));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, IFilterQueryParams queryParams)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(filter, queryParams.OrderBy, queryParams)));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Model, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(ConvertExpression(filter), orderBy.Compile())));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(filter, orderBy.Compile())));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Model, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(ConvertExpression(filter), orderBy.Compile(), paginationFilter)));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(filter, orderBy.Compile(), paginationFilter)));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Model, bool>> filter, string orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(ConvertExpression(filter), orderBy)));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, string orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(filter, orderBy)));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Model, bool>> filter, string orderBy, IPaginationFilter paginationFilter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(ConvertExpression(filter), orderBy, paginationFilter)));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, string orderBy, IPaginationFilter paginationFilter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(filter, orderBy, paginationFilter)));
        public virtual async Task<IServiceResponseList<Model>> Get(IDictionary<string, string> filter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(filter)));
        public virtual async Task<IServiceResponseList<Model>> Get(IDictionary<string, string> filter, IPaginationFilter paginationFilter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(filter, paginationFilter)));
        public virtual async Task<IServiceResponseList<Model>> Get(IDictionary<string, string> filter, IFilterQueryParams queryParams)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(filter, queryParams.OrderBy, queryParams)));
        public virtual async Task<IServiceResponseList<Model>> Get(IDictionary<string, string> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(filter, orderBy.Compile())));
        public virtual async Task<IServiceResponseList<Model>> Get(IDictionary<string, string> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(filter, orderBy.Compile(), paginationFilter)));
        public virtual async Task<IServiceResponseList<Model>> Get(IDictionary<string, string> filter, string orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(orderBy)));
        public virtual async Task<IServiceResponseList<Model>> Get(IDictionary<string, string> filter, string orderBy, IPaginationFilter paginationFilter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(filter, orderBy, paginationFilter)));
    }
}
