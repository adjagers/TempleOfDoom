namespace TempleOfDoom.BusinessLogic.Models.Doors
{
    public class BasicDoor : IDoor
    {
        private bool _isOpen;

        // Constructor voor initiële staat
        public BasicDoor(bool initialState = false) // Standaard gesloten
        {
            SetInitialState(initialState);
        }

        // Eigenschap om de staat te lezen
        public virtual bool GetState()
        {
            return _isOpen;
        }

        // Methode om de deur te openen
        public virtual void OpenDoor()
        {
            if (!_isOpen)
            {
                _isOpen = true;
                Console.WriteLine("The door is now open.");
            }
            else
            {
                Console.WriteLine("The door is already open.");
            }
        }
        public virtual void CloseDoor()
        {
            if (_isOpen)
            {
                _isOpen = false;
                Console.WriteLine("The door is now closed.");
            }
            else
            {
                Console.WriteLine("The door is already closed.");
            }
        }
        public virtual void SetInitialState(bool isOpen)
        {
            _isOpen = isOpen;
        }
        public void Interact(Player player)
        {
            return;
        }
    }
}