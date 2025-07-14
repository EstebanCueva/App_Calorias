using System.Text.Json;
using App_Calorias.Models;

namespace App_Calorias.Helpers;

public static class FileHelper
{
    private static readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "usuarios.json");
    private static readonly string filePath1 = Path.Combine(FileSystem.AppDataDirectory, "dietas.json");


    public static async Task ExportarAsync(List<UsuarioSimple> usuarios)
    {
        string json = JsonSerializer.Serialize(usuarios);
        await File.WriteAllTextAsync(filePath, json);
    }

    public static async Task<List<UsuarioSimple>> ImportarAsync()
    {
        if (!File.Exists(filePath))
            return new List<UsuarioSimple>();

        string json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<UsuarioSimple>>(json);
    }
    public static async Task ExportarDietaAsync(List<Dieta> dietas)
    {
        string json = JsonSerializer.Serialize(dietas);
        await File.WriteAllTextAsync(filePath1, json);
    }

    public static async Task<List<Dieta>> ImportarDietaAsync()
    {
        if (!File.Exists(filePath1))
            return new List<Dieta>();

        string json = await File.ReadAllTextAsync(filePath1);
        return JsonSerializer.Deserialize<List<Dieta>>(json);
    }
}
