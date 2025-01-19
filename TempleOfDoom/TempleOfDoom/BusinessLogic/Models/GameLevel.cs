using TempleOfDoom.DataLayer.Models;

namespace TempleOfDoom.BusinessLogic.Models
{
    public class GameLevel : IGameObject
    {
        public List<Room> Rooms { get; }
        public Player Player { get; }


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
