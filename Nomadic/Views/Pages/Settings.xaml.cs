using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nomadic.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
            BindingContext = ViewModels.SettingsViewModel.Instance;
        }

        private async void SignOut_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"signout");
        }
    }
}