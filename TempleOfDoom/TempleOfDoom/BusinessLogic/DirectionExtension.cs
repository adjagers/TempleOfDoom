namespace TempleOfDoom.BusinessLogic;

public static class DirectionExtension
{
    public static Position GetDirectionValues(this Direction direction) => direction switch
    {
        Direction.WEST => new Position(-1, 0),
        Direction.EAST => new Position(1, 0),
        Direction.SOUTH => new Position(0, 1),
        Direction.NORTH => new Position(0, -1),
        _ => new Position(0, 0)
    };

    public static bool IsHorizontal(this Direction direction) => direction switch
    {
        Direction.WEST => true,
        Direction.EAST => true,
        _ => false
    };

    public static Direction? ToDirectionOrNull(ConsoleKey consoleKey) => consoleKey switch
    {
        ConsoleKey.UpArrow or ConsoleKey.W => Direction.NORTH,
        ConsoleKey.LeftArrow or ConsoleKey.A => Direction.WEST,
        ConsoleKey.RightArrow or ConsoleKey.D => Direction.EAST,
        ConsoleKey.DownArrow or ConsoleKey.S => Direction.SOUTH,
        _ => null
    };

    public static Direction Opposite(this Direction direction) => direction switch
    {
        Direction.WEST => Direction.EAST,
        Direction.EAST => Direction.WEST,
        Direction.SOUTH => Direction.NORTH,
        Direction.NORTH => Direction.SOUTH,
        _ => throw new InvalidOperationException("Invalid direction")
    };
}