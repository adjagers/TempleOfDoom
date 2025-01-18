using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.DataLayer.Models.Items;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom
{
    public class GameLevel : IGameObject
    {
        public List<Room> Rooms { get; set; }
        public Player Player { get; set; }


        public GameLevel(List<Room> rooms, Player player)
        {
            Rooms = rooms;
            Player = player;
        }

        public int GetTotalSankaraStones()
        {
            return Rooms.Sum(room => room.CountSankraStonesInRoom());
        }

        public bool HasPlayerCollectedRequiredStones(int requiredStoneCount)
        {
            int playerCollectedStones = Player.Inventory.GetSankaraStonesCount();
            return playerCollectedStones >= requiredStoneCount;
        }
        
    }
}
