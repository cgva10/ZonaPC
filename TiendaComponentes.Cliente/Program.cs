using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TiendaComponentes.Cliente.Services;
using TiendaComponentes.Cliente;
using Blazored.LocalStorage;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Cultura
var culture = new CultureInfo("es-AR");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

// LocalStorage y autenticacion
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

// Servicio de autenticacion
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Handler para token
builder.Services.AddScoped<AuthMessageHandler>();

// HttpClient con autenticación (para API)
builder.Services.AddHttpClient("ApiAutenticada", client =>
{
    client.BaseAddress = new Uri("https://localhost:7144/");
}).AddHttpMessageHandler<AuthMessageHandler>();

await builder.Build().RunAsync();
