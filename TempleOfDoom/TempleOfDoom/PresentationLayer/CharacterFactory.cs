using TempleOfDoom.BusinessLogic.Models.Doors;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.DataLayer.Models.Items;

public class CharacterFactory
{
    private readonly Dictionary<Type, char> _typeToCharacterMap = new()
    {
        { typeof(SankaraStone), 'S' },
        { typeof(Key), 'K' },
        { typeof(PressurePlate), 'P' },
        { typeof(Boobytrap), 'B' },
        { typeof(DisappearingBoobytrap), 'D' },
        { typeof(ClosingGateDecorator), '|' },
        { typeof(ColoredDoorDecorator), '/' },
        { typeof(Player), 'X' }
    };

    public char GetCharacter(object obj)
    {
        if (obj == null) return ' '; // Default voor null objecten

        var type = obj.GetType();
        return _typeToCharacterMap.TryGetValue(type, out var character) 
            ? character 
            : ' '; // Default voor onbekende types
    }
}