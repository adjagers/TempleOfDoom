using TempleOfDoom.DataLayer.Decorators;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;

public class DoorFactory
{
    private readonly Player _player;

    public DoorFactory(Player player)
    {
        _player = player ?? throw new ArgumentNullException(nameof(player));
    }

    public IDoor CreateDoor(DoorDTO dtoDoor)
    {
        if (dtoDoor == null)
            throw new ArgumentNullException(nameof(dtoDoor));

        // Standaard basisdeur (initieel gesloten)
        IDoor door = new BasicDoor(initialState: false);

        switch (dtoDoor.Type.ToLower())
        {
            case "colored":
                door = new ColoredDoorDecorator(door, dtoDoor.Color);
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
                door = new NumberOfStonesDoorDecorator(door, dtoDoor.No_of_stones);
                break;
            default:
                Console.WriteLine("Warning: Unrecognized door type.");
                break;
        }

        return door;
    }
}
