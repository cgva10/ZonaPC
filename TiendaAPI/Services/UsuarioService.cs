using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TiendaComponentes.Shared.DTOs;
using TiendaAPI.Models;
using TiendaAPI.Settings;

namespace TiendaAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly TiendaDbContext _context;
        private readonly JwtSettings _jwtSettings;

        public UsuarioService(TiendaDbContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        public async Task<Usuario?> RegistrarUsuarioAsync(UsuarioRegistroDTO dto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
                return null;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Contrasena);

            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Contrasena = hashedPassword,
                Rol = string.IsNullOrWhiteSpace(dto.Rol) ? "user" : dto.Rol
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioRespuestaDTO?> LoginUsuarioAsync(UsuarioLoginDTO dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Contrasena, usuario.Contrasena))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nombre),  // Asegurate de que esté incluido
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Rol),
        }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new UsuarioRespuestaDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Token = tokenHandler.WriteToken(token),
                Rol = usuario.Rol
            };
        }


        public async Task<Usuario?> ValidarUsuarioAsync(UsuarioLoginDTO dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Contrasena, usuario.Contrasena))
                return null;

            return usuario;
        }

    }
}
