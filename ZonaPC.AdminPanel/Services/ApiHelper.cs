using System.Net.Http;

namespace ZonaPC.AdminPanel.Services
{
    public static class ApiHelper
    {
        public static HttpClient Client { get; set; }

        public static void InitializeClient(string token = null)
        {
            var handler = new HttpClientHandler
            {
                // ✅ Ignorar certificado en entorno local
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            Client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7144/api/")
            };

            if (!string.IsNullOrEmpty(token))
            {
                Client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
