using System;
namespace FingerPickerApp
{
    public class Finger
    {
        private int fingerId;
        private int fingerX;
        private int fingerY;
        private String fingerColour;

        public Finger(int fingerId, int fingerX, int fingerY, String fingerColour)
        {
            this.fingerId = fingerId;
            this.fingerX = fingerX;
            this.fingerY = fingerY;
            this.fingerColour = fingerColour;
        }

        public void setFingerId(int fingerId)
        {
            this.fingerId = fingerId;
        }

        public int getFingerId()
        {
            return fingerId;
        }

        public void setFingerX(int fingerX)
        {
            this.fingerX = fingerX;
        }

        public int getFingerX()
        {
            return fingerX;
        }

        public void setFingerY(int fingerY)
        {
            this.fingerY = fingerY;
        }

        public int getFingerY()
        {
            return fingerY; 
        }

        public void setFingerColour(String fingerColour)
        {
            this.fingerColour = fingerColour;
        }

        public String getFingerColour()
        {
            return fingerColour;
        }

    }
}
