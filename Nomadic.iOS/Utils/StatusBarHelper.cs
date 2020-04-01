using Nomadic.Interfaces;
using Nomadic.iOS.Utils;
using Xamarin.Forms;

[assembly: Dependency(typeof(StatusBarHelper))]
namespace Nomadic.iOS.Utils
{
    public class StatusBarHelper : IStatusBar
    {
        public void ChangeStatusBarColorToWhite()
        {
            UIKit.UIApplication.SharedApplication.StatusBarStyle = UIKit.UIStatusBarStyle.LightContent;
        }

        public void ChangeStatusBarColorToBlack()
        {
            UIKit.UIApplication.SharedApplication.StatusBarStyle = UIKit.UIStatusBarStyle.Default;
        }

        public void HideStatusBar()
        {
            UIKit.UIApplication.SharedApplication.StatusBarStyle = UIKit.UIStatusBarStyle.BlackTranslucent;
        }
    }
}