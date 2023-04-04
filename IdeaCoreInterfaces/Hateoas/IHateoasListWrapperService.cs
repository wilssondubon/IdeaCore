using IdeaCoreInterfaces.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IdeaCoreInterfaces.Hateoas
{
    public interface IHateoasListWrapperService
    {
        IHttpContextAccessor HttpContextAccessor { get; }
        IHateoasListWrapper<Model> Wrap<Model>(IEnumerable<Model> embed) where Model : class;
        IHateoasListWrapper<Model> AddSelfLink<Model>(IHateoasListWrapper<Model> wrapper) where Model : class;
        IHateoasListWrapper<Model> AddPagination<Model>(IHateoasListWrapper<Model> wrapper, int totalRecords, IPaginationFilter paginationFilter = null) where Model : class;
        IHateoasListWrapper<Model> AddLink<Model>(IHateoasListWrapper<Model> wrapper, ILink link) where Model : class;
        bool isHateoasRequest();
    }
}