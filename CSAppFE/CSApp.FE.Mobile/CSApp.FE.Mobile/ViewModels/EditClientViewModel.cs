namespace CSApp.FE.Mobile.ViewModels
{
    using CSAppFE.Common.Models;
    using CSAppFE.Common.Services;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class EditClientViewModel : BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;

        public Client Client { get; set; }

        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }

        public ICommand SaveCommand => new RelayCommand(this.Save);

        public ICommand DeleteCommand => new RelayCommand(this.Delete);

        public EditClientViewModel(Client client)
        {
            this.Client = client;
            this.apiService = new ApiService();
            this.IsEnabled = true;
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Client.Cuit))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar un Cuit", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Client.Name))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar un Nombre.", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Client.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar un Email.", "Aceptar");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PutAsync(
                url,
                "/api",
                "/Clients",
                this.Client.Id,
                this.Client,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            var modifiedClient = (Client)response.Result;
            MainViewModel.GetInstance().Clients.UpdateClientInList(modifiedClient);
            await App.Navigator.PopAsync();
        }

        private async void Delete()
        {
            var confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", "Seguro que desea eliminar el cliente?", "Sí", "No");
            if (!confirm)
            {
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.DeleteAsync(
                url,
                "/api",
                "/Clients",
                this.Client.Id,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            MainViewModel.GetInstance().Clients.DeleteClientInList(this.Client.Id);
            await App.Navigator.PopAsync();
       }
    }
}
