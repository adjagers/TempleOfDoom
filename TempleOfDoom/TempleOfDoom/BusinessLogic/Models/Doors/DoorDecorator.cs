namespace TempleOfDoom.BusinessLogic.Models.Doors
{
    public class DoorDecorator : IDoor
    {
        protected readonly IDoor _door;

        // Constructor
        public DoorDecorator(IDoor door)
        {
            _door = door ?? throw new ArgumentNullException(nameof(door));
        }

        // DecoratedDoor property to access the wrapped door
        public IDoor DecoratedDoor => _door;

        public virtual void OpenDoor()
        {
            Console.WriteLine("Decorator: Adding functionality before opening the door.");
            _door.OpenDoor(); // Call the original door's functionality
            Console.WriteLine("Decorator: Additional functionality after opening the door.");
        }

        public virtual void CloseDoor()
        {
            Console.WriteLine("Decorator: Adding functionality before closing the door.");
            _door.CloseDoor(); // Call the original door's functionality
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