using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.BusinessLogic.Models.Doors;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;

public class DoorFactory
{
    public IDoor CreateDoor(DoorDTO dtoDoor)
    {
        if (dtoDoor == null)
            throw new ArgumentNullException(nameof(dtoDoor));

        // Standaard basisdeur (initieel gesloten)
        IDoor door = new BasicDoor(initialState: false);

        switch (dtoDoor.Type.ToLower())
        {
            case "colored":
                Color keyColor =
                Enum.Parse<Color>(dtoDoor.Color ?? throw new InvalidDataException("A key needs a color value"),
                true);
                door = new ColoredDoorDecorator(door, keyColor);
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
                door = new NumberOfStonesRoomDoorDecorator(door, dtoDoor.No_of_stones);
                break;
            default:
                Console.WriteLine("Warning: Unrecognized door type.");
                break;
        }

        return door;
    }
}
