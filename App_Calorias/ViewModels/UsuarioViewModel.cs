using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using App_Calorias.Models;
using App_Calorias.Services;

namespace App_Calorias.ViewModels;

public class UsuarioViewModel : INotifyPropertyChanged
{
    private readonly ApiService _apiService = new();

    public UsuarioSimple Usuario { get; set; } = new();

    public ICommand CrearUsuarioCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    public UsuarioViewModel()
    {
        CrearUsuarioCommand = new Command(async () => await CrearUsuarioAsync());
    }

    public event Action UsuarioCreadoConExito;

    private async Task CrearUsuarioAsync()
    {
        bool exito = await _apiService.CrearUsuarioAsync(Usuario);
        if (exito)
        {
            await Application.Current.MainPage.DisplayAlert("Éxito", "Usuario creado", "OK");
            UsuarioCreadoConExito?.Invoke(); 
            LimpiarCampos();
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo crear el usuario", "OK");
        }
    }

    private void LimpiarCampos()
    {
        Usuario = new UsuarioSimple();
        OnPropertyChanged(nameof(Usuario));
    }

    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
