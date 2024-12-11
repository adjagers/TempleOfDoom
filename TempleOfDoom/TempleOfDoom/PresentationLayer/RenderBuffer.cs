using TempleOfDoom.HelperClasses;

namespace TempleOfDoom.PresentationLayer;

public class RenderBuffer
{
    private readonly Dictionary<Position, string> _frame = new();

    public void Clear()
    {
        _frame.Clear();
    }

    public void SetPixel(Position pos, string value)
    {
        _frame[pos] = value;
    }

    public void Render()
    {
        
    }
}