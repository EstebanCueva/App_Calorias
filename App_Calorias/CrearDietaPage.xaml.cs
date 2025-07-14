namespace App_Calorias;
using App_Calorias.ViewModels;
using App_Calorias.Models;

public partial class CrearDietaPage : ContentPage
{
    private readonly DietaViewModel _viewModel;

    public CrearDietaPage()
    {
        InitializeComponent();
        _viewModel = new DietaViewModel();
        BindingContext = _viewModel;

        _viewModel.DietaCreadaConExito += OnDietaCreadaConExito;
    }

    private async void OnDietaCreadaConExito()
    {
        await Shell.Current.GoToAsync("//DietasPage"); // Cambia la ruta según tu app
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.DietaCreadaConExito -= OnDietaCreadaConExito;
    }
}
