using TiendaComponentes.Shared.DTOs;

namespace TiendaAPI.Services
{
    public interface IPedidoService
    {
        Task<(bool exito, string? error)> FinalizarCompraAsync(int userId, string provincia, string localidad);

        Task<List<PedidoConDetallesDTO>> ObtenerPedidosPorUsuarioAsync(int usuarioId);

        Task<List<PedidoConDetallesDTO>> ObtenerTodosLosPedidosAsync(int? usuarioId, DateTime? desde, DateTime? hasta);

        // winform
        Task<List<PedidoConDetallesDTO>> ObtenerTodosLosPedidosSinFiltrosAsync();
        Task<bool> ActualizarEstadoPedidoAsync(int pedidoId, string nuevoEstado);
        Task<bool> EliminarPedidoAsync(int id);

    }
}
