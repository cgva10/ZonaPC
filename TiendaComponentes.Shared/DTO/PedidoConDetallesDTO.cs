using System;
using System.Collections.Generic;
using System.Linq;
using TiendaComponentes.Shared.DTO;


namespace TiendaComponentes.Shared.DTOs
{
    public class PedidoConDetallesDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetallePedidoDTO> Detalles { get; set; } = new();
        public int UsuarioId { get; set; }
        public string? NombreUsuario { get; set; }
        public string Provincia { get; set; } = string.Empty;
        public string Localidad { get; set; } = string.Empty;

        public decimal CostoEnvio { get; set; }

        public decimal Total => Math.Round(Detalles.Sum(d => d.Subtotal) + CostoEnvio, 2);
        public EstadoPedido Estado { get; set; } = EstadoPedido.Pendiente;
    }

    public class DetallePedidoDTO
    {
        public string NombreProducto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
        public List<string> ImagenesUrl { get; set; } = new();

        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
