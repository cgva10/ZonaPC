using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaAPI.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public required string Categoria { get; set; }
        public required string Subcategoria { get; set; }
        public ICollection<ProductoImagen> Imagenes { get; set; } = new List<ProductoImagen>();

        public ICollection<DetallePedido> DetallesPedido { get; set; } = new List<DetallePedido>();
        public ICollection<CarritoProducto> CarritoProductos { get; set; } = new List<CarritoProducto>();
    }
}
