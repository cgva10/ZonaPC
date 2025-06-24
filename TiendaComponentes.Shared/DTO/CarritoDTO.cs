namespace TiendaComponentes.Shared.DTOs
{
    public class CarritoDTO
    {
        public List<ItemCarritoDTO> Productos { get; set; } = new();
    }


    public class ItemCarritoDTO
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = string.Empty;
        public List<string> ImagenesUrl { get; set; } = new();
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public int Stock { get; set; }
    }
}
