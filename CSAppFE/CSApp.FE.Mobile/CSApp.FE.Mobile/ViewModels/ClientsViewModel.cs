namespace CSApp.FE.Mobile.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using CSAppFE.Common.Models;
    using CSAppFE.Common.Services;
    using Xamarin.Forms;

    public class ClientsViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<Client> clients;

        public ObservableCollection<Client> Clients
        {
            get { return this.clients; }
            set { this.SetValue(ref this.clients, value); }
        }

        private bool isRefreshing;

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public ClientsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadClients();
        }

        private async void LoadClients()
        {
            this.IsRefreshing = true;
            var response = await this.apiService.GetListAsync<Client>(
                "http://190.105.226.109:8085",
                "/api",
                "/Clients");

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                return;
            }

            var myClients = (List<Client>)response.Result;
            this.Clients = new ObservableCollection<Client>(myClients);
        }
    }
}
