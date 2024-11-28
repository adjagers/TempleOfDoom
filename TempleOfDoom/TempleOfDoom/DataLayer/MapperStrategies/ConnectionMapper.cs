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
        var connectionDTO = dto as ConnectionDTO;
        IDoor door = new BasicDoor(true);
        foreach (var dtoDoor in connectionDTO.Doors)
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
        var connection = new Connection();
        if (connectionDTO.NORTH == 0)
        {
            connection.EAST = connectionDTO.EAST;
            connection.WEST = connectionDTO.WEST;
        }
        else
        {
            connection.NORTH = connectionDTO.NORTH;
            connection.SOUTH = connectionDTO.SOUTH;
        }
        connection.Doors = door;

        DebugPrint(connection);
        return connection;
    }

    public void DebugPrint(Connection connection)
    {
        if (connection.NORTH == 0)
        {
            Console.WriteLine(connection.EAST.ToString() + " and " + connection.WEST.ToString());
        }
        else
        {
            Console.WriteLine(connection.NORTH.ToString() + " and " + connection.SOUTH);
        }
        Console.WriteLine("and door is " + connection.Doors.GetType());
    }
}
