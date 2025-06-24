public class ItemCarritoDTO
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; } = "";
    public string ImagenUrl { get; set; } = "";
    public List<string> ImagenesUrl { get; set; } = new();
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public int Stock { get; set; }

}
