using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom
{
    public class GameLevel : IGameObject
    {
        public Room[] Rooms { get; set; }
        public Connection[] Connections { get; set; }
        public Player Player { get; set; }
    }
}
