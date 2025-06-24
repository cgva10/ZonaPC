using TiendaComponentes.Shared.DTOs;
using TiendaAPI.Models;

namespace TiendaAPI.Services
{
    public interface IUsuarioService
    {
        Task<Usuario?> RegistrarUsuarioAsync(UsuarioRegistroDTO dto);
        Task<Usuario?> ValidarUsuarioAsync(UsuarioLoginDTO dto);
        Task<UsuarioRespuestaDTO?> LoginUsuarioAsync(UsuarioLoginDTO dto);
    }
}
