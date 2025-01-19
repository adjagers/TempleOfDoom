using System;
using System.Collections.Generic;
using System.Linq;
using CODE_TempleOfDoom_DownloadableContent;
using TempleOfDoom.BusinessLogic;
using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.FactoryMethodes
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

            Room startRoom = _roomDict.ContainsKey(gameLevelDTO.Player.StartRoomId)
                ? _roomDict[gameLevelDTO.Player.StartRoomId]
                : throw new InvalidOperationException(
                    $"Room with ID {gameLevelDTO.Player.StartRoomId} does not exist.");

            Player player = new Player(
                gameLevelDTO.Player.Lives,
                new Position(gameLevelDTO.Player.StartX, gameLevelDTO.Player.StartY),
                startRoom
            );


            GameLevel gameLevel = new GameLevel(rooms, player);
            return gameLevel;
        }

        private void CreateRooms(List<RoomDTO> roomDtos)
        {
            foreach (RoomDTO roomDto in roomDtos)
            {
                _roomDict[roomDto.Id] = (Room)_roomFactory.Create(roomDto);
            }
        }



        private void AddConnectionsToRooms(List<ConnectionDTO> connectionDtos)
        {
            // Delegate connection creation to RoomFactory
            _roomFactory.CreateRoomConnectionsWithTransitions(_roomDict, connectionDtos);
        }
    }
}
