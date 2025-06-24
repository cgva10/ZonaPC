using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaAPI.Services;
using TiendaComponentes.Shared.DTO;

namespace TiendaAPI.Controllers
{
    [ApiController]
    [Route("api/envios")]
    public class EnviosController : ControllerBase
    {
        private readonly IGeoRefService _geoRefService;

        public EnviosController(IGeoRefService geoRefService)
        {
            _geoRefService = geoRefService;
        }

        // GET: api/envios/provincias
        [HttpGet("provincias")]
        public async Task<ActionResult<List<ProvinciaDTO>>> ObtenerProvincias()
        {
            var provincias = await _geoRefService.ObtenerProvinciasAsync();
            return Ok(provincias);
        }

        // GET: api/envios/departamentos?provinciaId=
        [HttpGet("departamentos")]
        public async Task<ActionResult<List<DepartamentoDTO>>> ObtenerDepartamentos([FromQuery] string provinciaId)
        {
            if (string.IsNullOrWhiteSpace(provinciaId))
                return BadRequest("Debe indicar un ID de provincia.");

            var departamentos = await _geoRefService.ObtenerDepartamentosAsync(provinciaId);
            return Ok(departamentos);
        }

        // GET: api/envios/localidades?provinciaId=
        [HttpGet("localidades")]
        public async Task<ActionResult<List<LocalidadDTO>>> ObtenerLocalidades([FromQuery] string provinciaId)
        {
            if (string.IsNullOrWhiteSpace(provinciaId))
                return BadRequest("Debe indicar un ID de provincia.");

            var localidades = await _geoRefService.ObtenerLocalidadesAsync(provinciaId);
            return Ok(localidades);
        }
    }
}
