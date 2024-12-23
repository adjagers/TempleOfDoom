using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.HelperClasses;

namespace TempleOfDoom.PresentationLayer;

public class RoomRenderer
{
    public static void RenderRoom(Room currentRoom, Position playerPosition)
    {
        for (int y = 0; y < currentRoom.Dimensions.getHeight(); y++)
        {
            for (int x = 0; x < currentRoom.Dimensions.getWidth(); x++)
            {
                if (IsWallOrDoor(x, y, currentRoom))
                {
                    Console.Write(IsDoor(x, y, currentRoom) ? " D " : " # ");
                }
                else if (playerPosition.GetX() == x && playerPosition.GetY() == y)
                {
                    Console.Write(" P "); // Player
                }
                else
                {
                    Console.Write("   "); // Empty space
                }
            }
            Console.WriteLine();
        }
    }

    private static bool IsWallOrDoor(int x, int y, Room currentRoom)
    {
        return x == 0 || x == currentRoom.Dimensions.getWidth() - 1 ||
               y == 0 || y == currentRoom.Dimensions.getHeight() - 1;
    }

    private static bool IsDoor(int x, int y, Room currentRoom)
    {
        return (y == 0 && x == currentRoom.Dimensions.getWidth() / 2 && currentRoom.AdjacentRooms.ContainsKey(Direction.NORTH)) ||
               (y == currentRoom.Dimensions.getHeight() - 1 && x == currentRoom.Dimensions.getWidth() / 2 && currentRoom.AdjacentRooms.ContainsKey(Direction.SOUTH)) ||
               (x == 0 && y == currentRoom.Dimensions.getHeight() / 2 && currentRoom.AdjacentRooms.ContainsKey(Direction.WEST)) ||
               (x == currentRoom.Dimensions.getWidth() - 1 && y == currentRoom.Dimensions.getHeight() / 2 && currentRoom.AdjacentRooms.ContainsKey(Direction.EAST));
    }
}
