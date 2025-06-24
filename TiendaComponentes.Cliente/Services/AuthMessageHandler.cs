// ✅ AuthMessageHandler.cs
using System.Net.Http.Headers;
using TiendaComponentes.Cliente.Services;

public class AuthMessageHandler : DelegatingHandler
{
    private readonly AuthService _authService;

    public AuthMessageHandler(AuthService authService)
    {
        _authService = authService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _authService.GetTokenAsync();

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
