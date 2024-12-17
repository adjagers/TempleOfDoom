using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.BusinessLogic.FactoryMethodes
{
    public class GameLevelFactory : IFactory
    {
        private RoomFactory _roomFactory;
        private ConnectionFactory _connectionFactory;

        List<Connection> _connections;

        public GameLevelFactory(IDTO dto) {

            _roomFactory = new RoomFactory();
            _connectionFactory = new ConnectionFactory();

            _connections = (List<Connection>?)_connectionFactory.Create(dto);

        }
        public IGameObject Create(IDTO dto)
        {
            if (dto == null) return null;

            GameLevelDTO gameLevelDTO = dto as GameLevelDTO;

            GameLevel gameLevel = new GameLevel
            {
               Player = CreatePlayer((PlayerDTO)dto),
                
            };





            return gameLevel;
        }

        public Player CreatePlayer(PlayerDTO playerDTO)
        {
            return new Player
            {
                CurrentRoom = new Room(),
                Lives = playerDTO.Lives,
                Position = new Position(playerDTO.StartX, playerDTO.StartY)
            };
        }

        private void LinkRoomsWithConnections(List<RoomDTO> roomDTOs, List<Room> rooms)
        {
            // Maak een dictionary van roomDTO's op basis van RoomDTO's Id's
            Dictionary<int, RoomDTO> roomDTODictionary = roomDTOs.ToDictionary(dto => dto.Id);

            // Maak een dictionary van rooms, waarbij de rooms worden gekoppeld aan hun RoomDTO via de Id.
            Dictionary<int, Room> roomDictionary = rooms.ToDictionary(r => r.Id);

            foreach (Connection connection in _connections)
            {
                // Koppel kamers via de verbindingen door te zoeken op RoomId (nu gebruikmakend van RoomDTO.Id)
                LinkRoomsByDirection(connection.NORTH, connection.SOUTH, roomDictionary);
                LinkRoomsByDirection(connection.SOUTH, connection.NORTH, roomDictionary);
                LinkRoomsByDirection(connection.EAST, connection.WEST, roomDictionary);
                LinkRoomsByDirection(connection.WEST, connection.EAST, roomDictionary);
            }
        }

        private void LinkRoomsByDirection(int fromRoomId, int toRoomId, Dictionary<int, Room> roomDictionary)
        {
            // Zorg ervoor dat de kamers in het dictionary bestaan
            if (roomDictionary.ContainsKey(fromRoomId) && roomDictionary.ContainsKey(toRoomId))
            {
                Room fromRoom = roomDictionary[fromRoomId];
                Room toRoom = roomDictionary[toRoomId];

                fromRoom.AddAdjacentRoom(toRoom);
                toRoom.AddAdjacentRoom(fromRoom);  // Voeg de kamer toe aan de lijst van aangrenzende kamers
            }
        }



    }


}
}
