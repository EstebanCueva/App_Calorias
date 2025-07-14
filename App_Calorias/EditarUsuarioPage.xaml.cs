using App_Calorias.ViewModels;
using App_Calorias.Models;

namespace App_Calorias.Views;

public partial class EditarUsuarioPage : ContentPage
{
    public EditarUsuarioPage(UsuarioSimple usuario)
    {
        InitializeComponent();
        BindingContext = new EditarUsuarioViewModel(usuario);
    }
}
