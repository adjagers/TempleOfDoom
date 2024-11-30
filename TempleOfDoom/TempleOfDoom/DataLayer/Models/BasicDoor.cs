using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.Models
{
    public class BasicDoor : IDoor
    {
        private bool _isOpen;

        public BasicDoor(bool initialState)
        {
            _isOpen = initialState;
        }

        public bool IsOpen()
        {
            return _isOpen;
        }

        public void SetInitialState(bool state)
        {
            _isOpen = state;
        }

        public void Open()
        {
            _isOpen = true;
        }

        public void Close()
        {
            _isOpen = false;
        }
    }
}
