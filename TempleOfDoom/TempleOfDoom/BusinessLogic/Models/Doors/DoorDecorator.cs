using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.DataLayer.Models;

namespace TempleOfDoom.BusinessLogic.Models.Doors
{
    public class DoorDecorator(IDoor door) : IDoor
    {
        protected readonly IDoor _door = door ?? throw new ArgumentNullException(nameof(door));

        // Eigenschap die de status doorgeeft van de originele deur
        public virtual void OpenDoor()
        {
            Console.WriteLine("Decorator: Adding functionality before opening the door.");
            _door.OpenDoor(); // Roep de functionaliteit van de oorspronkelijke deur aan
            Console.WriteLine("Decorator: Additional functionality after opening the door.");
        }

        public virtual void CloseDoor()
        {
            Console.WriteLine("Decorator: Adding functionality before closing the door.");
            _door.CloseDoor(); // Roep de functionaliteit van de oorspronkelijke deur aan
            Console.WriteLine("Decorator: Additional functionality after closing the door.");
        }

        public virtual void SetInitialState(bool isOpen)
        {
            _door.SetInitialState(isOpen);
        }

        public virtual bool GetState()
        {
            return _door.GetState();
        }

        public virtual void Interact(Player player)
        {
            _door.Interact(player);
        }
    }
}