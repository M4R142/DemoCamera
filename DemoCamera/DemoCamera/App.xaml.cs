using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoCamera
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            // Indicar la pagina de inicio
            MainPage = new NavigationPage(new Views.MenuView());
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
