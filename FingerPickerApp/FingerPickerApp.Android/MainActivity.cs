
using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using FingerPickerApp;

namespace FingerPickerApp.Droid
{
    [Activity(Label = "FingerPickerApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            Finger finger = new Finger(10,10,10,"Black");

            Console.WriteLine("finger picker X = " + finger.getFingerX());
        }
    }
}