using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.BusinessLogic.Models;
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

    public static ConsoleColor MapColorToConsoleColor(Color color)
    {
        return color switch
        {
            Color.Red => ConsoleColor.Red,
            Color.Green => ConsoleColor.Green,
            Color.Blue => ConsoleColor.Blue,
            _ => ConsoleColor.Gray // Default to gray if no match
        };
    }


    public (char character, ConsoleColor color) GetDoorCharacterAndColor(List<Connection> connections)
    {
        char doorCharacter = 'D'; // Default character for door
        ConsoleColor doorColor = ConsoleColor.Gray; // Default color for door

        foreach (var connection in connections)
        {
            var door = connection.Door; // The actual door for this connection

            // Traverse through the decorator chain to get the final appearance of the door
            while (door is DoorDecorator doorDecorator)
            {
                // Apply logic to determine which door properties to extract
                // Get the base door from the decorator chain
                door = doorDecorator.DecoratedDoor;

                // Check the type of the decorator and apply corresponding behavior
                if (doorDecorator is ColoredDoorDecorator coloredDoor)
                {
                    // Convert Color to ConsoleColor
                    doorColor = MapColorToConsoleColor(coloredDoor._color);
                }

                if (doorDecorator is ToggleDoorDecorator toggleDoor)
                {
                    doorCharacter = '\u2534';
                }

                if (doorDecorator is NumberOfStonesRoomDoorDecorator stonesDoor)
                {
                    doorCharacter =
                        '='; // For example, if NumberOfStonesRoomDoorDecorator is applied, set a unique character.
                }

                // You can add more checks for other decorators here, depending on your decorator types.
            }

            // If no decorators change the appearance, fallback to default
            if (doorCharacter == 'D' && doorColor == ConsoleColor.Gray)
            {
                // This means no decorators affected the appearance, set default behavior
                doorCharacter = 'B'; // For BasicDoor, for example.
                doorColor = ConsoleColor.Green;
            }
        }

        return (doorCharacter, doorColor); // Return the selected door character and color
    }



}
