using System;
using System.Collections.Generic;
using System.Linq;
using FingerPickerApp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using Android.Views;
using Android.OS;
using System.Runtime.Remoting.Contexts;
using Android.Content;
using Xamarin.Essentials;

[assembly: ResolutionGroupName("XamarinDocs")]
[assembly: ExportEffect(typeof(TouchTracking.Droid.TouchEffect), "TouchEffect")]

namespace TouchTracking.Droid
{
    public class TouchEffect : PlatformEffect
    {
        Android.Views.View view;
        Element formsElement;
        TouchTracking.TouchEffect libTouchEffect;
        bool capture;
        float locationX;
        float locationY;

        static Dictionary<Android.Views.View, TouchEffect> viewDictionary = new Dictionary<Android.Views.View, TouchEffect>();

        static Dictionary<int, TouchEffect> idToEffectDictionary = new Dictionary<int, TouchEffect>();

        protected override void OnAttached()
        {
            // Get the Android View corresponding to the Element that the effect is attached to
            view = Control == null ? Container : Control;

            // Get access to the TouchEffect class in the .NET Standard library
            TouchTracking.TouchEffect touchEffect =  (TouchTracking.TouchEffect) Element.Effects.FirstOrDefault(e => e is TouchTracking.TouchEffect);

            if (touchEffect != null && view != null)
            {
                viewDictionary.Add(view, this);

                formsElement = Element;

                libTouchEffect = touchEffect;

                // Set event handler on View
                view.Touch += OnTouch;
            }
        }

        protected override void OnDetached()
        {
            if (viewDictionary.ContainsKey(view))
            {
                viewDictionary.Remove(view);
                view.Touch -= OnTouch;
            }
        }

        void OnTouch(object sender, Android.Views.View.TouchEventArgs args)
        {
            // Two object common to all the events
            Android.Views.View senderView = sender as Android.Views.View;
            MotionEvent motionEvent = args.Event;

            // Get the pointer index
            int pointerIndex = motionEvent.ActionIndex;

            // Get the id that identifies a finger over the course of its progress
            int id = motionEvent.GetPointerId(pointerIndex);

            //Console.WriteLine("motionx = " + motionEvent.GetX(pointerIndex) + " motiony = " + motionEvent.GetY(pointerIndex));

            locationX = motionEvent.GetX(pointerIndex);
            locationY = motionEvent.GetY(pointerIndex);

            // Use ActionMasked here rather than Action to reduce the number of possibilities
            // when the screen is tapped, this line of code runs
            switch (args.Event.ActionMasked)
            {
                case MotionEventActions.Down:
                case MotionEventActions.PointerDown: // checks for pointer down

                    Console.Write("what is this? = ");
                    Console.WriteLine(this);
                    FireEvent(this, id, TouchActionType.Pressed, locationX, locationY, true);

                    idToEffectDictionary.Add(id, this);

                    capture = libTouchEffect.Capture;
                   
                     
                   
                    break;

               case MotionEventActions.Move:
                    // Multiple Move events are bundled, so handle them in a loop

                    // this line of code runs when the finger is moved.ss
                    for (pointerIndex = 0; pointerIndex < motionEvent.PointerCount; pointerIndex++)
                    {
                        // this stores the pointer id of eadh finger until removed from the screen
                        // when the pointer is removed another finger is placed, the new finger takes the index
                        // number of the previous finger.
                        /*id = motionEvent.GetPointerId(pointerIndex);
                        locationX = motionEvent.GetX(pointerIndex);
                        locationY = motionEvent.GetY(pointerIndex);*/
                        id = pointerIndex;
                        locationX = motionEvent.GetX(pointerIndex);
                        locationY = motionEvent.GetY(pointerIndex);
                        if (capture)
                        {
                            FireEvent(this, id, TouchActionType.Moved, locationX, locationY, true);
                        }
                        else
                        {
                            if (idToEffectDictionary[id] != null)
                            {
                                FireEvent(idToEffectDictionary[id], id, TouchActionType.Moved, locationX, locationY, true);
                            }
                        }
                    }
                    break;

                case MotionEventActions.Up:
                case MotionEventActions.PointerUp: // as the finger is removed, this line of code runs
                    if (capture)
                    {
                        FireEvent(this, id, TouchActionType.Released, locationX, locationY, false);
                    }
                    else
                    {
                        //CheckForBoundaryHop(id, screenPointerCoords);
                        if (idToEffectDictionary[id] != null)
                        {
                            FireEvent(idToEffectDictionary[id], id, TouchActionType.Released, locationX, locationY, false);
                        }
                    }
                    idToEffectDictionary.Remove(id);
                    break;

                case MotionEventActions.Cancel:
                    if (capture)
                    {
                        FireEvent(this, id, TouchActionType.Cancelled, locationX, locationY, false);
                    }
                    else
                    {
                        if (idToEffectDictionary[id] != null)
                        {
                            FireEvent(idToEffectDictionary[id], id, TouchActionType.Cancelled, locationX, locationY, false);
                        }
                    }
                    idToEffectDictionary.Remove(id);
                    break;
            }
        }

        /*void CheckForBoundaryHop(int id, Point pointerLocation)
        { 
            TouchEffect touchEffectHit = null;

            foreach (Android.Views.View view in viewDictionary.Keys)
            {
                // Get the view rectangle
                try
                {
                    //view.GetLocationOnScreen(twoIntArray);
                }
                catch // System.ObjectDisposedException: Cannot access a disposed object.
                {
                    continue;
                }
                //Rectangle viewRect = new Rectangle(twoIntArray[0], twoIntArray[1], view.Width, view.Height);

                /*if (viewRect.Contains(pointerLocation))
                {
                    touchEffectHit = viewDictionary[view];
                }*/
            //}

            /*if (touchEffectHit != idToEffectDictionary[id])
            {
                if (idToEffectDictionary[id] != null)
                {
                    FireEvent(idToEffectDictionary[id], id, TouchActionType.Exited, locationX, locationY, true);
                }
                if (touchEffectHit != null)
                {
                    FireEvent(touchEffectHit, id, TouchActionType.Entered, locationX, locationY, true);
                }
                idToEffectDictionary[id] = touchEffectHit;
            }
        }*/

        void FireEvent(TouchEffect touchEffect, int id, TouchActionType actionType,float pointX, float pointY, bool isInContact)
        {
            // Get the method to call for firing events
            Action<Element, TouchActionEventArgs> onTouchAction = touchEffect.libTouchEffect.OnTouchAction;

            // Get the location of the pointer within the view
            // touchEffect.view.GetLocationOnScreen(twoIntArray);
            // Call the method
            // THIS IS SENDING THE TOUCH TO THE TOUCH EVENT ARGS
            onTouchAction(touchEffect.formsElement, new TouchActionEventArgs(id, actionType, locationX, locationY, isInContact));
        }
    }
}
