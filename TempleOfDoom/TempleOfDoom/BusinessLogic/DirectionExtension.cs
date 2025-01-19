namespace TempleOfDoom.BusinessLogic
{
    public static class DirectionExtension
    {
        // Converts a Direction to its corresponding position offset
        public static Position GetDirectionValues(this Direction direction) => direction switch
        {
            Direction.WEST => new Position(-1, 0), // Move left
            Direction.EAST => new Position(1, 0), // Move right
            Direction.SOUTH => new Position(0, 1), // Move down
            Direction.NORTH => new Position(0, -1), // Move up
            _ => throw new ArgumentException("Unknown direction")
        };

        // Checks if the direction is horizontal
        public static bool IsHorizontal(this Direction direction) => direction is Direction.WEST or Direction.EAST;

        // Maps ConsoleKey input to a Direction, returns null for unsupported keys
        public static Direction? ToDirectionOrNull(ConsoleKey consoleKey) => consoleKey switch
        {
            ConsoleKey.UpArrow or ConsoleKey.W => Direction.NORTH,
            ConsoleKey.LeftArrow or ConsoleKey.A => Direction.WEST,
            ConsoleKey.RightArrow or ConsoleKey.D => Direction.EAST,
            ConsoleKey.DownArrow or ConsoleKey.S => Direction.SOUTH,
            _ => null
        };

        // Gets the opposite direction
        public static Direction Opposite(this Direction direction) => direction switch
        {
            Direction.WEST => Direction.EAST,
            Direction.EAST => Direction.WEST,
            Direction.SOUTH => Direction.NORTH,
            Direction.NORTH => Direction.SOUTH,
            _ => throw new InvalidOperationException("Invalid direction")
        };

        public static Position GetPositionForDirection(this Direction direction, int width, int height)
        {
            switch (direction)
            {
                case Direction.NORTH:
                    return new Position(width / 2, 0);
                case Direction.SOUTH:
                    return new Position(width / 2, height - 1);
                case Direction.WEST:
                    return new Position(0, height / 2);
                case Direction.EAST:
                    return new Position(width - 1, height / 2);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), "Unsupported direction");
            }
        }
    }
}