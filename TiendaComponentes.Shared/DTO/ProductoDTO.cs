namespace TiendaComponentes.Shared.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Subcategoria { get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = string.Empty;
        public List<string>? ImagenesUrl { get; set; }
        public string CategoriaAmigable => Categoria switch
        {
            "Notebook" => "Notebooks",
            "PlacaDeVideo" => "Placas de Video",
            _ => Categoria
        };
    }

}
