using App_Calorias.Views;

namespace App_Calorias
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        private async void OnCalcularCaloriasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UsuariosPage());
        }

      

    }

}
