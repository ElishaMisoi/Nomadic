using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nomadic.Views.Components.InterestsComponents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Interest : Grid
    {
        public Interest()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var obj = (sender as Grid).BindingContext as Models.Interest;

            if (obj.IsInterestAdded)
            {
                obj.IsInterestAdded = false;
                ViewModels.InterestsViewModel.Instance.RemoveUserInterest(obj);
            }
            else
            {
                obj.IsInterestAdded = true;
                await ViewModels.InterestsViewModel.Instance.AddUserInterest(obj).ConfigureAwait(false);
            }
        }
    }
}