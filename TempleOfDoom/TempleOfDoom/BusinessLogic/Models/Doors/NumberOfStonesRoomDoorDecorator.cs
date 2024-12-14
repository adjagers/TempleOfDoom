using TempleOfDoom.DataLayer.Models;

namespace TempleOfDoom.BusinessLogic.Models.Doors
{
    public class NumberOfStonesRoomDoorDecorator : DoorDecorator
    {
        private readonly int _requiredStonesRemaining;

        public NumberOfStonesRoomDoorDecorator(IDoor door, int requiredStonesRemaining) : base(door)
        {
            this._requiredStonesRemaining = requiredStonesRemaining;
        }
        public override void Interact(Player player)
        {
            if (player.CurrentRoom.CountSankraStonesInRoom() == _requiredStonesRemaining) base.OpenDoor();
        }
    }
}