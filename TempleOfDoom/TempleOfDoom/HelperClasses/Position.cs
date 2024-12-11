using System;

namespace TempleOfDoom.HelperClasses
{
    public class Position
    {
        private int x, y;

        public Position(int x, int y)
        {
            setPosition(x, y);
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public int[] getPosition()
        {
            return new int[] { x, y };  // Correcte manier om een array te retourneren
        }

        public void setPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}