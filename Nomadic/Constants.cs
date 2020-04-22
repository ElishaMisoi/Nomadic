using System;
using System.Collections.Generic;
using System.Text;

namespace Nomadic
{
    public static class Constants
    {
        public const string NewsAPIKey = "a71fb05d411c47daaf574708efdb66b5";

        public static string AppName = "Nomadic"; // Name of your Google App

        // OAuth
        // For Google login, configure at https://console.developers.google.com/
        public static string GoogleiOSClientId = "654631461988-e6g8606omb24clld9i4okci42akjh51p.apps.googleusercontent.com"; // example 6546314619-e6g8606ombclld9i4okci42akjhp.apps.googleusercontent.com
        public static string GoogleAndroidClientId = "654631461988-s8ugttecvkan63tvk2u3hpkc0u71ljnr.apps.googleusercontent.com"; // example 6546314619-s8ugecvkan63tvk2u3hpkc0u71lj.apps.googleusercontent.com

        // These values do not need changing
        public static string GoogleScope = "https://www.googleapis.com/auth/userinfo.email";
        public static string GoogleAuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string GoogleAccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string GoogleUserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string GoogleiOSRedirectUri = "com.googleusercontent.apps.654631461988-e6g8606omb24clld9i4okci42akjh51p:/oauth2redirect"; // example com.googleusercontent.apps.6546314619-e6g8606ombclld9i4okci42akjhp:/oauth2redirect
        public static string GoogleAndroidRedirectUri = "com.googleusercontent.apps.654631461988-s8ugttecvkan63tvk2u3hpkc0u71ljnr:/oauth2redirect"; // example com.googleusercontent.apps.6546314619-s8ugecvkan63tvk2u3hpkc0u71lj:/oauth2redirect
    }
}

//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Nomadic
//{
//    public static class Constants
//    {
//        public const string NewsAPIKey = "place your NewsAPI key here";

//        public static string AppName = "Nomadic"; // Name of your Google App

//        // OAuth
//        // For Google login, configure at https://console.developers.google.com/
//        public static string GoogleiOSClientId = "place your iOS Google client Id here"; // example 6546314619-e6g8606ombclld9i4okci42akjhp.apps.googleusercontent.com
//        public static string GoogleAndroidClientId = "place your Android Google client Id here"; // example 6546314619-s8ugecvkan63tvk2u3hpkc0u71lj.apps.googleusercontent.com

//        // These values do not need changing
//        public static string GoogleScope = "https://www.googleapis.com/auth/userinfo.email";
//        public static string GoogleAuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
//        public static string GoogleAccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
//        public static string GoogleUserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

//        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
//        public static string GoogleiOSRedirectUri = "place your iOS Google redirect uri here"; // example com.googleusercontent.apps.6546314619-e6g8606ombclld9i4okci42akjhp:/oauth2redirect
//        public static string GoogleAndroidRedirectUri = "place your Android Google redirect uri here"; // example com.googleusercontent.apps.6546314619-s8ugecvkan63tvk2u3hpkc0u71lj:/oauth2redirect
//    }
//}