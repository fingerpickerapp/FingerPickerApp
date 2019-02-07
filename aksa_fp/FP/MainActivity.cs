using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace FP
{
    [Activity(Label = "FP5", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@android:style/Theme.Black.NoTitleBar")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            System.Diagnostics.Debug.WriteLine("Am I here?");
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            var button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += (a, b) =>
             {
                 Intent secondActivity = new Intent(this, typeof(touch));
                 StartActivity(secondActivity);
             };




        }
    }
}

