using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;

public class ConnectionMapper : IMapper
{
    private Player _player;
    private readonly DoorFactory _doorFactory;
    public ConnectionMapper(Player player)
    {
        _player = player;
        _doorFactory = new DoorFactory(_player);
    }
    public IGameObject Map(IDTO dto)
    {
        if (dto == null) return null;

        var connectionDTO = dto as ConnectionDTO;
        IDoor door = new BasicDoor(true);
        foreach (var dtoDoor in connectionDTO.Doors)
        {
            door = _doorFactory.CreateDoor(dtoDoor);
        }

        var connection = new Connection();
        MapConnection(connection, connectionDTO);
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

    private void MapConnection(Connection connection, ConnectionDTO connectionDTO)
    {
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
    }

}
