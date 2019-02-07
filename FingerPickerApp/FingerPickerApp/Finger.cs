using System;

namespace FingerPickerApp
{
    public class Finger
    {
        int fingerID;
        int fingerX;
        int fingerY;

        public Finger(int fingerID, int fingerX, int fingerY)
        {
            this.fingerID = fingerID;
            this.fingerX = fingerX;
            this.fingerY = fingerY; 
        }

        public void SetFingerID(int fingerID)
        {
            this.fingerID = fingerID;
        }

        public int GetFingerID()
        {
            return this.fingerID;
        }

        public void SetFingerX(int fingerX)
        {
            this.fingerX = fingerX;
        }

        public int GetFingerX()
        {
            return this.fingerX;
        }

        public void SetFingerY(int fingerY)
        {
            this.fingerY = fingerY; 
        }

        public int GetFingerY()
        {
            return this.fingerY;
        }

    }
}
