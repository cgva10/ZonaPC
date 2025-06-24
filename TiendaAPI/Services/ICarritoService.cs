using TiendaAPI.Models;
using TiendaComponentes.Shared.DTOs;

public interface ICarritoService
{
    Task<CarritoDTO> ObtenerCarritoDTOAsync(int? usuarioId, string? sesionId);
    Task<bool> AgregarProductoAlCarritoAsync(int? usuarioId, string? sesionId, int productoId, int cantidad);
    Task<bool> CambiarCantidadProductoAsync(int? usuarioId, string? sesionId, int productoId, int cantidad);
    Task<bool> EliminarProductoDelCarritoAsync(int? usuarioId, string? sesionId, int productoId);
    Task SincronizarCarritoAnonimoConUsuario(int usuarioId, string sesionId);


}
