using TempleOfDoom.BusinessLogic;
using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.BusinessLogic.Models.Doors;
using TempleOfDoom.BusinessLogic.Models.Enemy;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.DataLayer.Models.Items;
using TempleOfDoom.Interfaces;

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
        { typeof(EnemyAdapter), ('Z', ConsoleColor.Red) },
        { typeof(Player), ('X', ConsoleColor.White) },
        { typeof(Ladder), ('!', ConsoleColor.Blue) }
    };

    public static char GetTransitionCharacter(ITransition transition)
    {
        while (transition is DoorDecorator doorDecorator)
        {
            if (doorDecorator is ColoredDoorDecorator)
            {
                
            }
            else if (doorDecorator is ToggleDoorDecorator)
            {
                return '\u2534';
            }
            else if (doorDecorator is NumberOfStonesRoomDoorDecorator)
            {
                return '=';
            }

            // Move to the next level in the decorator chain
            transition = doorDecorator.DecoratedDoor;
        }

        // Handle base transitions
        return transition switch
        {
            Ladder _ => 'L',
            BasicDoor _ => 'D',
            _ => '?'
        };
    }

    public static char GetWallCharacter()
    {
        return '#';
    }


    public static char GetMovableCharacter(IMovableGameObject movableGameObject)
    {
        return movableGameObject switch
        {
            EnemyAdapter _ => 'E',
            Player _ => 'X',
        };
    }

    public static char GetItemDisplay(IItem item)
    {
        return item switch
        {
            SankaraStone _ => 'S',
            PressurePlate _ => 'P',
            Key _ => 'K',
            DisappearingBoobytrap _ => 'D',
            Boobytrap _ => 'B',
            _ => '?'
        };
    }
    public (char character, ConsoleColor color) GetCharacterWithColor(object obj)
    {
        if (obj == null)
            return (' ', DefaultDoorColor);

        if (_typeToCharacterMap.TryGetValue(obj.GetType(), out var result))
            return result;

        return (' ', DefaultDoorColor); // Default for unknown types
    }
}
