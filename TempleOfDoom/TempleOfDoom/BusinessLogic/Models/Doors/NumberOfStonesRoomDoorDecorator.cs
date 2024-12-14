using TempleOfDoom.DataLayer.Models;

namespace TempleOfDoom.BusinessLogic.Models.Doors
{
    public class NumberOfStonesRoomDoorDecorator : DoorDecorator
    {
        private readonly int _requiredStones;
        private int no_of_stones;

        public NumberOfStonesRoomDoorDecorator(IDoor door, int no_of_stones) : base(door)
        {
            this.no_of_stones = no_of_stones;
        }
        public override void Interact(Player player)
        {

        }
    }
}