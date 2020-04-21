using Newtonsoft.Json;
using Nomadic.Helpers;
using Nomadic.Helpers.Authentication;
using Nomadic.Themes;
using PSC.Xamarin.MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Auth;
using Xamarin.Forms;

namespace Nomadic.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set
            {
                _isLoggedIn = value;
                OnPropertyChanged();
            }
        }

        string GoogleClientId()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return Constants.GoogleiOSClientId;
                case Device.Android:
                    return Constants.GoogleAndroidClientId;
                default:
                    return null;
            }
        }

        string GoogleRedirectUrl()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return Constants.GoogleiOSRedirectUrl;
                case Device.Android:
                    return Constants.GoogleAndroidRedirectUrl;
                default:
                    return null;
            }
        }

        Models.User _currentUser;
        public Models.User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        // Authentication Stuff

        /// <summary>
        /// Initiates Google Login by presenting the user
        /// with Google Login presenter (Native Web Authenticator)
        /// </summary>
        [Obsolete]
        void LoginWithGoogle()
        {
            try
            {
                var store = AccountStore.Create();

                var account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

                var authenticator = new OAuth2Authenticator(
                    GoogleClientId(),
                    string.Empty,
                    Constants.GoogleScope,
                    new Uri(Constants.GoogleAuthorizeUrl),
                    new Uri(GoogleRedirectUrl()),
                    new Uri(Constants.GoogleAccessTokenUrl),
                    null,
                    true)
                {
                    AllowCancel = true,
                    Title = "Google Login"
                };

                authenticator.Completed += OnGoogleAuthCompleted;
                authenticator.Error += OnGoogleAuthError;

                AuthenticationState.Authenticator = authenticator;

                var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                presenter.Login(authenticator);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                DialogsHelper.HandleDialogMessage(DialogsHelper.Errors.UndefinedError);
            }
        }

        /// <summary>
        /// Google Auth Completed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnGoogleAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnGoogleAuthCompleted;
                authenticator.Error -= OnGoogleAuthError;
            }

            if (e.IsAuthenticated)
            {
                DialogsHelper.ProgressDialog.Show();

                try
                {
                    var google_provider_authCredential = Plugin.FirebaseAuth.CrossFirebaseAuth.Current
                        .GoogleAuthProvider.GetCredential(e.Account.Properties["id_token"], e.Account.Properties["access_token"]);

                    var result = await Plugin.FirebaseAuth.CrossFirebaseAuth.Current.Instance
                        .SignInWithCredentialAsync(google_provider_authCredential);

                    await SaveUser(result);
                }
                catch (Plugin.FirebaseAuth.FirebaseAuthException ex)
                {
                    Debug.WriteLine(ex.Message);

                    DialogsHelper.HandleDialogMessage(DialogsHelper.Errors.UndefinedError);
                }

                DialogsHelper.ProgressDialog.Hide();
            }
        }

        /// <summary>
        /// Google Auth Error event handler
        /// Do something when an error occurs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnGoogleAuthError(object sender, AuthenticatorErrorEventArgs e)
        {

            Debug.WriteLine("Authentication error: " + e.Message);

            DialogsHelper.ProgressDialog.Hide();

            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnGoogleAuthCompleted;
                authenticator.Error -= OnGoogleAuthError;
            }

            DialogsHelper.HandleDialogMessage(DialogsHelper.Errors.UndefinedError);
        }

        /// <summary>
        /// Function to save user to the database 
        /// if the user does not exist
        /// </summary>
        /// <param name="result"></param>
        async Task SaveUser(Plugin.FirebaseAuth.IAuthResult result)
        {
            var userObject = new Models.User
            {
                Name = result.User.DisplayName,
                Email = result.User.Email,
                UserID = result.User.Uid,
                ImageUrl = result.User.PhotoUrl.ToString(),
            };

            var existingUser = await DatabaseHelper.GetUser(result.User.Uid);

            if(existingUser == null)
            {
                await DatabaseHelper.AddUser(userObject);
            }

            CurrentUser = userObject;

            IsLoggedIn = true;

            DialogsHelper.ProgressDialog.Hide();
        }

        [Obsolete]
        async Task ShowPrompt()
        {
            string displayMessage = "\"Nomadic\" Wants to Use " + "\"" + "google.com" + "\"" + " to Sign In";
            bool action = await Application.Current.MainPage.DisplayAlert(displayMessage, "This allows the app and website to share information about you", "Continue", "Cancel");

            if (action)
            {
                LoginWithGoogle();
            }
        }

        async Task SignOut()
        {
            bool action = await Application.Current.MainPage.DisplayAlert("Sign out?", "Are you sure you want to sign out?", "Yes", "No");

            if (action)
            {
                DialogsHelper.ProgressDialog.Show();

                CurrentUser = null;
                Plugin.FirebaseAuth.CrossFirebaseAuth.Current.Instance.SignOut();

                IsLoggedIn = false;

                await Shell.Current.Navigation.PopAsync();

                DialogsHelper.ProgressDialog.Hide();
            }
        }

        ICommand _loginWithGoogleCommand;

        [Obsolete]
        public ICommand LoginWithGoogleCommand
        {
            get
            {
                return _loginWithGoogleCommand ?? (_loginWithGoogleCommand =
                                          new Command(async (object obj) => await ShowPrompt()));
            }
        }

        ICommand _signOutCommand;

        [Obsolete]
        public ICommand SignOutCommand
        {
            get
            {
                return _signOutCommand ?? (_signOutCommand =
                                          new Command(async (object obj) => await SignOut()));
            }
        }

        public static SettingsViewModel Instance { get; } = new SettingsViewModel();
    }
}
