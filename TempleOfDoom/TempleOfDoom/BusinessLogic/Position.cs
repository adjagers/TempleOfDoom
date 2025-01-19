namespace TempleOfDoom.BusinessLogic
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

        public Position Add(Position other) => new Position(_x + other._x, _y + other._y);

        // This is for the Frame.cs it's used to Hash the position for the dictionary that is used there.
        public override int GetHashCode()
        {
            return HashCode.Combine(_x, _y);
        }

        // Checks whether the enemy is nearby
        public bool IsAdjacentTo(Position other) =>
            Math.Abs(GetX() - other.GetX()) <= 1 && Math.Abs(GetY() - other.GetY()) <= 1;
    }
}