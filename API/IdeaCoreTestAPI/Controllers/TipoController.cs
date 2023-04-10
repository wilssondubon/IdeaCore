using DTOs;
using Entities;
using IdeaCoreApplication.Contracts;
using IdeaCoreModels;
using IdeaCorePresentation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdeaCoreTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoController : CRUDController<TipoDTO, Tipo>
    {
        public TipoController(ILogger<TipoController> logger, IAppServices service)
            :base(logger, service.Service<TipoDTO, Tipo>()) { 
        }

        [HttpGet("ById/{IdTipo}")]
        public async Task<IActionResult> GetById(short IdTipo)
            => await _FindByID(IdTipo);

        [HttpGet("All")]
        public async Task<IActionResult> GetAll([FromQuery] FilterQueryParams filterparams = null)
           => await _Get(filterparams);

        [HttpGet("Query")]
        public async Task<IActionResult> GetListByQuery([FromQuery] IDictionary<string, string> filter, [FromQuery] FilterQueryParams filterparams = null)
            => await _Get(filter, filterparams);

        [HttpPost("SaveNew")]
        public async Task<IActionResult> SaveNew([FromBody] TipoDTO data)
            => await _SaveNew(data);

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] TipoDTO data)
            => await _Update(data);

        [HttpDelete("Remove/{IdTipo}")]
        public async Task<IActionResult> Remove(short IdTipo)
            => await _RemoveEntity(IdTipo);

        protected override string GetByIdActionName() => nameof(GetById);
    }
}
