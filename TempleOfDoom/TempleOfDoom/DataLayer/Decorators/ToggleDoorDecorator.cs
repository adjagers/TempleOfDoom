using System;

namespace TempleOfDoom.DataLayer.Decorators
{
    public class ToggleDoorDecorator : DoorDecorator
    {
        public ToggleDoorDecorator(IDoor door) : base(door) { }

        public void Trigger()
        {
            if (_door.IsOpen)
            {
                base.Close();
                Console.WriteLine("The door was toggled and is now closed.");
            }
            else
            {
                base.Open();
                Console.WriteLine("The door was toggled and is now open.");
            }
        }
    }
}
