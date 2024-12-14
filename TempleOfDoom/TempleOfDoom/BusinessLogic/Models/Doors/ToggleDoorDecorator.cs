using System;

namespace TempleOfDoom.BusinessLogic.Models.Doors
{
    public class ToggleDoorDecorator : DoorDecorator
    {
        public ToggleDoorDecorator(IDoor door) : base(door) { }

        public void Trigger()
        {
            if (_door.GetState())
            {
                base.CloseDoor();
                Console.WriteLine("The door was toggled and is now closed.");
            }
            else
            {
                base.OpenDoor();
                Console.WriteLine("The door was toggled and is now open.");
            }
        }
    }
}
