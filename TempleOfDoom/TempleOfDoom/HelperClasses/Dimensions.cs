using System;

namespace TempleOfDoom.HelperClasses
{
    public class Dimensions
    {
        private int width, height;

        public Dimensions(int width, int height)
        {
            setDimensions(width, height);
        }

        public int getWidth()
        {
            return width;
        }

        public int getHeight()
        {
            return height;
        }

        public int[] getDimensions()
        {
            return new int[] { width, height };  // Correcte manier om een array te retourneren
        }

        public void setDimensions(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }
}