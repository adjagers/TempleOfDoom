using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.MapperStrategies
{
    public class GameLevelMapper : IMapper
    {
        private PlayerMapper _playerMapper;
        private RoomMapper _roomMapper;
        private ConnectionMapper? _connectionMapper;
        public GameLevelMapper()
        {
            _playerMapper = new PlayerMapper();
            _roomMapper = new RoomMapper(new ItemMapper());
        }

        public IGameObject Map(IDTO dto)
        {
            if (dto == null) return null;
            
            GameLevelDTO gameLevelDTO = dto as GameLevelDTO;
            // Player Mapping
            GameLevel gameLevel = new GameLevel();
            gameLevel.Player = MapPlayer(gameLevelDTO);

            // Connection Mapping
            _connectionMapper = new ConnectionMapper(gameLevel.Player);
            gameLevel.Connections = MapConnections(gameLevelDTO);

            // Room Mapping & ItemMapper
            gameLevel.Rooms = MapRooms(gameLevelDTO);

            return gameLevel;
        }

        private List<Connection> MapConnections(GameLevelDTO gameLevelDTO)
        {
            List<Connection> connections = new List<Connection>();
            foreach (ConnectionDTO connection in gameLevelDTO.Connections)
            {
                connections.Add((Connection)_connectionMapper.Map(connection));
            }
            return connections;
        }

        private List<Room> MapRooms(GameLevelDTO gameLevelDTO)
        {
            Console.WriteLine($"GameLevelMapper: Mapping {gameLevelDTO.Rooms.Count} rooms.");
            List<Room> rooms = new List<Room>();
            foreach (RoomDTO room in gameLevelDTO.Rooms)
            {
                Console.WriteLine($"GameLevelMapper: Mapping RoomDTO with Id={room.Id}");
                rooms.Add((Room)_roomMapper.Map(room));
            }
            return rooms;
        }

        private Player MapPlayer(GameLevelDTO gameLevelDTO)
        {
            Player player = (Player)_playerMapper.Map(gameLevelDTO.Player);
            return player;
        }

    }
}
