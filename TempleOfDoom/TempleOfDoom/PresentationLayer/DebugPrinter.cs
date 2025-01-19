using TempleOfDoom.BusinessLogic;
using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.DataLayer.Models;

namespace TempleOfDoom.PresentationLayer;

public class DebugPrinter
{

    public DebugPrinter(Room currentRoom)
    {
            PrintDoorPositions(currentRoom);
    }
    

    public static void PrintDoorPositions(Room currentRoom)
    {
        Console.WriteLine("Door Positions:");
        if (currentRoom.AdjacentRooms.ContainsKey(Direction.NORTH))
        {
            Console.WriteLine($"  NORTH Door: X={currentRoom.Dimensions.getWidth() / 2}, Y=0");
        }
        if (currentRoom.AdjacentRooms.ContainsKey(Direction.SOUTH))
        {
            Console.WriteLine($"  SOUTH Door: X={currentRoom.Dimensions.getWidth() / 2}, Y={currentRoom.Dimensions.getHeight() - 1}");
        }
        if (currentRoom.AdjacentRooms.ContainsKey(Direction.WEST))
        {
            Console.WriteLine($"  WEST Door: X=0, Y={currentRoom.Dimensions.getHeight() / 2}");
        }
        if (currentRoom.AdjacentRooms.ContainsKey(Direction.EAST))
        {
            Console.WriteLine($"  EAST Door: X={currentRoom.Dimensions.getWidth() - 1}, Y={currentRoom.Dimensions.getHeight() / 2}");
        }
    }
}
