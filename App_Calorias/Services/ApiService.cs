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
            BaseAddress = new Uri("https://localhost:7118/")
        };
    }

    // Crear usuario - ruta relativa api/TestDb
    public async Task<bool> CrearUsuarioAsync(UsuarioSimple usuario)
    {
        var response = await _httpClient.PostAsJsonAsync("api/TestDb", usuario);
        return response.IsSuccessStatusCode;
    }

    // Crear dieta - ruta relativa api/DietasApi
    public async Task<bool> CrearDietaAsync(Dieta dieta)
    {
        var response = await _httpClient.PostAsJsonAsync("api/DietasApi", dieta);
        return response.IsSuccessStatusCode;
    }
}
