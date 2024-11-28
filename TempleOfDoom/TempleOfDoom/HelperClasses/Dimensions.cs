using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return [width, height];
        }
        public void setDimensions(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }
}