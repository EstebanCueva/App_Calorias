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
        _db.CreateTableAsync<Dieta>().Wait();
    }

    public Task<List<UsuarioSimple>> GetUsuariosAsync() => _db.Table<UsuarioSimple>().ToListAsync();
    public Task<int> AddUsuarioAsync(UsuarioSimple u) => _db.InsertAsync(u);
    public Task<int> UpdateUsuarioAsync(UsuarioSimple u) => _db.UpdateAsync(u);
    public Task<int> DeleteUsuarioAsync(UsuarioSimple u) => _db.DeleteAsync(u);

    public Task<List<Dieta>> GetDietaAsync() => _db.Table<Dieta>().ToListAsync();
    public Task<int> AddDietaAsync(Dieta d) => _db.InsertAsync(d);
    public Task<int> UpdateDietaAsync(Dieta d) => _db.UpdateAsync(d);
    public Task<int> DeleteDietaAsync(Dieta d) => _db.DeleteAsync(d);


}
