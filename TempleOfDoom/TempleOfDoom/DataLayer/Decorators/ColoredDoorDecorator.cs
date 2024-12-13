using TempleOfDoom.DataLayer.Models;

namespace TempleOfDoom.DataLayer.Decorators
{
    public class ColoredDoorDecorator : DoorDecorator
    {
        private readonly string _color;

        public ColoredDoorDecorator(IDoor door, string color) : base(door)
        {
            _color = color ?? throw new ArgumentNullException(nameof(color));
        }

        public override void Open()
        {
            if (PlayerHasMatchingKey())
            {
                base.Open(); // Allow the base door to open if the key matches
                Console.WriteLine($"The {_color} door is now open.");
            }
            else
            {
                Console.WriteLine($"The {_color} door remains closed. You need a {_color} key.");
            }
        }
        
        private bool PlayerHasMatchingKey()
        {
            // For demonstration purposes, let's just assume the player doesn't have the key
            bool hasKey = false;
            if (hasKey)
            {
                return true; // Player has the key
            }
            else
            {
                Console.WriteLine($"The {_color} door does not have a matching key.");
                return false; // Player doesn't have the key
            }
        }
    }
}