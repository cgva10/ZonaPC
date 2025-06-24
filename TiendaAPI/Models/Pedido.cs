using TiendaComponentes.Shared.DTO;

namespace TiendaAPI.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Provincia { get; set; }
        public string Localidad { get; set; }
        public decimal CostoEnvio { get; set; }


        public EstadoPedido Estado { get; set; } = EstadoPedido.Pendiente;

        public required ICollection<DetallePedido> Detalles { get; set; }
    }
}
