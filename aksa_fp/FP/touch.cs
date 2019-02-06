
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FP
{
    [Activity(Label = "touch", Theme = "@android:style/Theme.Black.NoTitleBar")]
    public class touch : Activity
    {

        private TextView detectTouch;
        private LinearLayout backgroundTouch;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //getting the xml file
            SetContentView(Resource.Layout.touch_layout);

            //getting the text view by the "touchInfoTextView" id from the xml file
            detectTouch = FindViewById<TextView>(Resource.Id.touchInfoTextView);
            backgroundTouch = FindViewById<LinearLayout>(Resource.Id.touchDetect);

            System.Diagnostics.Debug.WriteLine("Console, where are you?");
        }
        public override bool OnTouchEvent(MotionEvent e)
        {
            //System.Diagnostics.Debug.WriteLine("Hello, I am here");

            int pointerIndex = e.ActionIndex;

            //each finger is given an id
            int id = e.GetPointerId(pointerIndex);

            string myString = id.ToString();
            // System.Diagnostics.Debug.WriteLine(x);

            string message;
            switch (e.ActionMasked)
            {
                //if the user has his finger on the screen or moved
                case MotionEventActions.Down:
                case MotionEventActions.Move:
                case MotionEventActions.PointerDown:

                    message = myString;
                    for (pointerIndex = 0; pointerIndex < e.PointerCount; pointerIndex++)

                    {
                        id = e.GetPointerId(pointerIndex);
                        float x = e.GetX(pointerIndex);
                        float y = e.GetY(pointerIndex);
                        // printing the fingers id 
                        System.Diagnostics.Debug.WriteLine(id);
                        //use x / y values to draw the circle 

                        // the x value of where the finger touched the screen
                        System.Diagnostics.Debug.WriteLine(x);
                        // the y value of where the finger touched the screen
                        System.Diagnostics.Debug.WriteLine(y);
                    }
                    Console.WriteLine(message);
                    break;

                    // when the fingers removed - print a message
                case MotionEventActions.Up:
                    message = "1 Finger Removed";
                    break;

                default:
                    message = "Place a finger to begin";
                    break;
            }
            detectTouch.Text = message;
            return true;
        }
    }
}
