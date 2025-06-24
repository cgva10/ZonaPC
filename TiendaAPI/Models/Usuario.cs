using System.ComponentModel.DataAnnotations;

namespace TiendaAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }  // Clave primaria
        public string Rol { get; set; } = "user"; // valores posibles: "user", "admin"

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Contrasena { get; set; } = string.Empty;

        // Relaciones
        public ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
