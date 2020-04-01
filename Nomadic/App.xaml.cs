using Nomadic.Themes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nomadic
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            ThemeHelper.GetSystemRequestedTheme();
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}
