using System;

namespace TempleOfDoom.HelperClasses
{
public class Position
    {
        private readonly int _x, _y;
        public Position(int x, int y)
        {
            this._x = x;
            this._y = y;
        }
        public int GetX()
        {
            return _x;
        }
        public int GetY()
        {
            return _y;
        }
        public int[] GetPosition()
        {
            return [_x, _y];
        }
}
}