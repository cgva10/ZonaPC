namespace TiendaComponentes.Shared.DTOs
{
    public class ProductoCreacionDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Subcategoria { get; set; } = string.Empty;

        // Imagen principal (obligatoria)
        public string ImagenUrl { get; set; } = string.Empty;

        // Imágenes adicionales (opcional)
        public List<string> ImagenesUrl { get; set; } = new();
    }
}
