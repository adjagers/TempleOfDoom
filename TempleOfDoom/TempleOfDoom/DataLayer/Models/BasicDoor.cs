using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.Models
{
    public class BasicDoor : IDoor
    {
        private bool _isOpen;

        // Constructor voor initiële staat
        public BasicDoor(bool initialState = false) // Standaard gesloten
        {
            _isOpen = initialState;
        }

        // Eigenschap om de staat te lezen
        public bool IsOpen
        {
            get => _isOpen;
            set => _isOpen = value;
        }

        // Methode om de deur te openen
        public virtual void Open()
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

        // Methode om de deur te sluiten
        public virtual void Close()
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
    }
}
