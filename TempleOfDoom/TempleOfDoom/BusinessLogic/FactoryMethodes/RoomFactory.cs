using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.HelperClasses;
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
                        ?? new List<IItem>()
            };
            return room;
        }

        public List<Connection> CreateRoomConnectionsWithDoors(
            Dictionary<int, Room> roomDict,
            List<ConnectionDTO> connectionDtos)
        {
            List<Connection> connections = new List<Connection>();
            DoorFactory doorFactory = new DoorFactory();

            foreach (ConnectionDTO connectionDto in connectionDtos)
            {
                List<DoorDTO> doorDtoTypes = connectionDto.Doors;
                IDoor door = doorFactory.CreateDoor(doorDtoTypes);

                void AddConnection(int? sourceRoomId, int? targetRoomId, Direction direction)
                {
                    if (sourceRoomId.HasValue && sourceRoomId.Value > 0 &&
                        targetRoomId.HasValue && targetRoomId.Value > 0 &&
                        roomDict.ContainsKey(sourceRoomId.Value) && roomDict.ContainsKey(targetRoomId.Value))
                    {
                        Room sourceRoom = roomDict[sourceRoomId.Value];
                        Room targetRoom = roomDict[targetRoomId.Value];

                        Connection connection = new Connection(targetRoom, door);
                        sourceRoom.AdjacentRooms[direction] = targetRoom;
                        sourceRoom.AddConnection(connection);
                        connections.Add(connection);
                    }
                }

                AddConnection(connectionDto.NORTH, connectionDto.SOUTH, Direction.SOUTH);
                AddConnection(connectionDto.SOUTH, connectionDto.NORTH, Direction.NORTH);
                AddConnection(connectionDto.WEST, connectionDto.EAST, Direction.EAST);
                AddConnection(connectionDto.EAST, connectionDto.WEST, Direction.WEST);
            }

            return connections;
        }
    }
}