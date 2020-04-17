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
    public partial class Explore : ContentPage
    {
        public Explore()
        {
            InitializeComponent();
            BindingContext = ViewModels.InterestsViewModel.Instance;
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (collectionView.SelectedItem != null)
            {
                var selected = e.CurrentSelection.FirstOrDefault() as Models.Interest;

                ViewModels.InterestsViewModel.Instance.CurrentItem = new Models.Tab { Title = selected.Title };

                await Shell.Current.GoToAsync($"interestpage");

                collectionView.SelectedItem = null;
            }
        }
    }
}