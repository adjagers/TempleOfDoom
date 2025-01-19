namespace TempleOfDoom.BusinessLogic.HelperClasses
{
    public class Position
    {
        private readonly int _x, _y;

        public Position(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int GetX() => _x;
        public int GetY() => _y;

        // Override Equals for value comparison
        public override bool Equals(object obj)
        {
            if (obj is Position other)
            {
                return _x == other._x && _y == other._y;
            }

            return false;
        }

        // Override GetHashCode to ensure positions with the same coordinates produce the same hash
        public override int GetHashCode()
        {
            return HashCode.Combine(_x, _y);
        }

        public bool IsAdjacentTo(Position other) =>
            Math.Abs(GetX() - other.GetX()) <= 1 && Math.Abs(GetY() - other.GetY()) <= 1;
    }
}