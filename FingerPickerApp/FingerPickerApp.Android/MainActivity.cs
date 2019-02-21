
using System;
using System.Collections;
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
            Finger finger2 = new Finger(15,15,20,"Red");
            Finger finger3 = new Finger(20, 20, 30, "Red");

            ArrayList fingers = new ArrayList();

            fingers.Add(finger);
            fingers.Add(finger2);
            fingers.Add(finger3);

            for(int i = 0; i < fingers.Count; i++)
            {
                Console.WriteLine("finger picker X " + i + " = " + finger.getFingerX());
            }

        }
    }
}