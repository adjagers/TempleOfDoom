using System;

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
                base.Open();
                Console.WriteLine($"The {_color} door is now open.");
            }
            else
            {
                Console.WriteLine($"The {_color} door remains closed. You need a {_color} key.");
            }
        }

        private bool PlayerHasMatchingKey()
        {
            // Simuleer de sleutelcontrole. Vervang dit met je eigen logica.
            // Bijvoorbeeld: return Player.Keys.Contains(_color);
            return true; // Voor testdoeleinden
        }
    }
}
