using Nomadic.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SearchBar), typeof(SearchBarRendererForiOS))]
namespace Nomadic.iOS.Renderers
{
    public class SearchBarRendererForiOS : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.SearchBar> e)
        {
            base.OnElementChanged(e);

            var searchbar = (UISearchBar)Control;

            if (e.NewElement != null)
            {
                searchbar.TintColor = UIColor.LightGray;
                searchbar.BarTintColor = UIColor.Clear;
                searchbar.BackgroundColor = UIColor.Clear;
                searchbar.Layer.CornerRadius = 0;
                searchbar.SearchBarStyle = UISearchBarStyle.Minimal;

                searchbar.SetShowsCancelButton(false, false);
                searchbar.ShowsCancelButton = false;

                searchbar.TextChanged += delegate
                {
                    searchbar.ShowsCancelButton = false;
                };
            }
        }
    }
}