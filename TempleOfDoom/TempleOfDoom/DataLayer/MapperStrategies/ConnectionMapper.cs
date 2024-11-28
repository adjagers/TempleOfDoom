using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;

public class ConnectionMapper : IMapper
{
    private Player _player;
    public ConnectionMapper(Player player)
    {
        _player = player;
    }
    public IGameObject Map(IDTO dto)
    {
        if (dto == null) return null;
        var connectDTO = dto as ConnectionDTO;
        IDoor door = new BasicDoor(true);
        foreach (var dtoDoor in connectDTO.Doors)
        {
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
        }
        var connectionGOM = new Connection();
        if (connectDTO.NORTH == 0)
        {
            connectionGOM.EAST = connectDTO.EAST;
            connectionGOM.WEST = connectDTO.WEST;
        }
        else
        {
            connectionGOM.NORTH = connectDTO.NORTH;
            connectionGOM.SOUTH = connectDTO.SOUTH;
        }
        connectionGOM.Doors = door;

        DebugPrint(connectionGOM);
        return connectionGOM;
    }

    public void DebugPrint(Connection connectGOM)
    {
        if (connectGOM.NORTH == 0)
        {
            Console.WriteLine(connectGOM.EAST.ToString() + " and " + connectGOM.WEST.ToString());
        }
        else
        {
            Console.WriteLine(connectGOM.NORTH.ToString() + " and " + connectGOM.SOUTH);
        }
        Console.WriteLine("and door is " + connectGOM.Doors.GetType());
    }
}
