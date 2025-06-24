using Microsoft.EntityFrameworkCore;
using TiendaAPI.Models;
using TiendaComponentes.Shared.DTO;
using TiendaComponentes.Shared.DTOs;

namespace TiendaAPI.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly TiendaDbContext _dbContext;

        public PedidoService(TiendaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(bool exito, string? error)> FinalizarCompraAsync(int usuarioId, string provincia, string localidad)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            var carrito = await _dbContext.Carritos
                .Include(c => c.Productos)
                    .ThenInclude(cp => cp.Producto)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

            if (carrito == null || !carrito.Productos.Any())
                return (false, "No hay productos en tu carrito.");

            var productoIds = carrito.Productos.Select(p => p.ProductoId).ToList();
            var placeholders = string.Join(",", productoIds);

            await _dbContext.Database.ExecuteSqlRawAsync(
                $"SELECT * FROM Productos WHERE Id IN ({placeholders}) FOR UPDATE");

            foreach (var item in carrito.Productos)
            {
                if (item.Producto.Stock < item.Cantidad)
                {
                    await transaction.RollbackAsync();
                    return (false, $"Stock insuficiente para '{item.Producto.Nombre}'. Disponible: {item.Producto.Stock}, Solicitado: {item.Cantidad}");
                }
            }

            var detalles = new List<DetallePedido>();
            foreach (var item in carrito.Productos)
            {
                detalles.Add(new DetallePedido
                {
                    ProductoId = item.ProductoId,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.Producto.Precio
                });

                item.Producto.Stock -= item.Cantidad;
            }

            var subtotal = detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
            var costoEnvio = CalcularCostoEnvio(provincia);

            var pedido = new Pedido
            {
                UsuarioId = usuarioId,
                Fecha = DateTime.Now,
                Provincia = provincia,
                Localidad = localidad,
                CostoEnvio = costoEnvio,
                Total = subtotal + costoEnvio,
                Estado = EstadoPedido.Pendiente,
                Detalles = detalles
            };

            _dbContext.Pedidos.Add(pedido);
            _dbContext.CarritoProductos.RemoveRange(carrito.Productos);

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return (true, null);
        }



        public async Task<List<PedidoConDetallesDTO>> ObtenerPedidosPorUsuarioAsync(int usuarioId)
        {
            var pedidos = await _dbContext.Pedidos
                .Where(p => p.UsuarioId == usuarioId)
                .Include(p => p.Usuario)
                .Include(p => p.Detalles)
                    .ThenInclude(dp => dp.Producto)
                        .ThenInclude(p => p.Imagenes)
                .OrderByDescending(p => p.Fecha)
                .ToListAsync();

            return pedidos.Select(p => new PedidoConDetallesDTO
            {
                Id = p.Id,
                UsuarioId = p.UsuarioId,
                NombreUsuario = p.Usuario?.Nombre,
                Fecha = p.Fecha,
                Provincia = p.Provincia,
                Localidad = p.Localidad,
                Estado = p.Estado,
                CostoEnvio = p.CostoEnvio,
                Detalles = p.Detalles.Select(d => new DetallePedidoDTO
                {
                    NombreProducto = d.Producto.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    ImagenUrl = d.Producto.Imagenes.FirstOrDefault()?.Url ?? "",
                    ImagenesUrl = d.Producto.Imagenes.Select(i => i.Url).ToList(),
                }).ToList()
            }).ToList();
        }

        public async Task<List<PedidoConDetallesDTO>> ObtenerTodosLosPedidosSinFiltrosAsync()
        {
            var pedidos = await _dbContext.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.Detalles)
                    .ThenInclude(dp => dp.Producto)
                        .ThenInclude(p => p.Imagenes)
                .OrderByDescending(p => p.Fecha)
                .ToListAsync();

            return pedidos.Select(p => new PedidoConDetallesDTO
            {
                Id = p.Id,
                UsuarioId = p.UsuarioId,
                Fecha = p.Fecha,
                NombreUsuario = p.Usuario.Nombre,
                Provincia = p.Provincia,
                Localidad = p.Localidad,
                Estado = p.Estado,
                CostoEnvio = p.CostoEnvio,
                Detalles = p.Detalles.Select(d => new DetallePedidoDTO
                {
                    NombreProducto = d.Producto.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    ImagenUrl = d.Producto.Imagenes.FirstOrDefault()?.Url ?? "",
                    ImagenesUrl = d.Producto.Imagenes.Select(i => i.Url).ToList(),

                }).ToList()
            }).ToList();
        }

        public async Task<bool> ActualizarEstadoPedidoAsync(int pedidoId, string nuevoEstado)
        {
            var pedido = await _dbContext.Pedidos.FindAsync(pedidoId);
            if (pedido == null) return false;

            if (Enum.TryParse<EstadoPedido>(nuevoEstado, out var estado))
            {
                pedido.Estado = estado;
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<PedidoConDetallesDTO>> ObtenerTodosLosPedidosAsync(int? usuarioId, DateTime? desde, DateTime? hasta)
        {
            var query = _dbContext.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.Detalles)
                    .ThenInclude(dp => dp.Producto)
                        .ThenInclude(p => p.Imagenes)
                .AsQueryable();

            if (usuarioId.HasValue)
                query = query.Where(p => p.UsuarioId == usuarioId);

            if (desde.HasValue)
                query = query.Where(p => p.Fecha >= desde.Value);

            if (hasta.HasValue)
                query = query.Where(p => p.Fecha <= hasta.Value);

            var pedidos = await query.OrderByDescending(p => p.Fecha).ToListAsync();

            return pedidos.Select(p => new PedidoConDetallesDTO
            {
                Id = p.Id,
                UsuarioId = p.UsuarioId,
                Fecha = p.Fecha,
                NombreUsuario = p.Usuario.Nombre,
                Provincia = p.Provincia,
                Localidad = p.Localidad,
                Estado = p.Estado,
                CostoEnvio = p.CostoEnvio,
                Detalles = p.Detalles.Select(d => new DetallePedidoDTO
                {
                    NombreProducto = d.Producto.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    ImagenUrl = d.Producto.Imagenes.FirstOrDefault()?.Url ?? "",
                    ImagenesUrl = d.Producto.Imagenes.Select(i => i.Url).ToList(),

                }).ToList()
            }).ToList();
        }

        public async Task<bool> EliminarPedidoAsync(int id)
        {
            var pedido = await _dbContext.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return false;

            _dbContext.DetallesPedido.RemoveRange(pedido.Detalles);
            _dbContext.Pedidos.Remove(pedido);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private decimal CalcularCostoEnvio(string provincia)
        {
            return provincia switch
            {
                "Ciudad Autónoma de Buenos Aires" => 1000,
                "Buenos Aires" or "Santa Fe" => 1500,
                "Córdoba" => 1800,
                "Mendoza" => 1900,
                "Tucumán" => 2000,
                "San Juan" or "San Luis" or "La Rioja" or "Catamarca" or "Chaco" or "Santiago del Estero" => 2200,
                "Jujuy" or "Misiones" => 2300,
                "Neuquén" or "Río Negro" or "Formosa" => 2400,
                "Chubut" => 2500,
                "Santa Cruz" => 2700,
                "Tierra del Fuego, Antártida e Islas del Atlántico Sur" => 3000,
                "Corrientes" => 2100,
                "Entre Ríos" => 1900,
                "La Pampa" => 2000,
                _ => 2500
            };
        }


    }
}
