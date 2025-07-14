using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using App_Calorias.Models;

namespace App_Calorias.ViewModels;

public class EditarUsuarioViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private readonly HttpClient _httpClient = new();

    public UsuarioSimple Usuario { get; set; }

    public ICommand ActualizarCommand { get; }

    public EditarUsuarioViewModel(UsuarioSimple usuario)
    {
        Usuario = usuario;
        ActualizarCommand = new Command(async () => await Actualizar());
    }

    private async Task Actualizar()
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"https://localhost:7118/api/testdb/{Usuario.Id}", Usuario);

            if (response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Usuario actualizado", "OK");
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo actualizar", "OK");
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
