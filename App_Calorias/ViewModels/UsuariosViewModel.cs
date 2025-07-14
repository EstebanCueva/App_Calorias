using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using App_Calorias.Models;
using App_Calorias.Helpers;
using App_Calorias.Services;
using App_Calorias.Views;

namespace App_Calorias.ViewModels;

public class UsuariosViewModel : INotifyPropertyChanged
{
    public ObservableCollection<UsuarioSimple> Usuarios { get; } = new();

    private readonly HttpClient _httpClient = new();
    private readonly DatabaseService _db;

    public ICommand CargarUsuariosCommand { get; }
    public ICommand EliminarUsuarioCommand { get; }
    public ICommand ExportarCommand { get; }
    public ICommand ImportarCommand { get; }
    public ICommand EditarUsuarioCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    public UsuariosViewModel(DatabaseService db)
    {
        _db = db;

        CargarUsuariosCommand = new Command(async () => await CargarUsuariosAsync());
        EliminarUsuarioCommand = new Command<UsuarioSimple>(async u => await EliminarUsuario(u));
        ExportarCommand = new Command(async () => await ExportarAsync());
        ImportarCommand = new Command(async () => await ImportarAsync());
        EditarUsuarioCommand = new Command<UsuarioSimple>(async (usuario) => await EditarUsuario(usuario));
    }

    public async Task CargarUsuariosAsync()
    {
        try
        {
            Usuarios.Clear();

            var lista = await _httpClient.GetFromJsonAsync<List<UsuarioSimple>>("https://localhost:7118/api/Testdb");

            if (lista != null)
            {
                var locales = await _db.GetUsuariosAsync();

                foreach (var u in lista)
                {
                    Usuarios.Add(u);

                    bool yaExiste = locales.Any(l => l.Id == u.Id);

                    if (!yaExiste)
                    {
                        await _db.AddUsuarioAsync(u);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Fallo la carga: {ex.Message}", "OK");

            var locales = await _db.GetUsuariosAsync();
            foreach (var u in locales)
                Usuarios.Add(u);
        }
    }

    private async Task EliminarUsuario(UsuarioSimple usuario)
    {
        bool confirm = await Application.Current.MainPage.DisplayAlert("Confirmar",
                          $"¿Eliminar usuario {usuario.Name}?", "Sí", "No");

        if (!confirm) return;

        try
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7118/api/Testdb/{usuario.Id}");
            if (response.IsSuccessStatusCode)
            {
                Usuarios.Remove(usuario);
                await _db.DeleteUsuarioAsync(usuario);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar", "OK");
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async Task ExportarAsync()
    {
        await FileHelper.ExportarAsync(Usuarios.ToList());
        await Application.Current.MainPage.DisplayAlert("Exportado", "Archivo guardado", "OK");
    }

    private async Task ImportarAsync()
    {
        var importados = await FileHelper.ImportarAsync();
        Usuarios.Clear();
        foreach (var u in importados)
            Usuarios.Add(u);
    }

    private async Task EditarUsuario(UsuarioSimple usuario)
    {
        if (usuario == null)
            return;

        await Shell.Current.Navigation.PushAsync(new EditarUsuarioPage(usuario));
    }

    void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
