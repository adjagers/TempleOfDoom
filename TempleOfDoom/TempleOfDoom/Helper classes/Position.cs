using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.Helper_classes
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
            return [x, y];
        }
        public void setPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
