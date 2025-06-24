using System.ComponentModel.DataAnnotations;

namespace TiendaComponentes.Shared.DTOs

{
    public class UsuarioRegistroDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El email no es válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Contrasena { get; set; } = string.Empty;

        public string Rol { get; set; } = "user";

    }
}
