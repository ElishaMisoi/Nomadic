using Newtonsoft.Json;
using Nomadic.AppSettings;
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
        #region Properties

        /// <summary>
        /// True if LightTheme has been selected
        /// </summary>
        bool _isLightTheme;
        public bool IsLightTheme
        {
            get { return _isLightTheme; }
            set
            {
                _isLightTheme = value;
                OnPropertyChanged();
                if (IsLightTheme)
                {
                    IsDarkTheme = false;
                    IsSystemPreferredTheme = false;
                }
            }
        }

        /// <summary>
        /// True if DarkTheme has been selected
        /// </summary>
        bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get { return _isDarkTheme; }
            set
            {
                _isDarkTheme = value;
                OnPropertyChanged();
                if (IsDarkTheme)
                {
                    IsLightTheme = false;
                    IsSystemPreferredTheme = false;
                }
            }
        }

        /// <summary>
        /// True if SystemPreferredTheme has been selected
        /// </summary>
        bool _isSystemPreferredTheme;
        public bool IsSystemPreferredTheme
        {
            get { return _isSystemPreferredTheme; }
            set
            {
                _isSystemPreferredTheme = value;
                OnPropertyChanged();
                if (IsSystemPreferredTheme)
                {
                    IsLightTheme = false;
                    IsDarkTheme = false;
                }
            }
        }

        /// <summary>
        /// True if user is logged in
        /// </summary>
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

        /// <summary>
        /// Represents the object of the current user
        /// </summary>
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

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SettingsViewModel()
        {
            CurrentUser = new Models.User();
            _ = GetSettings();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Function to get GoogleClientId depending on platform
        /// </summary>
        /// <returns>Returns GoogleClientID</returns>
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

        /// <summary>
        /// Function to get GoogleRedirectUri depending on platform
        /// </summary>
        /// <returns>Returns GoogleRedirectUri</returns>
        string GoogleRedirectUrl()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return Constants.GoogleiOSRedirectUri;
                case Device.Android:
                    return Constants.GoogleAndroidRedirectUri;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Function to fetch current user preferences
        /// </summary>
        async Task GetSettings()
        {
            await GetUser().ConfigureAwait(false);
            GetThemeSetting();
        }

        /// <summary>
        /// Function to get currently logged in user
        /// IsLoggedIn is set to false if user is
        /// not logged in
        /// </summary>
        async Task GetUser()
        {
            string isloggedIn = Settings.GetSetting(Settings.AppPrefrences.IsLoggedIn);

            if (isloggedIn != null || isloggedIn != "False")
            {
                string userJson = await Settings.GetSecureSetting(Settings.AppPrefrences.User);

                if (userJson != null)
                {
                    CurrentUser = JsonConvert.DeserializeObject<Models.User>(userJson);
                    IsLoggedIn = true;
                }
            }
            else
            {
                IsLoggedIn = false;
            }
        }

        /// <summary>
        /// Function to get the current user's Theme preference
        /// </summary>
        void GetThemeSetting()
        {
            string theme = Settings.GetSetting(Settings.AppPrefrences.AppTheme);

            var appTheme = EnumsHelper.ConvertToEnum<Settings.Theme>(theme);

            switch (appTheme)
            {
                case Settings.Theme.LightTheme:
                    IsLightTheme = true;
                    break;
                case Settings.Theme.DarkTheme:
                    IsDarkTheme = true;
                    break;
                case Settings.Theme.SystemPreferred:
                    IsSystemPreferredTheme = true;
                    break;
                default:
                    IsSystemPreferredTheme = true;
                    break;
            }
        }

        /// <summary>
        /// Function to change user's Theme in realtime 
        /// when user chooses a  different Theme preference
        /// </summary>
        void ChangeTheme(string theme)
        {
            var appTheme = EnumsHelper.ConvertToEnum<Settings.Theme>(theme);

            switch (appTheme)
            {
                case Settings.Theme.LightTheme:
                    IsLightTheme = true;
                    ThemeHelper.ChangeToLightTheme();
                    break;
                case Settings.Theme.DarkTheme:
                    IsDarkTheme = true;
                    ThemeHelper.ChangeToDarkTheme();
                    break;
                case Settings.Theme.SystemPreferred:
                    IsSystemPreferredTheme = true;
                    ThemeHelper.ChangeToSystemPreferredTheme();
                    break;
                default:
                    IsSystemPreferredTheme = true;
                    ThemeHelper.ChangeToSystemPreferredTheme();
                    break;
            }
        }


        // Authentication Stuff

        /// <summary>
        /// Function to Initiate Google Login
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
        /// Function to handle successful Google Login
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
                    var google_provider_authCredential = Plugin.FirebaseAuth.CrossFirebaseAuth.Current.GoogleAuthProvider.GetCredential(e.Account.Properties["id_token"], e.Account.Properties["access_token"]);
                    var result = await Plugin.FirebaseAuth.CrossFirebaseAuth.Current.Instance.SignInWithCredentialAsync(google_provider_authCredential);

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
        /// Event handler to capture Google Login error
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
        /// Function to save user object and user interest 
        /// locally/in the database
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

            string userJson = JsonConvert.SerializeObject(userObject);

            await Settings.SetSecureSetting(Settings.AppPrefrences.User, userJson);

            var existingUser = await DatabaseHelper.GetUser(result.User.Uid);

            var existingInterests = DatabaseHelper.GetSavedInterestsList();

            if (existingUser != null)
            {
                if (existingInterests != null && existingInterests.Any())
                {
                    if (existingUser.Interests.Any())
                    {
                        foreach (var item in existingUser.Interests)
                        {
                            if (!existingInterests.Any(s => s.Title.ToLower().Equals(item.Title.ToLower())))
                            {
                                existingInterests.Add(item);
                            }
                        }
                    }

                    await DatabaseHelper.UpdateUserInterests(existingInterests).ConfigureAwait(false);
                }
                else
                {
                    if (existingUser.Interests.Any())
                    {
                        string userInterests = JsonConvert.SerializeObject(existingUser.Interests);
                        Settings.AddSetting(Settings.AppPrefrences.Interests, userInterests);
                    }
                }

                var savedInterests = DatabaseHelper.GetSavedInterestsList();

                if (savedInterests != null && savedInterests.Any())
                {
                    await MainFeedViewModel.Instance.GetUserData().ConfigureAwait(false);
                    await InterestsViewModel.Instance.LoadInterestsList().ConfigureAwait(false);
                }
            }
            else
            {
                if (existingInterests != null && existingInterests.Any())
                {
                    userObject.Interests = existingInterests;
                }

                await DatabaseHelper.AddUser(userObject);
            }

            Settings.AddSetting(Settings.AppPrefrences.IsLoggedIn, "True");

            await GetUser();
        }

        /// <summary>
        /// Function to prompt user on Google Login
        /// </summary>
        [Obsolete]
        async Task ShowPrompt()
        {
            string displayMessage = "\"Nomadic\" Wants to use " + "\"" + "google.com" + "\"" + " to Sign In";
            bool action = await Application.Current.MainPage.DisplayAlert(displayMessage, "This allows the app and website to share information about you", "Continue", "Cancel");

            if (action)
            {
                LoginWithGoogle();
            }
        }

        /// <summary>
        /// Function to Sign Out user
        /// </summary>
        async Task SignOut()
        {
            bool action = await Application.Current.MainPage.DisplayAlert("Sign out?", "Are you sure you want to sign out?", "Yes", "No");

            if (action)
            {
                DialogsHelper.ProgressDialog.Show();

                CurrentUser = null;
                Plugin.FirebaseAuth.CrossFirebaseAuth.Current.Instance.SignOut();
                Settings.ClearSecureSorage();
                Settings.RemoveSetting(Settings.AppPrefrences.IsLoggedIn);

                IsLoggedIn = false;

                await Shell.Current.Navigation.PopAsync();

                DialogsHelper.ProgressDialog.Hide();
            }
        }

        /// <summary>
        /// Command to login with Google
        /// </summary>
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

        /// <summary>
        /// Command to Sign Out user
        /// </summary>
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

        /// <summary>
        /// Command to change theme
        /// </summary>
        ICommand _themeChangeCommand = null;

        public ICommand ThemeChangeCommand
        {
            get
            {
                return _themeChangeCommand ?? (_themeChangeCommand =
                                          new Xamarin.Forms.Command((object obj) => ChangeTheme((string)obj)));
            }
        }

        #endregion

        /// <summary>
        /// Gets an Instance of this class
        /// </summary>
        public static SettingsViewModel Instance { get; } = new SettingsViewModel();
    }
}
