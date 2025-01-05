using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.BusinessLogic.Models.Doors;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.DataLayer.Models.Items;

public class CharacterFactory
{
    private const ConsoleColor DefaultDoorColor = ConsoleColor.White; // Fallback color

    // Character and color mappings for various objects
    private readonly Dictionary<Type, (char character, ConsoleColor color)> _typeToCharacterMap = new()
    {
        { typeof(SankaraStone), ('S', ConsoleColor.DarkYellow) },
        { typeof(Key), ('K', ConsoleColor.Green) },
        { typeof(PressurePlate), ('P', ConsoleColor.Yellow) },
        { typeof(Boobytrap), ('O', ConsoleColor.White) },
        { typeof(DisappearingBoobytrap), ('D', ConsoleColor.Cyan) },
        { typeof(ToggleDoorDecorator), ('T', ConsoleColor.Magenta) },
        { typeof(Player), ('X', ConsoleColor.White) }
    };

    public (char character, ConsoleColor color) GetCharacterWithColor(object obj)
    {
        if (obj == null)
            return (' ', DefaultDoorColor);

        if (_typeToCharacterMap.TryGetValue(obj.GetType(), out var result))
            return result;

        return (' ', DefaultDoorColor); // Default for unknown types
    }

    // Example method for wall
    public (char character, ConsoleColor color) GetWallCharacterAndColor()
    {
        return ('#', ConsoleColor.Yellow);
    }

    public (char character, ConsoleColor color) GetDoorCharacterAndColor()
    {
        return ('D', ConsoleColor.Cyan);
    }
}
