using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nomadic.Views.Components.NewsComponents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WideNewsView : ViewCell
    {
        public WideNewsView()
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
    }
}