using Microsoft.EntityFrameworkCore;
using TiendaAPI.Models;
using TiendaComponentes.Shared.DTOs;

public class CarritoService : ICarritoService
{
    private readonly TiendaDbContext _context;

    public CarritoService(TiendaDbContext context)
    {
        _context = context;
    }

    private async Task<Carrito> ObtenerOCrearCarritoAsync(int? usuarioId, string? sesionId)
    {
        Carrito? carrito = null;

        if (usuarioId != null)
        {
            carrito = await _context.Carritos
                .Include(c => c.Productos)
                    .ThenInclude(cp => cp.Producto)
                        .ThenInclude(p => p.Imagenes)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId.Value);
        }
        else if (!string.IsNullOrWhiteSpace(sesionId))
        {
            carrito = await _context.Carritos
                .Include(c => c.Productos)
                    .ThenInclude(cp => cp.Producto)
                        .ThenInclude(p => p.Imagenes)
                .FirstOrDefaultAsync(c => c.SesionId == sesionId);
        }

        if (carrito == null)
        {
            carrito = new Carrito
            {
                UsuarioId = usuarioId,
                SesionId = sesionId,
                Productos = new List<CarritoProducto>()
            };
            _context.Carritos.Add(carrito);
            await _context.SaveChangesAsync();
        }

        return carrito;
    }

    public async Task<CarritoDTO> ObtenerCarritoDTOAsync(int? usuarioId, string? sesionId)
    {
        var carrito = await ObtenerOCrearCarritoAsync(usuarioId, sesionId);

        return new CarritoDTO
        {
            Productos = carrito.Productos.Select(cp => new TiendaComponentes.Shared.DTOs.ItemCarritoDTO
            {
                ProductoId = cp.ProductoId,
                Nombre = cp.Producto.Nombre,
                ImagenUrl = cp.Producto.Imagenes.FirstOrDefault()?.Url ?? "",
                ImagenesUrl = cp.Producto.Imagenes.Select(i => i.Url).ToList(),
                Precio = cp.Producto.Precio,
                Cantidad = cp.Cantidad,
                Stock = cp.Producto.Stock,
            }).ToList()

        };
    }

    public async Task<bool> AgregarProductoAlCarritoAsync(int? usuarioId, string? sesionId, int productoId, int cantidad)
    {
        var carrito = await ObtenerOCrearCarritoAsync(usuarioId, sesionId);

        var item = carrito.Productos.FirstOrDefault(p => p.ProductoId == productoId);
        if (item != null)
        {
            item.Cantidad += cantidad;
        }
        else
        {
            var nuevoItem = new CarritoProducto
            {
                ProductoId = productoId,
                Cantidad = cantidad,
                CarritoId = carrito.Id
            };

            _context.CarritoProductos.Add(nuevoItem);
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CambiarCantidadProductoAsync(int? usuarioId, string? sesionId, int productoId, int cantidad)
    {
        var carrito = await ObtenerOCrearCarritoAsync(usuarioId, sesionId);
        var item = carrito.Productos.FirstOrDefault(p => p.ProductoId == productoId);

        if (item == null) return false;

        item.Cantidad += cantidad;
        if (item.Cantidad <= 0)
        {
            carrito.Productos.Remove(item);
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarProductoDelCarritoAsync(int? usuarioId, string? sesionId, int productoId)
    {
        var carrito = await ObtenerOCrearCarritoAsync(usuarioId, sesionId);
        var item = carrito.Productos.FirstOrDefault(p => p.ProductoId == productoId);
        if (item == null) return false;

        carrito.Productos.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SincronizarCarritoAnonimoConUsuario(int usuarioId, string sesionId)
    {
        var anonimo = await _context.Carritos
            .Include(c => c.Productos)
            .FirstOrDefaultAsync(c => c.SesionId == sesionId && c.UsuarioId == null);

        if (anonimo == null) return;

        var usuario = await _context.Carritos
            .Include(c => c.Productos)
            .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

        if (usuario == null)
        {
            usuario = new Carrito
            {
                UsuarioId = usuarioId,
                Productos = new List<CarritoProducto>()
            };
            _context.Carritos.Add(usuario);
        }

        foreach (var item in anonimo.Productos)
        {
            var existente = usuario.Productos.FirstOrDefault(p => p.ProductoId == item.ProductoId);
            if (existente != null)
            {
                existente.Cantidad += item.Cantidad;
            }
            else
            {
                usuario.Productos.Add(new CarritoProducto
                {
                    ProductoId = item.ProductoId,
                    Cantidad = item.Cantidad
                });
            }
        }

        _context.Carritos.Remove(anonimo);
        await _context.SaveChangesAsync();
    }
}
