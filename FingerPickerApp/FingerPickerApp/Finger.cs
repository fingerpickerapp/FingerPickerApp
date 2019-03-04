using System;
namespace FingerPickerApp
{
    public class Finger
    {
        private int fingerId;
        private double fingerX;
        private double fingerY;
        private String fingerColour;

        public Finger(int fingerId, double fingerX, double fingerY, String fingerColour)
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

        public void setFingerX(double fingerX)
        {
            this.fingerX = fingerX;
        }

        public double getFingerX()
        {
            return fingerX;
        }

        public void setFingerY(double fingerY)
        {
            this.fingerY = fingerY;
        }

        public double getFingerY()
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
