using App_Calorias.Models;
using System.Net.Http.Json;

namespace App_Calorias.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7118/api/testdb") // Coloca la URL real
        };
    }

    public async Task<bool> CrearUsuarioAsync(UsuarioSimple usuario)
    {
        var response = await _httpClient.PostAsJsonAsync("", usuario);
        return response.IsSuccessStatusCode;
    }
}
