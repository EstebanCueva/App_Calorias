using System.Net.Http.Json;

namespace App_Calorias;

public partial class EditarUsuarioPage : ContentPage
{
    private readonly HttpClient _httpClient = new();
    private readonly UsuarioSimple _usuario;

    public EditarUsuarioPage(UsuarioSimple usuario)
    {
        InitializeComponent();
        _usuario = usuario;

        nameEntry.Text = usuario.Name;
        ageEntry.Text = usuario.Age.ToString();
        descriptionEntry.Text = usuario.Description;
        activityEntry.Text = usuario.Activity;
        sexEntry.Text = usuario.Sex;
        weightEntry.Text = usuario.Weight.ToString();
        heightEntry.Text = usuario.Height.ToString();
        idDietaEntry.Text = usuario.IdDieta.ToString();
    }

    private async void OnActualizarClicked(object sender, EventArgs e)
    {
        try
        {
            var actualizado = new
            {
                Id = _usuario.Id,
                Name = nameEntry.Text,
                Age = int.Parse(ageEntry.Text),
                Description = descriptionEntry.Text,
                Activity = activityEntry.Text,
                Sex = sexEntry.Text,
                Weight = int.Parse(weightEntry.Text),
                Height = int.Parse(heightEntry.Text),
                IdDieta = int.Parse(idDietaEntry.Text)
            };

            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7118/api/testdb/{_usuario.Id}", actualizado);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Usuario actualizado", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo actualizar", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
