namespace TiendaComponentes.Shared.DTOs

{
    public class UsuarioRespuestaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Rol { get; set; }
    }
}
