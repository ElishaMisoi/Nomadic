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

        private async void SavedArticles_Tapped(object sender, EventArgs e)
        {
            var userId = await Helpers.DatabaseHelper.UserId();

            if (userId != null)
            {
                await Shell.Current.GoToAsync($"savedarticles");
            }
        }

        private async void NotImplemented_Tapped(object sender, EventArgs e)
        {
            var actionSheet = await DisplayActionSheet("Not Implemented", "No", "Yes", "This feature has not been implemented. No plans are in the works. You can contribute to the project if you like 🙂. Would you like to open the project in GitHub?");
        
            if(actionSheet == "Yes")
            {
                await Helpers.DialogsHelper.OpenBrowser("https://github.com/Elisha-Misoi/Nomadic");
            }
        }

        private async void About_Tapped(object sender, EventArgs e)
        {
            await Helpers.DialogsHelper.OpenBrowser("https://github.com/Elisha-Misoi/Nomadic");
        }

        private async void SendFeedBack_Tapped(object sender, EventArgs e)
        {
            Helpers.DialogsHelper.ProgressDialog.Show();

            await Task.Delay(1000);

            await Helpers.DialogsHelper.SendEmail("FEEDBACK FOR NOMADIC", "", new List<string> { "e.kmisoi@outlook.com" });

            Helpers.DialogsHelper.ProgressDialog.Hide();
        }
    }
}