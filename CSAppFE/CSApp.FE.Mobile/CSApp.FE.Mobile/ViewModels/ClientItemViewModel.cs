namespace CSApp.FE.Mobile.ViewModels
{
    using CSApp.FE.Mobile.Views;
    using CSAppFE.Common.Models;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;

    public class ClientItemViewModel : Client
    {
        public ICommand SelectClientCommand => new RelayCommand(this.SelectClient);

        private async void SelectClient()
        {
            MainViewModel.GetInstance().EditClient = new EditClientViewModel(this);
            await App.Navigator.PushAsync(new EditClientPage());
        }
    }
}
