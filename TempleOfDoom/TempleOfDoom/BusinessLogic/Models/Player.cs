using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.BusinessLogic.Models
{
    public class Player : IMovableGameObject, IObservable<Player>
    {
        private List<IObserver<Player>> _observers = new();
        
        public Room CurrentRoom { get; set; }
        public bool PerformedAction { get; private set; }
        public int SankaraStones => Inventory.GetSankaraStonesCount();
        public Position Position { get; set; }
        public int Lives { get; set; }
        public bool IsDead => Lives <= 0;
        public bool IsDone => IsDead;

        private const int PlayerDamage = 1;
        
        
        public Player()
        {
            this.Inventory = new Inventory();
        }


        public Inventory Inventory { get; }

        public void Damage(int amount)
        {
            Lives = Math.Max(Lives - amount, 0);
            NotifyObservers();
        }

        public void Attack(IMovableGameObject enemy)
        {
            PerformedAction = true;
            enemy.Damage(PlayerDamage);
            NotifyObservers();
            PerformedAction = false;
        }

        public void Move(Direction direction)
        {
            // Get the direction offset
            Position directionValues = direction.GetDirectionValues();

            // Calculate the new position
            int newX = Position.GetX() + directionValues.GetX();
            int newY = Position.GetY() + directionValues.GetY();

            // Check if the new position is a wall or not
            if (CurrentRoom.IsWall(newX, newY, CurrentRoom) &&
                !CurrentRoom.IsDoor(newX, newY, CurrentRoom))
            {
                Console.WriteLine("Cannot move, it's a wall!");
                return;
            }

            // Update the player's position
            Position = new Position(newX, newY);

            // Notify observers about the player's movement
            NotifyObservers();
        }

        public Direction GetLastDirection()
        {
            throw new NotImplementedException();
        }



        public IDisposable Subscribe(IObserver<Player> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber<Player>(_observers, observer);
        }

        public bool GameOverCheck()
        {
            if (Lives < 1)
            {
                return true;
            }
            else
            {
                return false;
            }
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
                case Direction.UPPER: // Handle moving up a ladder
                    Position = new Position(nextRoom.Dimensions.getWidth() / 2,
                        nextRoom.Dimensions.getHeight() / 2 - 1); // Ladder top position
                    break;

                case Direction.LOWER: // Handle moving down a ladder
                    Position = new Position(nextRoom.Dimensions.getWidth() / 2,
                        nextRoom.Dimensions.getHeight() / 2 + 1); // Ladder bottom position
                    break;
            }

            NotifyObservers();
        }
    }
}
