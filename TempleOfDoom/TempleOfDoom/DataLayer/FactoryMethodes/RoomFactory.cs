using TempleOfDoom.BusinessLogic;
using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.BusinessLogic.Models.Enemy;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.FactoryMethodes
{
    public class RoomFactory
    {
        public IGameObject Create(IDTO dto)
        {
            if (dto is not RoomDTO roomDTO)
                throw new ArgumentException("Invalid DTO type provided for Room creation.", nameof(dto));

            ItemFactory itemFactory = new();

            List<IItem> items = roomDTO.Items?
                                    .Select(itemDTO => itemFactory.CreateItem(itemDTO))
                                    .ToList()
                                ?? new List<IItem>();

            List<IAutoMovableGameObject> enemies = roomDTO.Enemies?
                .Select(enemyDto => new EnemyAdapter(
                    enemyDto.Type,
                    enemyDto.X,
                    enemyDto.Y,
                    enemyDto.MinX,
                    enemyDto.MaxX,
                    enemyDto.MinY,
                    enemyDto.MaxY
                )).Cast<IAutoMovableGameObject>()
                .ToList() ?? new List<IAutoMovableGameObject>();


            Room room = new Room(
                new Dimensions(roomDTO.Width, roomDTO.Height),
                items,
                enemies
            );
            return room;
        }

        public List<Connection> CreateRoomConnectionsWithTransitions(Dictionary<int, Room> roomDict,
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