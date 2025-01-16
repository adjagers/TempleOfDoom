using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom
{
    public class GameLevel : IGameObject
    {
        public List<Room> Rooms { get; set; }
        public Player Player { get; set; }
    }
    
    
}
