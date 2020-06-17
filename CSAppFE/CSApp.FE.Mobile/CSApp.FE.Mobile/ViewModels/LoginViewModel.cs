namespace CSApp.FE.Mobile.ViewModels
{
    using CSApp.FE.Mobile.Views;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(Login);

        public LoginViewModel()
        {
            this.Email = "alenewberry@gmail.com";
            this.Password = "123456";
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Por favor, ingresa un email válido", "Aceptar");
                return;
            }

            if(string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Por favor, ingresa una contraseña", "Aceptar");
                return;
            }

            if (!this.Email.Equals("alenewberry@gmail.com") || !this.Password.Equals("123456"))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Usuario o contraseña incorrecto/a", "Aceptar");
                return;
            }

            //await Application.Current.MainPage.DisplayAlert(
            //        "Ok", "Adentro perrito malvado", "Aceptar");
            //return;

            MainViewModel.GetInstance().Clients = new ClientsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ClientsPage());
        }
    }
}
