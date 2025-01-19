using TempleOfDoom.BusinessLogic;
using TempleOfDoom.BusinessLogic.Models.Doors;
using TempleOfDoom.DataLayer.DTO;
namespace TempleOfDoom.DataLayer.FactoryMethodes;

public class DoorFactory
{
    public IDoor CreateDoor(List<DoorDTO> dtoDoors)
    {
        IDoor door = new BasicDoor(initialState: false);

        foreach (DoorDTO dtoDoor in dtoDoors)
        {
            switch (dtoDoor.Type.ToLower())
            {
                case "colored":
                    // Use a default color if no color is provided
                    door = new ColoredDoorDecorator(door, GetDoorColor(dtoDoor.Color));
                    break;
                case "toggle":
                    door = new ToggleDoorDecorator(door);
                    break;
                case "closing gate":
                    door = new ClosingGateDecorator(door);
                    break;
                case "open on odd":
                    door = new OpenOnOddDecorator(door);
                    break;
                case "open on stones in room":
                    door = new NumberOfStonesRoomDoorDecorator(door, dtoDoor.NoOfStones);
                    break;
                default:
                    Console.WriteLine("Warning: Unrecognized door type.");
                    break;
            }
        }

        return door;
    }

    private Color GetDoorColor(string color)
    {
        return color != null ? Enum.Parse<Color>(color, true) : Color.Blue; // Default color
    }
}