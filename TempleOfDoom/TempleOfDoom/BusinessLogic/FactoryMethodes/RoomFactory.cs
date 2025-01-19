using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.BusinessLogic.Models.Enemy;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.BusinessLogic.FactoryMethodes
{
    public class RoomFactory
    {
        public IGameObject Create(IDTO dto)
        {
            if (dto is not RoomDTO roomDTO)
                throw new ArgumentException("Invalid DTO type provided for Room creation.", nameof(dto));

            ItemFactory itemFactory = new();

            Room room = new Room
            {
                Dimensions = new Dimensions(roomDTO.Width, roomDTO.Height),
                Items = roomDTO.Items?
                            .Select(itemDTO => itemFactory.CreateItem(itemDTO))
                            .ToList<IItem>()
                        ?? new List<IItem>(),
            };
            AddEnemiesToRoom(room, roomDTO.Enemies);
            return room;
        }

        private void AddEnemiesToRoom(Room room, List<EnemyDTO>? enemyDtos)
        {
            if (enemyDtos == null || enemyDtos.Count == 0)
                return;

            List<IAutoMovableGameObject> enemies = new();

            foreach (EnemyDTO enemyDto in enemyDtos)
            {
                EnemyAdapter enemy = new EnemyAdapter(
                    enemyDto.Type,
                    enemyDto.X,
                    enemyDto.Y,
                    enemyDto.MinX,
                    enemyDto.MaxX,
                    enemyDto.MinY,
                    enemyDto.MaxY
                );

                enemies.Add(enemy);
            }

            room.Enemies = enemies;
        }

        public List<Connection> CreateRoomConnectionsWithTransitions(
            Dictionary<int, Room> roomDict,
            List<ConnectionDTO> connectionDtos)
        {
            List<Connection> connections = new List<Connection>();
            DoorFactory doorFactory = new DoorFactory();
            LadderFactory ladderFactory = new LadderFactory();

            foreach (ConnectionDTO connectionDto in connectionDtos)
            {
                List<DoorDTO> doorDtoTypes = connectionDto.Doors;
                ITransition transition;

                if (connectionDto.LOWER.HasValue)
                {
                    transition = ladderFactory.CreateLadder(connectionDto.Ladder);
                }
                else
                {
                    transition = doorFactory.CreateDoor(doorDtoTypes);
                }
                
                
                

                void AddConnection(int? sourceRoomId, int? targetRoomId, Direction direction)
                {
                    if (sourceRoomId.HasValue && sourceRoomId.Value > 0 &&
                        targetRoomId.HasValue && targetRoomId.Value > 0 &&
                        roomDict.ContainsKey(sourceRoomId.Value) && roomDict.ContainsKey(targetRoomId.Value))
                    {
                        Room sourceRoom = roomDict[sourceRoomId.Value];
                        Room targetRoom = roomDict[targetRoomId.Value];

                        Connection connection = new Connection(targetRoom, transition);
                        sourceRoom.AdjacentRooms[direction] = targetRoom;
                        sourceRoom.AddConnection(connection);
                        connections.Add(connection);
                    }
                }

                AddConnection(connectionDto.UPPER, connectionDto.LOWER, Direction.LOWER);
                AddConnection(connectionDto.LOWER, connectionDto.UPPER, Direction.UPPER);
                AddConnection(connectionDto.NORTH, connectionDto.SOUTH, Direction.SOUTH);
                AddConnection(connectionDto.SOUTH, connectionDto.NORTH, Direction.NORTH);
                AddConnection(connectionDto.WEST, connectionDto.EAST, Direction.EAST);
                AddConnection(connectionDto.EAST, connectionDto.WEST, Direction.WEST);
            }

            return connections;
        }
        
        
        
    }
}