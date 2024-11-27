using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom
{
    public class GameLevel : IGameObject
    {
        public RoomDTO[] Rooms { get; set; }
        public ConnectionDTO[] Connections { get; set; }
        public PlayerDTO Player { get; set; }
    }
}
