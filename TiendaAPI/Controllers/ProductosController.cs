using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaComponentes.Shared.DTOs;
using TiendaAPI.Services;

namespace TiendaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        // GET: api/producto
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var productos = await _productoService.ObtenerTodosProductosAsync();
            return Ok(productos);
        }

        // GET: api/producto/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var producto = await _productoService.ObtenerProductoPorIdAsync(id);
            if (producto == null)
            {
                return NotFound(new { mensaje = "Producto no encontrado" });
            }
            return Ok(producto);
        }

        // POST: api/producto
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProductoCreacionDTO dto)
        {
            var productoCreado = await _productoService.CrearProductoAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = productoCreado.Id }, productoCreado);
        }

        [Authorize(Roles = "admin")]
        // PUT: api/producto/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ProductoCreacionDTO dto)
        {
            var actualizado = await _productoService.ActualizarProductoAsync(id, dto);
            if (!actualizado)
                return NotFound(new { mensaje = "Producto no encontrado" });

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        // DELETE: api/producto/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _productoService.EliminarProductoAsync(id);
            if (!eliminado)
                return NotFound(new { mensaje = "Producto no encontrado" });

            return NoContent();
        }


        [HttpGet("filtrar")]
        public async Task<IActionResult> Filtrar([FromQuery] string? categoria, [FromQuery] string? subcategoria)
        {
            var productos = await _productoService.FiltrarProductosAsync(categoria, subcategoria);
            return Ok(productos);
        }

    }
}
