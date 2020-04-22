using Nomadic.Themes;
using Plugin.CloudFirestore;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nomadic
{
    public partial class App : Application
    {
        public App()
        {
            CrossCloudFirestore.Current.Instance.FirestoreSettings = new FirestoreSettings
            {
                AreTimestampsInSnapshotsEnabled = false,
                IsPersistenceEnabled = true
            };

            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            ThemeHelper.GetAppTheme();
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}
