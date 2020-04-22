using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Nomadic.Helpers.Authentication;

namespace Nomadic.Droid.Utils
{
    [Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(
         new[] { Intent.ActionView },
         Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
         DataSchemes = new[] { "com.googleusercontent.apps.654631461988-s8ugttecvkan63tvk2u3hpkc0u71ljnr" }, // "example com.googleusercontent.apps.6546314619-s8ugecvkan63tvk2u3hpkc0u71lj
         DataPath = "/oauth2redirect")]
    public class CustomUrlSchemeInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            global::Android.Net.Uri uri_android = Intent.Data;

            Uri uri_netfx = new Uri(uri_android.ToString());

            // load redirect_url Page
            AuthenticationState.Authenticator.OnPageLoading(uri_netfx);

            var intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);

            this.Finish();

            return;
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.Content.PM;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Nomadic.Helpers.Authentication;

//namespace Nomadic.Droid.Utils
//{
//    [Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
//    [IntentFilter(
//         new[] { Intent.ActionView },
//         Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
//         DataSchemes = new[] { "place your Android Google reversed client id here" }, // "example com.googleusercontent.apps.6546314619-s8ugecvkan63tvk2u3hpkc0u71lj
//         DataPath = "/oauth2redirect")]
//    public class CustomUrlSchemeInterceptorActivity : Activity
//    {
//        protected override void OnCreate(Bundle savedInstanceState)
//        {
//            base.OnCreate(savedInstanceState);

//            global::Android.Net.Uri uri_android = Intent.Data;

//            Uri uri_netfx = new Uri(uri_android.ToString());

//            // load redirect_url Page
//            AuthenticationState.Authenticator.OnPageLoading(uri_netfx);

//            var intent = new Intent(this, typeof(MainActivity));
//            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
//            StartActivity(intent);

//            this.Finish();

//            return;
//        }
//    }
//}