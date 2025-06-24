using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TiendaAPI.Services;

namespace TiendaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarritoController : ControllerBase
    {
        private readonly ICarritoService _carritoService;

        public CarritoController(ICarritoService carritoService)
        {
            _carritoService = carritoService;
        }

        private (int? usuarioId, string? sesionId) ObtenerIdentidad()
        {
            int? userId = null;
            if (User.Identity?.IsAuthenticated == true)
            {
                userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            }

            string? sesionId = Request.Headers["Sesion-Id"];
            return (userId, string.IsNullOrWhiteSpace(sesionId) ? null : sesionId);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerCarrito()
        {
            var (usuarioId, sesionId) = ObtenerIdentidad();
            var carrito = await _carritoService.ObtenerCarritoDTOAsync(usuarioId, sesionId);
            return Ok(carrito);
        }

        [HttpPost("agregar")]
        [AllowAnonymous]
        public async Task<IActionResult> AgregarProducto([FromBody] AgregarProductoDTO dto)
        {
            var (usuarioId, sesionId) = ObtenerIdentidad();
            var result = await _carritoService.AgregarProductoAlCarritoAsync(usuarioId, sesionId, dto.ProductoId, dto.Cantidad);
            return result ? Ok() : BadRequest("No se pudo agregar el producto al carrito");
        }

        [HttpPut("cambiar-cantidad")]
        [AllowAnonymous]
        public async Task<IActionResult> CambiarCantidad([FromBody] AgregarProductoDTO dto)
        {
            var (usuarioId, sesionId) = ObtenerIdentidad();
            var result = await _carritoService.CambiarCantidadProductoAsync(usuarioId, sesionId, dto.ProductoId, dto.Cantidad);
            return result ? Ok() : BadRequest("No se pudo cambiar la cantidad del producto");
        }

        [HttpDelete("eliminar/{productoId}")]
        [AllowAnonymous]
        public async Task<IActionResult> EliminarProducto(int productoId)
        {
            var (usuarioId, sesionId) = ObtenerIdentidad();
            var result = await _carritoService.EliminarProductoDelCarritoAsync(usuarioId, sesionId, productoId);
            return result ? Ok() : NotFound("Producto no encontrado en el carrito");
        }

        [Authorize]
        [HttpPost("sincronizar")]
        public async Task<IActionResult> SincronizarCarrito([FromHeader(Name = "Sesion-Id")] string sesionId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _carritoService.SincronizarCarritoAnonimoConUsuario(userId, sesionId);
            return Ok();
        }


    }

    public class AgregarProductoDTO
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; } = 1;
    }
}
