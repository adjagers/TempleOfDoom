using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.DataLayer.Models;

namespace TempleOfDoom.BusinessLogic.Models.Doors
{
    public class ColoredDoorDecorator(IDoor door, Color color) : DoorDecorator(door)
    {
        public readonly Color _color = color;

        public override void Interact(Player player)
        {
            if (player.Inventory.HasKey(_color)) base.OpenDoor();
        }
    }
}