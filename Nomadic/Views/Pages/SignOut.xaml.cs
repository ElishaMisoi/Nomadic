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
    public partial class SignOut : ContentPage
    {
        public SignOut()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
            BindingContext = ViewModels.SettingsViewModel.Instance;
        }
    }
}