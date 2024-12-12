using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.HelperClasses
{
    public class Dimensions
    {
        private readonly int _width, _height;
        public Dimensions(int width, int height)
        {
            _height = height;
            _width = width;
        }
        public int getWidth()
        {
            return _width;
        }
        public int getHeight()
        {
            return _height;
        }
        public int[] getDimensions()
        {
            return [_width, _height];
        }
    }
}