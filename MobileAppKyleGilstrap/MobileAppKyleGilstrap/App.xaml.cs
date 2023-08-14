using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileAppKyleGilstrap.DataHolders;

namespace MobileAppKyleGilstrap
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            Database.createDataBase();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
