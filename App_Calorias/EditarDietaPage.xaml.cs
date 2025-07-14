using App_Calorias.Models;
using App_Calorias.ViewModels;

namespace App_Calorias;

public partial class EditarDietaPage : ContentPage
{
	public EditarDietaPage(Dieta dieta)
	{
        InitializeComponent();
        BindingContext = new EditarDietaViewModel(dieta);
    }
}
