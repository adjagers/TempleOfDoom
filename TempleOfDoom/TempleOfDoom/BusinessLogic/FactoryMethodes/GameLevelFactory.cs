using System;
using System.Collections.Generic;
using System.Linq;
using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.BusinessLogic.FactoryMethodes;
using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.BusinessLogic.FactoryMethodes
{
    public class GameLevelFactory : IFactory
    {
        private readonly RoomFactory _roomFactory = new();
        private readonly Dictionary<int, Room> _roomDict = new();

        public IGameObject Create(IDTO dto)
        {
            GameLevelDTO gameLevelDTO = dto as GameLevelDTO
                                        ?? throw new InvalidOperationException("Invalid DTO type. Expected GameLevelDTO.");

            // Create rooms and connections
            CreateRooms(gameLevelDTO.Rooms);
            AddConnectionsToRooms(gameLevelDTO.Connections);

            List<Room> rooms = _roomDict.Values.ToList();
            Player player = new Player
            {
                Lives = gameLevelDTO.Player.Lives,
                Position = new Position(gameLevelDTO.Player.StartX, gameLevelDTO.Player.StartY),
                CurrentRoom = _roomDict.ContainsKey(gameLevelDTO.Player.StartRoomId) 
                    ? _roomDict[gameLevelDTO.Player.StartRoomId]
                    : throw new InvalidOperationException($"Room with ID {gameLevelDTO.Player.StartRoomId} does not exist.")
            };

            return new GameLevel
            {
                Rooms = rooms,
                Player = player
            };
        }
        
        private void CreateRooms(List<RoomDTO> roomDtos)
        {
            foreach (RoomDTO roomDto in roomDtos)
            {
                _roomDict[roomDto.Id] = (Room)_roomFactory.Create(roomDto);
            }
        }
        private void ConnectRoomsWithConnections(List<ConnectionDTO> connectionDtos)
        {
            // Populeer het dictionary om Room IDs te koppelen aan Room objecten

            // Verbind kamers op basis van de connecties
            foreach (ConnectionDTO connectionDto in connectionDtos)
            {
                // If North en Zuid hebben een Id dan Connect de 2 kamers met elkaar
                if (connectionDto.NORTH > 0 && connectionDto.SOUTH > 0)
                {
                    Room northRoom = _roomDict[connectionDto.NORTH];
                    Room southRoom = _roomDict[connectionDto.SOUTH];

                    northRoom.AdjacentRooms[Direction.SOUTH] = southRoom;
                    southRoom.AdjacentRooms[Direction.NORTH] = northRoom;
                }

                if (connectionDto.WEST > 0 && connectionDto.EAST > 0)
                {
                    Room westRoom = _roomDict[connectionDto.WEST];
                    Room eastRoom = _roomDict[connectionDto.EAST];

                    westRoom.AdjacentRooms[Direction.EAST] = eastRoom;
                    eastRoom.AdjacentRooms[Direction.WEST] = westRoom;
                }
            }
        }

        private void AddConnectionsToRooms(List<ConnectionDTO> connectionDtos)
        {
            // Delegate connection creation to RoomFactory
            _roomFactory.CreateRoomConnectionsWithDoors(_roomDict, connectionDtos);
        }
    }
}
