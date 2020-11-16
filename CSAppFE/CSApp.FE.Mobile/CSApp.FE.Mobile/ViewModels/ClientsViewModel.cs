namespace CSApp.FE.Mobile.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using CSAppFE.Common.Models;
    using CSAppFE.Common.Services;
    using Xamarin.Forms;

    public class ClientsViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<ClientItemViewModel> clients;

        private List<Client> myClients;

        public ObservableCollection<ClientItemViewModel> Clients
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
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<Client>(
                url,
                "/api",
                "/Clients",
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                return;
            }

            this.myClients = (List<Client>)response.Result;
            this.RefreshClientsList();
        }

        private void RefreshClientsList()
        {
            this.Clients = new ObservableCollection<ClientItemViewModel>(myClients.Select(c => new ClientItemViewModel
            {
                Id = c.Id,
                Cuit = c.Cuit,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                User = c.User }).OrderBy(c => c.Name).ToList());
        }

        public void AddClientToList(Client client)
        {
            this.myClients.Add(client);
            this.RefreshClientsList();
        }

        public void UpdateClientInList(Client client)
        {
            var previousClient = this.myClients.Where(c => c.Id == client.Id).FirstOrDefault();
            if (previousClient != null)
            {
                this.myClients.Remove(previousClient);
            }

            this.myClients.Add(client);
            this.RefreshClientsList();
        }

        public void DeleteClientInList(int clientId)
        {
            var previousClient = this.myClients.Where(c => c.Id == clientId).FirstOrDefault();
            if (previousClient != null)
            {
                this.myClients.Remove(previousClient);
            }

            this.RefreshClientsList();
        }
    }
}
