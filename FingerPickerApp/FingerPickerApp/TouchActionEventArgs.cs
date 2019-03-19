using System;
using Xamarin.Forms;

namespace TouchTracking
{
    public class TouchActionEventArgs : EventArgs
    {
        public TouchActionEventArgs(long id, TouchActionType type, float locationX, float locationY, bool isInContact)
        {
            //Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< Run no = 5");
            Id = id;
            Type = type;
            LocationX = locationX;
            LocationY = locationY;
            IsInContact = isInContact;
        }

        public long Id { private set; get; }

        public TouchActionType Type { private set; get; }

        public float LocationX { private set; get; }

        public float LocationY { private set; get; }

        public bool IsInContact { private set; get; }
    }
}
