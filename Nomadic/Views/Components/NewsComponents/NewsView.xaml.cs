using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nomadic.Views.Components.NewsComponents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsView : ViewCell
    {
        public NewsView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            image.Source = null;

            var bindingContext = BindingContext as Models.Article;

            image.Source = bindingContext.UrlToImage;
            description.Text = bindingContext.Description;
            source.Text = $"{bindingContext.Source} . ";
            published.Text = bindingContext.Published;
        }

        /// <summary>
        /// THIS IS BAD AND VERY RISKY CODE, BUT THE PERFECT CODE DOES NOT EXIST :)
        /// </summary>
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var obj = (sender as Label).BindingContext as Models.Article;

            var parentBindingContext = viewCell.Parent.Parent.BindingContext;

            if (parentBindingContext != null && parentBindingContext == ViewModels.SavedArticlesViewModels.Instance)
            {
                ViewModels.SavedArticlesViewModels.Instance.CurrentArticle = obj;
                await PopupNavigation.Instance.PushAsync(new PopupComponents.SavedArticlesSavePopup());
            }
            else
            {
                ViewModels.MainFeedViewModel.Instance.CurrentArticle = obj;
                await PopupNavigation.Instance.PushAsync(new PopupComponents.MainFeedSavePopup());
            }
        }
    }
}