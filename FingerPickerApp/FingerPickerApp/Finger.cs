using System;
namespace FingerPickerApp
{
    public class Finger
    {
        private int fingerId;
        private double fingerX;
        private double fingerY;
        private int fingerColour;
        private int fingerColour1;
        private int fingerColour2;

        public Finger(int fingerId, double fingerX, double fingerY, int fingerColour, int fingerColour1, int fingerColour2)
        {
            this.fingerId = fingerId;
            this.fingerX = fingerX;
            this.fingerY = fingerY;
            this.fingerColour = fingerColour;
            this.fingerColour1 = fingerColour1;
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

        public void setFingerColour(int fingerColour)
        {
            this.fingerColour = fingerColour;
        }

        public int getFingerColour()
        {
            return fingerColour;
        }

        public void setFingerColour1(int fingerColour1)
        {
            this.fingerColour1 = fingerColour1;
        }

        public int getFingerColour1()
        {
            return fingerColour1;
        }
        public void setFingerColour2(int fingerColour2)
        {
            this.fingerColour2 = fingerColour2;
        }

        public int getFingerColour2()
        {
            return fingerColour2;
        }

    }
}
