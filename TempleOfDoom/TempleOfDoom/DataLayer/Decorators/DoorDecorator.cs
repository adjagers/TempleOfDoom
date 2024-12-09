using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.DataLayer.Models;

namespace TempleOfDoom.DataLayer.Decorators
{
    public class DoorDecorator : IDoor
    {
        protected readonly IDoor _door;

        public DoorDecorator(IDoor door)
        {
                _door = door ?? throw new ArgumentNullException(nameof(door));
        }
        // Eigenschap die de status doorgeeft van de originele deur
        public virtual bool IsOpen
        {
            get => _door.IsOpen;
            set => _door.IsOpen = value;
        }

        public virtual void Open()
        {
            Console.WriteLine("Decorator: Adding functionality before opening the door.");
            _door.Open(); // Roep de functionaliteit van de oorspronkelijke deur aan
            Console.WriteLine("Decorator: Additional functionality after opening the door.");
        }

        public virtual void Close()
        {
            Console.WriteLine("Decorator: Adding functionality before closing the door.");
            _door.Close(); // Roep de functionaliteit van de oorspronkelijke deur aan
            Console.WriteLine("Decorator: Additional functionality after closing the door.");
        }
    }
}
