using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TiendaAPI.Services;
using TiendaComponentes.Shared.DTOs;

namespace TiendaAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // POST: api/pedidos/finalizar
        [Authorize]
        [HttpPost("finalizar")]
        public async Task<IActionResult> FinalizarPedido([FromBody] PedidoInputDTO pedidoInput)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var (exito, error) = await _pedidoService.FinalizarCompraAsync(userId, pedidoInput.Provincia, pedidoInput.Localidad);

            if (!exito)
                return BadRequest(new { mensaje = error });

            return Ok(new { mensaje = "Pedido registrado correctamente." });

        }

        // GET: api/pedidos (usuario común)
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<PedidoConDetallesDTO>>> ObtenerPedidosUsuario()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var pedidos = await _pedidoService.ObtenerPedidosPorUsuarioAsync(userId);
            return Ok(pedidos);
        }

        // GET: api/pedidos/admin (todos los pedidos)
        [Authorize(Roles = "admin")]
        [HttpGet("admin")]
        public async Task<ActionResult<List<PedidoConDetallesDTO>>> ObtenerTodosLosPedidos()
        {
            var pedidos = await _pedidoService.ObtenerTodosLosPedidosSinFiltrosAsync();
            return Ok(pedidos);
        }

        // PUT: api/pedidos/{id}/estado (cambio de estado)
        [Authorize(Roles = "admin")]
        [HttpPut("{id}/estado")]
        public async Task<IActionResult> CambiarEstadoPedido(int id, [FromBody] string nuevoEstado)
        {
            var ok = await _pedidoService.ActualizarEstadoPedidoAsync(id, nuevoEstado);
            if (!ok)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/pedidos/{id}
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPedido(int id)
        {
            var ok = await _pedidoService.EliminarPedidoAsync(id);
            if (!ok)
                return NotFound();

            return NoContent();
        }


    }
}
