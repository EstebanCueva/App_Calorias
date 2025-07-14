using App_Calorias.Services;
using App_Calorias.ViewModels;

namespace App_Calorias;

public partial class DietasPage : ContentPage
{
    private readonly DatabaseService _dbService;

    public DietasPage()
    {
        InitializeComponent();

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "dietas.db3");
        _dbService = new DatabaseService(dbPath);

        BindingContext = new DietasViewModel(_dbService);

        ((DietasViewModel)BindingContext).CargarDietasCommand.Execute(null);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is DietasViewModel vm)
        {
            await vm.CargarDietasAsync();
        }
    }

    private async void OnCrearDietaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CrearDietaPage());
    }
}
