using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using App_Calorias.Models;
using App_Calorias.Services;

namespace App_Calorias.ViewModels;

public class DietaViewModel : INotifyPropertyChanged
{
    private readonly ApiService _apiService = new();

    public Dieta Dieta { get; set; } = new();

    public ICommand CrearDietaCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    public DietaViewModel()
    {
        CrearDietaCommand = new Command(async () => await CrearDietaAsync());
    }

    public event Action DietaCreadaConExito;

    private async Task CrearDietaAsync()
    {
        bool exito = await _apiService.CrearDietaAsync(Dieta);
        if (exito)
        {
            await Application.Current.MainPage.DisplayAlert("Éxito", "Dieta creada", "OK");
            DietaCreadaConExito?.Invoke();
            LimpiarCampos();
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo crear la dieta", "OK");
        }
    }

    private void LimpiarCampos()
    {
        Dieta = new Dieta();
        OnPropertyChanged(nameof(Dieta));
    }

    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
