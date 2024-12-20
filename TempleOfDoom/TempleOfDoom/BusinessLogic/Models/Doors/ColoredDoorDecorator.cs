using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.DataLayer.Models;

namespace TempleOfDoom.BusinessLogic.Models.Doors
{
    public class ColoredDoorDecorator : DoorDecorator
    {
        private readonly Color _color;
        public ColoredDoorDecorator(IDoor door, Color color) : base(door)
        {
            _color = color;
        }
        public override void Interact(Player player)
        {
            if (player.Inventory.HasKey(_color)) base.OpenDoor();
        }
    }
}