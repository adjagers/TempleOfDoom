using System;
using System.Collections.Generic;
using System.Linq;
using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.BusinessLogic.FactoryMethodes;
using TempleOfDoom.BusinessLogic.FactoryMethods;
using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.BusinessLogic.FactoryMethodes
{
    public class GameLevelFactory : IFactory
    {
        private readonly RoomFactory _roomFactory = new();
        private Dictionary<int, Room> roomDict = new();

        public IGameObject Create(IDTO dto)
        {
            // Ensure the correct type of DTO is used
            GameLevelDTO gameLevelDTO = dto as GameLevelDTO
                                        ?? throw new InvalidOperationException("Invalid DTO type. Expected GameLevelDTO.");

            // Step 1: Fill roomDict and connect rooms
            ConnectRoomsWithConnections(gameLevelDTO.Rooms, gameLevelDTO.Connections);

            // Step 2: Create doors and connect rooms via CreateRoomConnectionsWithDoors
            foreach (var connectionDto in gameLevelDTO.Connections)
            {
                CreateRoomConnectionsWithDoors(connectionDto);
            }

            // Step 3: Create a list of rooms for the GameLevel object
            List<Room> rooms = new();
            foreach (var roomDto in gameLevelDTO.Rooms)
            {
                if (roomDict.TryGetValue(roomDto.Id, out var room))
                {
                    rooms.Add(room);
                }
            }

            // Step 4: Create and return the GameLevel object
            return new GameLevel
            {
                Connections = new List<Connection>(), // Add logic here if needed
                Rooms = rooms
            };
        }

        public void ConnectRoomsWithConnections(List<RoomDTO> roomDtos, List<ConnectionDTO> connectionDtos)
        {
            // Step 1: Populate the dictionary to map Room IDs to Room objects
            foreach (var roomDto in roomDtos)
            {
                roomDict[roomDto.Id] = (Room)_roomFactory.Create(roomDto);
            }

            // Step 2: Connect rooms based on the connections
            foreach (var connectionDto in connectionDtos)
            {
                if (connectionDto.NORTH > 0 && connectionDto.SOUTH > 0)
                {
                    var northRoom = roomDict[connectionDto.NORTH];
                    var southRoom = roomDict[connectionDto.SOUTH];

                    // Add to the adjacency dictionary
                    northRoom.AdjacentRooms[Direction.SOUTH] = southRoom;
                    southRoom.AdjacentRooms[Direction.NORTH] = northRoom;

                    Console.WriteLine($"Connected Room {connectionDto.NORTH} (North) to Room {connectionDto.SOUTH} (South)");
                }

                if (connectionDto.WEST > 0 && connectionDto.EAST > 0)
                {
                    var westRoom = roomDict[connectionDto.WEST];
                    var eastRoom = roomDict[connectionDto.EAST];

                    // Add to the adjacency dictionary
                    westRoom.AdjacentRooms[Direction.EAST] = eastRoom;
                    eastRoom.AdjacentRooms[Direction.WEST] = westRoom;

                    Console.WriteLine($"Connected Room {connectionDto.WEST} (West) to Room {connectionDto.EAST} (East)");
                }
            }
        }

        public void CreateRoomConnectionsWithDoors(ConnectionDTO connectionDto)
        {
            List<DoorDTO> doorDtoTypes = connectionDto.Doors;
            DoorFactory doorFactory = new DoorFactory();
            IDoor door = doorFactory.CreateDoor(doorDtoTypes);

            foreach (var kvp in roomDict)
            {
                Console.WriteLine($"RoomDict contains: Key = {kvp.Key}, Room = {kvp.Value}");
            }

            Dictionary<Direction, Connection> dict = new();

            // Local function to add connections with validation
            void AddConnection(Direction direction, int? roomId)
            {
                if (roomId.HasValue && roomId.Value > 0 && roomDict.ContainsKey(roomId.Value))
                {
                    dict.Add(direction, new Connection(roomDict[roomId.Value], door));
                }
                else
                {
                    Console.WriteLine($"Invalid roomId: {roomId}. Skipping connection for direction {direction}.");
                }
            }

            Console.WriteLine($"Processing ConnectionDTO: NORTH={connectionDto.NORTH}, EAST={connectionDto.EAST}, SOUTH={connectionDto.SOUTH}, WEST={connectionDto.WEST}");

            if (connectionDto.NORTH != null) AddConnection(Direction.NORTH, connectionDto.NORTH);
            if (connectionDto.EAST != null) AddConnection(Direction.EAST, connectionDto.EAST);
            if (connectionDto.SOUTH != null) AddConnection(Direction.SOUTH, connectionDto.SOUTH);
            if (connectionDto.WEST != null) AddConnection(Direction.WEST, connectionDto.WEST);

            foreach (var entry in dict)
            {
                // Use roomDict to find the RoomDTO.Id
                var connectedRoom = entry.Value.ConnectedRoom;
                var connectedRoomId = roomDict.FirstOrDefault(x => x.Value == connectedRoom).Key;

                Console.WriteLine($"Direction: {entry.Key}, Connected Room ID: {connectedRoomId}, Door Type: {entry.Value.Door.GetType().Name}");
            }
        }
    }
}
