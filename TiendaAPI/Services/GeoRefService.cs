using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TiendaComponentes.Shared.DTO;

namespace TiendaAPI.Services
{
    public interface IGeoRefService
    {
        Task<List<ProvinciaDTO>> ObtenerProvinciasAsync();
        Task<List<DepartamentoDTO>> ObtenerDepartamentosAsync(string provinciaId);
        Task<List<LocalidadDTO>> ObtenerLocalidadesAsync(string provinciaId);
    }

    public class GeoRefService : IGeoRefService
    {
        private readonly HttpClient _httpClient;

        public GeoRefService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://apis.datos.gob.ar/georef/api/");
        }

        public async Task<List<ProvinciaDTO>> ObtenerProvinciasAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<GeoRefResponse<ProvinciaDTO>>("provincias?campos=id,nombre");
            return response?.Provincias ?? new List<ProvinciaDTO>();
        }

        public async Task<List<DepartamentoDTO>> ObtenerDepartamentosAsync(string provinciaId)
        {
            var url = $"departamentos?provincia={provinciaId}&campos=id,nombre&max=100";
            var response = await _httpClient.GetFromJsonAsync<GeoRefResponse<DepartamentoDTO>>(url);
            return response?.Departamentos ?? new List<DepartamentoDTO>();
        }

        public async Task<List<LocalidadDTO>> ObtenerLocalidadesAsync(string provinciaId)
        {
            var url = $"localidades?provincia={provinciaId}&campos=id,nombre&max=500";
            var response = await _httpClient.GetFromJsonAsync<GeoRefResponse<LocalidadDTO>>(url);
            return response?.Localidades ?? new List<LocalidadDTO>();
        }
    }
}
