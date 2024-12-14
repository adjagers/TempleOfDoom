using System;

namespace TempleOfDoom.BusinessLogic.Models.Doors
{
    public class ToggleDoorDecorator : DoorDecorator, IObserver<bool>
    {
        public ToggleDoorDecorator(IDoor door) : base(door) { }

        public void OnCompleted()
        {
            Console.WriteLine("event handling completed");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"Error: {error}");
        }

        public void OnNext(bool value)
        {
            Toggle();
        }

        public void Toggle()
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