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
using IdeaCoreUtils.Extensions;
using IdeaCoreUtils.Models;
using Microsoft.EntityFrameworkCore;

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
        public virtual async Task<IServiceResponse<Model>> GetById(params object[] keyvalues)
            => await FindByID(keyvalues);
        public virtual async Task<IServiceResponse<Model>> FindByID(params object[] keyvalues)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.FindByID(keyvalues)));
        public virtual async Task<IServiceResponse<Model>> Get(object o)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(o)));
        public virtual async Task<IServiceResponse<Model>> GetFirst()
            => await ExecuteAndHandleError(async () => Response(await _repositorio.GetFirst()));
        public virtual async Task<IServiceResponse<Model>> GetFirst(Expression<Func<Entity, bool>> filter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.GetFirst(filter)));
        public virtual async Task<IServiceResponse<Model>> GetFirst(Expression<Func<Entity, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
           => await ExecuteAndHandleError(async () => Response(await _repositorio.GetFirst(filter, orderBy.Compile())));
        public virtual async Task<IServiceResponse<Model>> GetFirst(Expression<Func<Entity, bool>> filter, string orderBy)
            => await ExecuteAndHandleError(async () =>
                {
                    IReadOnlyList<Entity> filteredEntityList = await _repositorio.Get(filter);
                    Model filteredModel = await _mapper.Map<IReadOnlyList<Model>>(filteredEntityList).AsQueryable().ApplySort(orderBy).FirstOrDefaultAsync();
                    return Response(filteredModel);
                }
            );
        public virtual async Task<IServiceResponse<Model>> GetFirst(Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.GetFirst(orderBy.Compile())));
        public virtual async Task<IServiceResponse<Model>> GetFirst(string orderBy)
            => await ExecuteAndHandleError(async () =>
                {
                    IReadOnlyList<Entity> entityList = await _repositorio.Get();
                    Model model = await _mapper.Map<IReadOnlyList<Model>>(entityList).AsQueryable().ApplySort(orderBy).FirstOrDefaultAsync();
                    return Response(model);
                }
            );
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
            => await ExecuteAndHandleError(async () =>
                {
                    IReadOnlyList<Entity> entityList = await _repositorio.Get();
                    IEnumerable<Model> modelList = await _mapper.Map<IReadOnlyList<Model>>(entityList).AsQueryable().ApplySort(queryParams?.OrderBy).ToListAsync(queryParams);
                    return Response(modelList);
                }
            );
        public virtual async Task<IServiceResponseList<Model>> Get(string orderBy)
            => await ExecuteAndHandleError(async () =>
                {
                    IReadOnlyList<Entity> entityList = await _repositorio.Get();
                    IEnumerable<Model> modelList = await _mapper.Map<IReadOnlyList<Model>>(entityList).AsQueryable().ApplySort(orderBy).ToListAsync();
                    return Response(modelList);
                }
            );
        public virtual async Task<IServiceResponseList<Model>> Get(string orderBy, IPaginationFilter paginationFilter)
            => await ExecuteAndHandleError(async () =>
                {
                    IReadOnlyList<Entity> entityList = await _repositorio.Get();
                    IEnumerable<Model> modelList = await _mapper.Map<IReadOnlyList<Model>>(entityList).AsQueryable().ApplySort(orderBy).ToListAsync(paginationFilter);
                    return Response(modelList);
                }
            );
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(orderBy.Compile())));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(orderBy.Compile(), paginationFilter)));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(filter)));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, IPaginationFilter paginationFilter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(filter, paginationFilter)));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, IFilterQueryParams queryParams)
            => await ExecuteAndHandleError(async () =>
                {
                    IReadOnlyList<Entity> entityList = await _repositorio.Get(filter);
                    IEnumerable<Model> modelList = await _mapper.Map<IReadOnlyList<Model>>(entityList).AsQueryable().ApplySort(queryParams?.OrderBy).ToListAsync(queryParams);
                    return Response(modelList);
                }
            );
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(filter, orderBy.Compile())));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, Expression<Func<IQueryable<Entity>, IOrderedQueryable<Entity>>> orderBy, IPaginationFilter paginationFilter)
            => await ExecuteAndHandleError(async () => Response(await _repositorio.Get(filter, orderBy.Compile(), paginationFilter)));
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, string orderBy)
            => await ExecuteAndHandleError(async () => 
                {
                    IReadOnlyList<Entity> entityList = await _repositorio.Get(filter);
                    IEnumerable<Model> modelList = await _mapper.Map<IReadOnlyList<Model>>(entityList).AsQueryable().ApplySort(orderBy).ToListAsync();
                    return Response(modelList);
                }
            );
        public virtual async Task<IServiceResponseList<Model>> Get(Expression<Func<Entity, bool>> filter, string orderBy, IPaginationFilter paginationFilter)
           => await ExecuteAndHandleError(async () => 
               {
                   IReadOnlyList<Entity> entityList = await _repositorio.Get(filter);
                   IEnumerable<Model> modelList = await _mapper.Map<IReadOnlyList<Model>>(entityList).AsQueryable().ApplySort(orderBy).ToListAsync(paginationFilter);
                   return Response(modelList);
               }
           );
    }
}
