namespace CSApp.FE.Mobile.ViewModels
{
    using CSApp.FE.Mobile.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MenuItemViewModel : CSAppFE.Common.Models.Menu
    {
        public ICommand SelectMenuCommand => new RelayCommand(this.SelectMenu);

        private async void SelectMenu()
        {

            App.Master.IsPresented = false;

            var mainViewModel = MainViewModel.GetInstance();

            switch (this.PageName)
            {
                case "AboutPage":
                    await App.Navigator.PushAsync(new AboutPage());
                    break;
                case "SetupPage":
                    await App.Navigator.PushAsync(new SetupPage());
                    break;
                case "EVentanillaPage":
                    await App.Navigator.PushAsync(new EVentanillaPage());
                    break;
                case "ECatedralPage":
                    await App.Navigator.PushAsync(new ECatedralPage());
                    break;
                default:
                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
            }
        }
    }
}
