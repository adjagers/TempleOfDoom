using System;
using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models.Items;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.Models
{
    public class Player : IGameObject, IObservable<Player>
    {
        private List<IObserver<Player>> _observers = new();
        
        public Room CurrentRoom { get; set; }
        public Position Position { get; set; }
        public int Lives { get; set; }

        public Inventory Inventory { get; }
        public Player()
        {
            this.Inventory = new Inventory();
        }

        public IDisposable Subscribe(IObserver<Player> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber<Player>(_observers, observer);
        }

        public void MoveTo(Position newPosition)
        {
            // Notify observers about the player movement
            Position = newPosition;
            NotifyObservers();
        }

        private void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(this);
            }
        }

        public class Unsubscriber<T> : IDisposable
        {
            private List<IObserver<T>> _observers;
            private IObserver<T> _observer;

            public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }
            }
        }
        public void AddItemInventory(IItem item)
        {
            Inventory.AddItem(item);
        }
        public bool NumberOfLivesIsOdd()
        {
            if (Lives % 2 == 1) return true;
            return false;
        }

       
        
        public bool IsPlayerPosition(int x, int y)
        {
            return Position.GetX() == x && Position.GetY() == y;
        }
        public void MoveThroughDoor(Room nextRoom, Direction direction)
        {
            CurrentRoom = nextRoom; // Update current room to the next room

            // Position the player just inside the door
            switch (direction)
            {
                case Direction.NORTH:
                    Position = new Position(nextRoom.Dimensions.getWidth() / 2,
                        nextRoom.Dimensions.getHeight() - 2); // Just inside
                    break;

                case Direction.SOUTH:
                    Position = new Position(nextRoom.Dimensions.getWidth() / 2, 1); // Just inside
                    break;

                case Direction.WEST:
                    Position = new Position(nextRoom.Dimensions.getWidth() - 2,
                        nextRoom.Dimensions.getHeight() / 2); // Just inside
                    break;

                case Direction.EAST:
                    Position = new Position(1, nextRoom.Dimensions.getHeight() / 2); // Just inside
                    break;
            }

            NotifyObservers();
        }

    }
}
