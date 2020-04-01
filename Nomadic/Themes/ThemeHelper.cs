using Nomadic.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Nomadic.Themes
{
    public static class ThemeHelper
    {
        public static void GetSystemRequestedTheme()
        {
            AppTheme appTheme = AppInfo.RequestedTheme;

            if (appTheme == AppTheme.Dark)
            {
                Application.Current.Resources = new DarkTheme();
                DependencyService.Get<IStatusBar>().ChangeStatusBarColorToBlack();
            }
            else if (appTheme == AppTheme.Light)
            {
                Application.Current.Resources = new LightTheme();
                DependencyService.Get<IStatusBar>().ChangeStatusBarColorToWhite();
            }
            else
            {
                App.Current.Resources = new LightTheme();
                DependencyService.Get<IStatusBar>().ChangeStatusBarColorToWhite();
            }    
        }    
    }    
} 
