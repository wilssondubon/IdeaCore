using IdeaCoreInterfaces.Application.Response;
using IdeaCoreInterfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInterfaces.Application
{
    /// <summary>
    /// interfaz para un servicio de operaciones Read
    /// </summary>
    /// <typeparam name="Model">tipo del modelo en el que se basara un controlador</typeparam>
    public interface IRService<Model, Entity> : ICommonService<Model, Entity> where Model : class where Entity : class
    {
        Task<IServiceResponse<Model>> FindByID(params object[] keyvalues);
        Task<IServiceResponse<Model>> Get(object o);
        Task<IServiceResponse<Model>> GetFirst();
        Task<IServiceResponse<Model>> GetFirst(Expression<Func<Model, bool>> filter);
        Task<IServiceResponse<Model>> GetFirst(Expression<Func<Entity, bool>> filter);
        Task<IServiceResponse<Model>> GetFirst(Expression<Func<Model, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy);
        Task<IServiceResponse<Model>> GetFirst(Expression<Func<Entity, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy);
        Task<IServiceResponse<Model>> GetFirst(Expression<Func<Model, bool>> filter, string orderBy);
        Task<IServiceResponse<Model>> GetFirst(Expression<Func<Entity, bool>> filter, string orderBy);
        Task<IServiceResponse<Model>> GetFirst(Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy);
        Task<IServiceResponse<Model>> GetFirst(string orderBy);
        Task<IServiceResponse<Model>> GetFirst(IDictionary<string, string> filter);
        Task<IServiceResponse<Model>> GetFirst(IDictionary<string, string> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy);
        Task<IServiceResponse<Model>> GetFirst(IDictionary<string, string> filter, string orderBy);
        IServiceResponse<Model> GetByID(object id);
        Task<IServiceResponse<Model>> GetByIDAsync(object id);
        IServiceResponse<Model> GetByID(params object[] keyvalues);
        Task<IServiceResponse<Model>> GetByIDAsync(params object[] keyvalues);
        Task<IServiceResponseList<Model>> Get();
        Task<IServiceResponseList<Model>> Get(IPaginationFilter paginationFilter);
        Task<IServiceResponseList<Model>> Get(IFilterQueryParams queryParams);
        Task<IServiceResponseList<Model>> Get(string orderBy);
        Task<IServiceResponseList<Model>> Get(string orderBy, IPaginationFilter paginationFilter);
        Task<IServiceResponseList<Model>> Get(Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy);
        Task<IServiceResponseList<Model>> Get(Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter);
        Task<IServiceResponseList<Model>> Get(Expression<Func<Model, bool>> filter);
        Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter);
        Task<IServiceResponseList<Model>> Get(Expression<Func<Model, bool>> filter, IPaginationFilter paginationFilter);
        Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, IPaginationFilter paginationFilter);
        Task<IServiceResponseList<Model>> Get(Expression<Func<Model, bool>> filter, IFilterQueryParams queryParams);
        Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, IFilterQueryParams queryParams);
        Task<IServiceResponseList<Model>> Get(Expression<Func<Model, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy);
        Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy);
        Task<IServiceResponseList<Model>> Get(Expression<Func<Model, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter);
        Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter);
        Task<IServiceResponseList<Model>> Get(Expression<Func<Model, bool>> filter, string orderBy);
        Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, string orderBy);
        Task<IServiceResponseList<Model>> Get(Expression<Func<Model, bool>> filter, string orderBy, IPaginationFilter paginationFilter);
        Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, string orderBy, IPaginationFilter paginationFilter);
        Task<IServiceResponseList<Model>> Get(IDictionary<string, string> filter);
        Task<IServiceResponseList<Model>> Get(IDictionary<string, string> filter, IPaginationFilter paginationFilter);
        Task<IServiceResponseList<Model>> Get(IDictionary<string, string> filter, IFilterQueryParams queryParams);
        Task<IServiceResponseList<Model>> Get(IDictionary<string, string> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy);
        Task<IServiceResponseList<Model>> Get(IDictionary<string, string> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter);
        Task<IServiceResponseList<Model>> Get(IDictionary<string, string> filter, string orderBy);
        Task<IServiceResponseList<Model>> Get(IDictionary<string, string> filter, string orderBy, IPaginationFilter paginationFilter);
    }
}
