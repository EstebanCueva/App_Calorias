using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace App_Calorias;

public partial class UsuariosPage : ContentPage
{
    public ObservableCollection<UsuarioSimple> Usuarios { get; set; } = new();

    private readonly HttpClient _httpClient = new();

    public UsuariosPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarUsuarios();
    }

    private async Task CargarUsuarios()
    {
        try
        {
            string url = "https://localhost:7118/api/testdb";
            var lista = await _httpClient.GetFromJsonAsync<List<UsuarioSimple>>(url);

            if (lista != null)
            {
                Usuarios.Clear();
                foreach (var u in lista)
                {
                    Usuarios.Add(u);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cargar la lista: {ex.Message}", "OK");
        }
    }

    private async void OnCrearClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CrearUsuarioPage());
    }

    private async void OnEditarClicked(object sender, EventArgs e)ego el fly
    {
        if (sender is Button button && button.BindingContext is UsuarioSimple usuario)
        {
            await Navigation.PushAsync(new EditarUsuarioPage());
        }
    }

    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is UsuarioSimple usuario)
        {
            bool confirm = await DisplayAlert("Confirmar", $"¿Eliminar usuario {usuario.Name}?", "Sí", "No");
            if (confirm)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync($"https://localhost:7118/api/testdb/{usuario.Id}");
                    if (response.IsSuccessStatusCode)
                    {
                        Usuarios.Remove(usuario);
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo eliminar el usuario", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }
    }
}

public class UsuarioSimple
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Description { get; set; }
    public string Activity { get; set; }
    public string Sex { get; set; }
    public int Weight { get; set; }
    public int Height { get; set; }
    public int TotalCalories { get; set; }
    public int IdDieta { get; set; }
    public Dieta Dieta { get; set; }
}

public class Dieta
{
    public int Id { get; set; }
    public string NameDiet { get; set; }
    public string DescriptionDiet { get; set; }
    public int Calories { get; set; }
    public int Proteins { get; set; }
    public int Carbohydrates { get; set; }
    public string DietType { get; set; }
}