using TiendaComponentes.Shared.DTOs;

namespace TiendaAPI.Services
{
    public interface IProductoService
    {
        Task<ProductoDTO?> ObtenerProductoPorIdAsync(int id);
        Task<IEnumerable<ProductoDTO>> ObtenerTodosProductosAsync();
        Task<ProductoDTO> CrearProductoAsync(ProductoCreacionDTO dto);
        Task<bool> ActualizarProductoAsync(int id, ProductoCreacionDTO dto);
        Task<bool> EliminarProductoAsync(int id);
        Task<IEnumerable<ProductoDTO>> FiltrarProductosAsync(string? categoria, string? subcategoria);


    }
}
