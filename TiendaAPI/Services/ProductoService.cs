using Microsoft.EntityFrameworkCore;
using TiendaComponentes.Shared.DTOs;
using TiendaAPI.Models;

namespace TiendaAPI.Services
{
    public class ProductoService : IProductoService
    {
        private readonly TiendaDbContext _dbContext;

        public ProductoService(TiendaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductoDTO?> ObtenerProductoPorIdAsync(int id)
        {
            var producto = await _dbContext.Productos
                .Include(p => p.Imagenes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
                return null;

            return new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                Categoria = producto.Categoria,
                Subcategoria = producto.Subcategoria,
                ImagenUrl = producto.Imagenes.FirstOrDefault()?.Url ?? string.Empty,
                ImagenesUrl = producto.Imagenes.Select(i => i.Url).ToList()
            };
        }

        public async Task<IEnumerable<ProductoDTO>> ObtenerTodosProductosAsync()
        {
            var productos = await _dbContext.Productos
                .Include(p => p.Imagenes)
                .ToListAsync();

                return productos.Select(p => new ProductoDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    Categoria = p.Categoria,
                    Subcategoria = p.Subcategoria,
                    ImagenUrl = p.Imagenes.FirstOrDefault()?.Url ?? string.Empty,
                    ImagenesUrl = p.Imagenes.Select(i => i.Url).ToList()
                });
        }

        public async Task<ProductoDTO> CrearProductoAsync(ProductoCreacionDTO dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Stock = dto.Stock,
                Categoria = dto.Categoria,
                Subcategoria = dto.Subcategoria,
                Imagenes = dto.ImagenesUrl.Select(url => new ProductoImagen { Url = url }).ToList()
            };

            _dbContext.Productos.Add(producto);
            await _dbContext.SaveChangesAsync();

            return new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                Categoria = producto.Categoria,
                Subcategoria = producto.Subcategoria,
                ImagenUrl = producto.Imagenes.FirstOrDefault()?.Url ?? string.Empty,
                ImagenesUrl = producto.Imagenes.Select(i => i.Url).ToList()
            };
        }

        public async Task<bool> ActualizarProductoAsync(int id, ProductoCreacionDTO dto)
        {
            var producto = await _dbContext.Productos
                .Include(p => p.Imagenes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
                return false;

            producto.Nombre = dto.Nombre;
            producto.Descripcion = dto.Descripcion;
            producto.Precio = dto.Precio;
            producto.Stock = dto.Stock;
            producto.Categoria = dto.Categoria;
            producto.Subcategoria = dto.Subcategoria;

            _dbContext.ProductoImagenes.RemoveRange(producto.Imagenes);
            producto.Imagenes = dto.ImagenesUrl.Select(url => new ProductoImagen { Url = url }).ToList();

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarProductoAsync(int id)
        {
            var producto = await _dbContext.Productos.FindAsync(id);
            if (producto == null)
                return false;

            _dbContext.Productos.Remove(producto);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductoDTO>> FiltrarProductosAsync(string? categoria, string? subcategoria)
        {
            var query = _dbContext.Productos
                .Include(p => p.Imagenes)
                .AsQueryable();

            if (!string.IsNullOrEmpty(categoria))
                query = query.Where(p => p.Categoria == categoria);

            if (!string.IsNullOrEmpty(subcategoria))
                query = query.Where(p => p.Subcategoria == subcategoria);

            var productos = await query.ToListAsync();

            return productos.Select(p => new ProductoDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Stock = p.Stock,
                Categoria = p.Categoria,
                Subcategoria = p.Subcategoria,
                ImagenUrl = p.Imagenes.FirstOrDefault()?.Url ?? string.Empty,
                ImagenesUrl = p.Imagenes.Select(i => i.Url).ToList()
            });
        }
    }
}
