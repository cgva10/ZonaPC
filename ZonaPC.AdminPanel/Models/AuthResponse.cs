﻿namespace ZonaPC.AdminPanel.Models
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Rol { get; set; }
    }
}
