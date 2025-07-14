using SQLite;
using App_Calorias.Models;

namespace App_Calorias.Services;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _db;

    public DatabaseService(string dbPath)
    {
        _db = new SQLiteAsyncConnection(dbPath);
        _db.CreateTableAsync<UsuarioSimple>().Wait();
    }

    public Task<List<UsuarioSimple>> GetUsuariosAsync() => _db.Table<UsuarioSimple>().ToListAsync();
    public Task<int> AddUsuarioAsync(UsuarioSimple u) => _db.InsertAsync(u);
    public Task<int> UpdateUsuarioAsync(UsuarioSimple u) => _db.UpdateAsync(u);
    public Task<int> DeleteUsuarioAsync(UsuarioSimple u) => _db.DeleteAsync(u);
}
