using System;

namespace TempleOfDoom.DataLayer.Decorators
{
    public class ClosingGateDecorator : DoorDecorator
    {
        private bool _hasClosed;

        // Constructor
        public ClosingGateDecorator(IDoor door) : base(door)
        {
            _hasClosed = false;
        }

        public override void Close()
        {
            if (!_hasClosed)
            {
                base.Close();
                _hasClosed = true; // Mark as permanently closed
                Console.WriteLine("The gate has permanently closed.");
            }
            else
            {
                Console.WriteLine("The gate is already permanently closed.");
            }
        }

        public override void Open()
        {
            if (_hasClosed)
            {
                Console.WriteLine("The gate is permanently closed and cannot be reopened.");
            }
            else
            {
                base.Open();
            }
        }

        public override bool IsOpen
        {
            get
            {
                // If permanently closed, always return false
                return !_hasClosed && base.IsOpen;
            }
            set
            {
                // Prevent modifications if permanently closed
                if (_hasClosed)
                {
                    Console.WriteLine("The gate is permanently closed. IsOpen cannot be modified.");
                }
                else
                {
                    base.IsOpen = value;
                }
            }
        }
    }
}