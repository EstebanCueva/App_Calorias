namespace App_Calorias;
using App_Calorias.ViewModels;
using App_Calorias.Models;
public partial class CrearUsuarioPageNEW : ContentPage
{
    private readonly UsuarioViewModel _viewModel;

    public CrearUsuarioPageNEW()
    {
        InitializeComponent();
        _viewModel = new UsuarioViewModel();
        BindingContext = _viewModel;

        _viewModel.UsuarioCreadoConExito += OnUsuarioCreadoConExito;
    }

    private async void OnUsuarioCreadoConExito()
    {
        await Shell.Current.GoToAsync("//UsuarioPage");
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.UsuarioCreadoConExito -= OnUsuarioCreadoConExito;
    }
}
