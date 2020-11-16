namespace CSApp.FE.Mobile.ViewModels
{
    using CSApp.FE.Mobile.Views;
    using CSAppFE.Common.Models;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    public class MainViewModel
    {
        private static MainViewModel instance;

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public TokenResponse Token { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        public LoginViewModel Login { get; set; }

        public ClientsViewModel Clients { get; set; }

        public AddClientViewModel AddClient { get; set; }

        public EditClientViewModel EditClient { get; set; }

        public ICommand AddClientCommand { get { return new RelayCommand(this.GoAddClient); } }

        public MainViewModel()
        {
            instance = this;
            this.LoadMenu();
        }

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }

        private void LoadMenu()
        {
            var menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_info",
                    PageName = "AboutPage",
                    Title = "Acerca de"
                },
                new Menu
                {
                    Icon = "ic_setup",
                    PageName = "SetupPage",
                    Title = "Setup"
                },

                new Menu
                {
                    Icon = "ic_ecatedral",
                    PageName = "ECatedralPage",
                    Title = "e-Catedral"
                },
                new Menu
                {
                    Icon = "ic_afip",
                    PageName = "EVentanillaPage",
                    Title = "e-Ventanilla"
                },
                new Menu
                {
                    Icon = "ic_exit",
                    PageName = "LoginPage",
                    Title = "Cerrar sesión"
                }
            };

            this.Menus = new ObservableCollection<MenuItemViewModel>(menus.Select(m => new MenuItemViewModel
            {
                Icon = m.Icon,
                PageName = m.PageName,
                Title = m.Title
            }).ToList());
        }

        private async void GoAddClient()
        {
            this.AddClient = new AddClientViewModel();
            await App.Navigator.PushAsync(new AddClientPage());
        }
    }
}
