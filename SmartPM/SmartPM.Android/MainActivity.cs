using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.OneSignal;


namespace SmartPM.Droid
{
    [Activity(Label = "SmartPM", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                base.OnCreate(bundle);

                global::Xamarin.Forms.Forms.Init(this, bundle);

                OneSignal.Current.StartInit("8e9e2a3a-dfdb-49f0-925c-c756cf54011a")
                      .EndInit();
                LoadApplication(new App());
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
    }
}

