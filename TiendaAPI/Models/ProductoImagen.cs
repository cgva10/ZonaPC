namespace TiendaAPI.Models
{
    public class ProductoImagen
    {
        public int Id { get; set; }

        public string Url { get; set; } = string.Empty;

        // Clave foránea
        public int ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;
    }
}
