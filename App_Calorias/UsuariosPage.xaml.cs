using App_Calorias.Services;
using App_Calorias.ViewModels;

namespace App_Calorias;

public partial class UsuariosPage : ContentPage
{
    private readonly DatabaseService _dbService;

    public UsuariosPage()
    {
        InitializeComponent();

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "usuarios.db3");
        _dbService = new DatabaseService(dbPath);

        BindingContext = new UsuariosViewModel(_dbService);

        ((UsuariosViewModel)BindingContext).CargarUsuariosCommand.Execute(null);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is UsuariosViewModel vm)
        {
            await vm.CargarUsuariosAsync();
        }
    }

    private async void OnCrearUsuarioClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CrearUsuarioPageNEW());
    }
}
