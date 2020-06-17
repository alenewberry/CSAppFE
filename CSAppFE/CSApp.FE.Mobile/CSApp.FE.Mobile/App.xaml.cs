﻿namespace CSApp.FE.Mobile
{
    using ViewModels;
    using Views;
    using Xamarin.Forms;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainViewModel.GetInstance().Login = new LoginViewModel();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
