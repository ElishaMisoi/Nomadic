using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nomadic.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("ArticleJsonString", "jsonString")]
    public partial class WebPage : ContentPage
    {
        Models.Article article;

        public WebPage(Models.Article _article)
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
            article = _article;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            webView.Source = article.Url;
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
    }
}