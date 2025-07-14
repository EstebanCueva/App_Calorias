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

public class DietasViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Dieta> Dietas { get; } = new();

    private readonly HttpClient _httpClient = new();
    private readonly DatabaseService _db;

    public ICommand CargarDietasCommand { get; }
    public ICommand EliminarDietaCommand { get; }
    public ICommand ExportarDietasCommand { get; }
    public ICommand ImportarDietasCommand { get; }
    public ICommand EditarDietaCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    public DietasViewModel(DatabaseService db)
    {
        _db = db;

        CargarDietasCommand = new Command(async () => await CargarDietasAsync());
        EliminarDietaCommand = new Command<Dieta>(async d => await EliminarDieta(d));
        ExportarDietasCommand = new Command(async () => await ExportarAsync());
        ImportarDietasCommand = new Command(async () => await ImportarAsync());
        EditarDietaCommand = new Command<Dieta>(async d => await EditarDieta(d));
    }

    public async Task CargarDietasAsync()
    {
        try
        {
            Dietas.Clear();

            var lista = await _httpClient.GetFromJsonAsync<List<Dieta>>("https://localhost:7118/api/DietasApi");

            if (lista != null)
            {
                var locales = await _db.GetDietaAsync();

                foreach (var d in lista)
                {
                    Dietas.Add(d);

                    bool yaExiste = locales.Any(l => l.Id == d.Id);

                    if (!yaExiste)
                    {
                        await _db.AddDietaAsync(d);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Fallo la carga: {ex.Message}", "OK");

            var locales = await _db.GetDietaAsync();
            foreach (var d in locales)
                Dietas.Add(d);
        }
    }

    private async Task EliminarDieta(Dieta dieta)
    {
        bool confirm = await Application.Current.MainPage.DisplayAlert("Confirmar",
                          $"¿Eliminar dieta {dieta.NameDiet}?", "Sí", "No");

        if (!confirm) return;

        try
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7118/api/DietasApi/{dieta.Id}");
            if (response.IsSuccessStatusCode)
            {
                Dietas.Remove(dieta);
                await _db.DeleteDietaAsync(dieta);
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
        await FileHelper.ExportarDietaAsync(Dietas.ToList());
        await Application.Current.MainPage.DisplayAlert("Exportado", "Archivo de dietas guardado", "OK");
    }

    private async Task ImportarAsync()
    {
        var importados = await FileHelper.ImportarDietaAsync();
        Dietas.Clear();
        foreach (var d in importados)
            Dietas.Add(d);
    }

    private async Task EditarDieta(Dieta dieta)
    {
        if (dieta == null)
            return;

        await Shell.Current.Navigation.PushAsync(new EditarDietaPage(dieta));
    }

    void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
