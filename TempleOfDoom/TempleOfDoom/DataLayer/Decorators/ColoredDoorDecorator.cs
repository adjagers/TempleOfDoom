using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Enums;

namespace TempleOfDoom.DataLayer.Decorators
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