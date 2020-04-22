using Nomadic.Helpers;
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
    public partial class WebPage : ContentPage
    {
        Models.Article _article;

        public WebPage(Models.Article article)
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
            _article = article;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            webView.Source = _article.Url;
        }

        private async void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            await progressBar.ProgressTo(1, 100, Easing.Linear);
            progressBar.IsVisible = false;
        }

        private async void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            await progressBar.ProgressTo(0.95, 7000, Easing.Linear);
        }

        private async void ShareArticle(object sender, EventArgs e)
        {
            await DialogsHelper.ShareText($"Check out this article: \n\n {_article.Url}");
        }

        private async void SaveArticle(object sender, EventArgs e)
        {
            await DatabaseHelper.SaveArticle(_article);
        }
    }
}