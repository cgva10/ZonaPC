using Microsoft.AspNetCore.Mvc;
using TiendaComponentes.Shared.DTOs;
using TiendaAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace TiendaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("registro")]
        [AllowAnonymous]
        public async Task<IActionResult> Registrar([FromBody] UsuarioRegistroDTO dto)
        {
            var usuario = await _usuarioService.RegistrarUsuarioAsync(dto);
            if (usuario == null)
            {
                return BadRequest(new { mensaje = "Ya existe un usuario con ese email." });
            }

            return Ok(new { mensaje = "Usuario registrado exitosamente", usuario.Id, usuario.Nombre, usuario.Email });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO dto)
        {
            var respuesta = await _usuarioService.LoginUsuarioAsync(dto);
            if (respuesta == null)
            {
                return Unauthorized(new { mensaje = "Credenciales inválidas." });
            }

            return Ok(respuesta);
        }

    }
}
