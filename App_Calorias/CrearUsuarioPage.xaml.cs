using System.Net.Http.Json;

namespace App_Calorias;

public partial class CrearUsuarioPage : ContentPage
{
    private readonly HttpClient _httpClient = new();

    public CrearUsuarioPage()
    {
        InitializeComponent();
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        try
        {
            var nuevoUsuario = new
            {
                Name = nameEntry.Text,
                Age = int.Parse(ageEntry.Text),
                Description = descriptionEntry.Text,
                Activity = activityEntry.Text,
                Sex = sexEntry.Text,
                Weight = int.Parse(weightEntry.Text),
                Height = int.Parse(heightEntry.Text),
                IdDieta = int.Parse(idDietaEntry.Text)
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7118/api/testdb", nuevoUsuario);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Usuario creado correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo crear el usuario", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
