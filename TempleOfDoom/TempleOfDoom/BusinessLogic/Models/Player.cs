using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.BusinessLogic.Models
{
    public class Player(int lives, Position position, Room currentRoom) : IMovableGameObject, IObservable<Player>
    {
        private readonly List<IObserver<Player>> _observers = new();

        public Room CurrentRoom { get; private set; } =
            currentRoom ?? throw new ArgumentNullException(nameof(currentRoom));
        public bool PerformedAction { get; private set; }
        public int SankaraStones => Inventory.GetSankaraStonesCount();
        public Position Position { get; private set; } = position;
        public int Lives { get; set; } = lives;
        public bool IsDead => Lives <= 0;
        public bool IsDone => IsDead;

        private const int PlayerDamage = 1;


        public Inventory Inventory { get; } = new();

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

        private bool IsBlocked(int x, int y) =>
            CurrentRoom.IsWall(x, y, CurrentRoom) && !CurrentRoom.IsDoor(x, y, CurrentRoom);

        public void Move(Direction direction)
        {
            Position directionValues = direction.GetDirectionValues();

            // Use the Add method of the Position struct to calculate the new position
            Position newPosition = Position.Add(directionValues);

            if (IsBlocked(newPosition.GetX(), newPosition.GetY()))
            {
                return;
            }

            Position = newPosition; // Update the player's position with the new calculated position
            NotifyObservers();
        }


        public Direction GetLastDirection(MovableDirection movableDirection)
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
            foreach (IObserver<Player> observer in _observers)
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
                _observers = observers;
                _observer = observer;
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
        
        public void MoveThroughDoor(Room nextRoom, Direction direction)
        {
            CurrentRoom = nextRoom;
            Position = nextRoom.GetPositionForDoor(direction);
            NotifyObservers();
        }
    }
}
