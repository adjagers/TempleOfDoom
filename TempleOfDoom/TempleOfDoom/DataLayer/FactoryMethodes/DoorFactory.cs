using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;

public class DoorFactory
{
    private readonly Player _player;

    public DoorFactory(Player player)
    {
        _player = player;
    }

    public IDoor CreateDoor(DoorDTO dtoDoor)
    {
        IDoor door = new BasicDoor(true);
        switch (dtoDoor.Type)
        {
            case "colored":
                door.SetInitialState(false);
                door = new ColoredDoorDecorator(door, dtoDoor.Color, _player.Inventory);
                break;
            case "toggle":
                door.SetInitialState(false);
                door = new ToggleDoorDecorator(door);
                break;
            case "closing gate":
                door.SetInitialState(true);
                door = new ClosingGateDecorator(door);
                break;
            case "open on odd":
                door = new OpenOnOddDoorDecorator(door, _player);
                break;
            case "open on stones in room":
                door = new NumberOfStonesInRoomDoorDecorator(door, dtoDoor.No_of_stones);
                break;
            default:
                break;
        }
        return door;
    }
}
