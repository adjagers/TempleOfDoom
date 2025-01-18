using System;

namespace TempleOfDoom.HelperClasses
{
    public class Position
    {
        private readonly int _x, _y;

        public Position(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int GetX()
        {
            return _x;
        }

        public int GetY()
        {
            return _y;
        }

        public bool IsAdjacentTo(Position other) =>
            Math.Abs(GetX() - other.GetX()) <= 1 && Math.Abs(GetY() - other.GetY()) <= 1;
    }
}