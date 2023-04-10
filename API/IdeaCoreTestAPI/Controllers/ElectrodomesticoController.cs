using DTOs;
using Entities;
using IdeaCoreApplication.Contracts;
using IdeaCoreModels;
using IdeaCorePresentation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdeaCoreTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectrodomesticoController : CRUDController<ElectrodomesticoDTO, Electrodomestico>
    {
        public ElectrodomesticoController(ILogger<ElectrodomesticoController> logger, IAppServices service)
            : base(logger, service.Service<ElectrodomesticoDTO, Electrodomestico>(t=>t.Include(i=>i.IdTipoNavigation)))
        {
        }

        [HttpGet("ById/{Codigo}")]
        public async Task<IActionResult> GetById(short Codigo)
            => await _FindByID(Codigo);

        [HttpGet("All")]
        public async Task<IActionResult> GetAll([FromQuery] FilterQueryParams filterparams = null)
           => await _Get(filterparams);

        [HttpGet("Query")]
        public async Task<IActionResult> GetListByQuery([FromQuery] IDictionary<string, string> filter, [FromQuery] FilterQueryParams filterparams = null)
            => await _Get(filter, filterparams);

        [HttpGet("ByTipo/{idTipo}")]
        public async Task<IActionResult> GetByTipo(short IdTipo, [FromQuery] FilterQueryParams filterparams = null)
          => await _Get((ElectrodomesticoDTO t)=> t.IdTipo == IdTipo, filterparams);

        [HttpPost("SaveNew")]
        public async Task<IActionResult> SaveNew([FromBody] ElectrodomesticoDTO data)
            => await _SaveNew(data);

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ElectrodomesticoDTO data)
            => await _Update(data);

        [HttpDelete("Remove/{Codigo}")]
        public async Task<IActionResult> Remove(short Codigo)
            => await _RemoveEntity(Codigo);

        protected override string GetByIdActionName() => nameof(GetById);
    }
}
