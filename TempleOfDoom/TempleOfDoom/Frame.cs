using TempleOfDoom.BusinessLogic.HelperClasses;
using TempleOfDoom.HelperClasses;

namespace TempleOfDoom;

public class Frame
{
    private readonly Dictionary<Position, char> _frame = new();
    private int MinX => _frame.Keys.Select(position => position.GetX()).Min();
    private int MaxX => _frame.Keys.Select(position => position.GetX()).Max();
    private int MinY => _frame.Keys.Select(position => position.GetY()).Min();
    private int MaxY => _frame.Keys.Select(position => position.GetY()).Max();


    public void Clear()
    {
        _frame.Clear();
    }


    public void SetPixel(Position pos, char value)
    {
        _frame[pos] = value;
    }


    public void Render()
    {
        for (int y = MinY; y <= MaxY; y++)
        {
            for (int x = MinX; x <= MaxX; x++)
            {
                Position position = new Position(x, y);

                if (_frame.ContainsKey(position))
                {
                    Console.Write($"{_frame[position]} ");
                }
                else
                {
                    Console.Write("  "); // Two spaces for empty positions
                }
            }

            Console.WriteLine();
        }
    }
}