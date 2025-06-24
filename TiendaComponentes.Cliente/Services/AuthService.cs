using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using TiendaComponentes.Shared.DTOs;


namespace TiendaComponentes.Cliente.Services
{
    public class AuthService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IHttpClientFactory _httpFactory;

        public AuthService(ILocalStorageService localStorage, IHttpClientFactory httpFactory)
        {
            _localStorage = localStorage;
            _httpFactory = httpFactory;
        }

        public async Task<LoginResponse?> Login(string email, string password, NavigationManager navigation)
        {
            var http = _httpFactory.CreateClient("ApiAutenticada");
            var loginDto = new { Email = email, Contrasena = password };

            var response = await http.PostAsJsonAsync("api/usuarios/login", loginDto);

            if (!response.IsSuccessStatusCode) return null;

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (loginResponse == null) return null;

            await _localStorage.SetItemAsync("authToken", loginResponse.Token);
            await _localStorage.SetItemAsync("userEmail", loginResponse.Email);
            await _localStorage.SetItemAsync("userNombre", loginResponse.Nombre);

            navigation.NavigateTo("/", forceLoad: true);
            return loginResponse;
        }

        public async Task<bool> Register(UsuarioRegistroDTO registro)
        {
            var http = _httpFactory.CreateClient("ApiAutenticada");

            var response = await http.PostAsJsonAsync("api/usuarios/registro", registro);
            return response.IsSuccessStatusCode;
        }


        public async Task<string?> GetTokenAsync() => await _localStorage.GetItemAsync<string>("authToken");

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("userEmail");
            await _localStorage.RemoveItemAsync("userNombre");
        }
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
