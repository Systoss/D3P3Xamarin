using Android.App;
using Android.Content.PM;
using Android.OS;
using Prism;
using Prism.Ioc;
using Amazon.CognitoIdentityProvider;
using Amazon;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace BlankApp1.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public AmazonCognitoIdentityProviderConfig ClientHttpConfig { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            AppCenter.Start("d2de2538-ef83-4c40-861e-15aaf2b41abc", typeof(Analytics), typeof(Crashes));

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ClientHttpConfig = new AmazonCognitoIdentityProviderConfig
            {
                HttpClientFactory = new AndroidClientFactory()

            };
            ClientHttpConfig.RegionEndpoint = RegionEndpoint.USEast1;

            LoadApplication(new App(new AndroidInitializer(), ClientHttpConfig));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

