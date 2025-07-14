using System.Text.Json;
using App_Calorias.Models;

namespace App_Calorias.Helpers;

public static class FileHelper
{
    private static readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "usuarios.json");

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
}
