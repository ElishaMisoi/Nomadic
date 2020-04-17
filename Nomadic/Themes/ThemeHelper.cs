using Nomadic.Interfaces;

namespace Nomadic.Themes
{
    public static class ThemeHelper
    {
        public static void GetSystemRequestedTheme()
        {
            Xamarin.Essentials.AppTheme appTheme = Xamarin.Essentials.AppInfo.RequestedTheme;

            if (appTheme == Xamarin.Essentials.AppTheme.Dark)
            {
                Xamarin.Forms.Application.Current.Resources = new DarkTheme();
                Xamarin.Forms.DependencyService.Get<IStatusBar>().ChangeStatusBarColorToBlack();
            }
            else if (appTheme == Xamarin.Essentials.AppTheme.Light)
            {
                Xamarin.Forms.Application.Current.Resources = new LightTheme();
                Xamarin.Forms.DependencyService.Get<IStatusBar>().ChangeStatusBarColorToWhite();
            }
            else
            {
                Xamarin.Forms.Application.Current.Resources = new LightTheme();
                Xamarin.Forms.DependencyService.Get<IStatusBar>().ChangeStatusBarColorToWhite();
            }
        }
    }
}