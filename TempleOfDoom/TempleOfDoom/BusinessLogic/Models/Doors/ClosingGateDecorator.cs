using System;

namespace TempleOfDoom.BusinessLogic.Models.Doors
{
    public class ClosingGateDecorator : DoorDecorator
    {
        private bool _hasClosed;

        // Constructor
        public ClosingGateDecorator(IDoor door) : base(door)
        {
            _hasClosed = false;
        }

        public override void CloseDoor()
        {
            if (!_hasClosed)
            {
                base.CloseDoor();
                _hasClosed = true; // Mark as permanently closed
                Console.WriteLine("The gate has permanently closed.");
            }
            else
            {
                Console.WriteLine("The gate is already permanently closed.");
            }
        }

        public override void OpenDoor()
        {
            if (_hasClosed)
            {
                Console.WriteLine("The gate is permanently closed and cannot be reopened.");
            }
            else
            {
                base.OpenDoor();
            }
        }

        public override bool GetState()
        {
            return base.GetState();
        }

    }
}