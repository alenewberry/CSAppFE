namespace CSApp.FE.Mobile.ViewModels
{
    using System.Windows.Input;
    using CSAppFE.Common.Models;
    using CSAppFE.Common.Services;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class AddClientViewModel : BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;

        public string Cuit { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

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

        public AddClientViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Cuit))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar un Cuit", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Name))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar un Nombre.", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar un Email.", "Aceptar");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var client = new Client {
                Cuit = this.Cuit,
                Name = this.Name,
                Phone = this.Phone,
                Email = this.Email,
                User = new User { Email = MainViewModel.GetInstance().UserEmail }
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PostAsync(
                url,
                "/api",
                "/Clients",
                client,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var newClient = (Client)response.Result;
            MainViewModel.GetInstance().Clients.AddClientToList(newClient);

            isRunning = false;
            isEnabled = true;
            await App.Navigator.PopAsync();
        }
    }
}
