using CODE_TempleOfDoom_DownloadableContent;
using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.BusinessLogic.HelperClasses;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.HelperClasses;

namespace TempleOfDoom.BusinessLogic.Models.Enemy
{
    public class EnemyAdapter : IAutoMovableGameObject
    {
        private readonly CODE_TempleOfDoom_DownloadableContent.Enemy _baseEnemy;
        public int MinX { get; }
        public int MaxX { get; }
        public int MinY { get; }
        public int MaxY { get; }
        public Position Position => new Position(_baseEnemy.CurrentXLocation, _baseEnemy.CurrentYLocation);

        public bool IsDead { get; private set; }
        public int Lives => _baseEnemy.NumberOfLives;
        public const int DamageAmount = 1;
        public Position StartPosition { get; }
        private Direction LastDirection { get; set; }

        // Constructor to initialize the EnemyAdapter with boundary values
        public EnemyAdapter(string type, int x, int y, int minX, int maxX, int minY, int maxY)
        {
            MovableDirection movableDirection = Enum.Parse<MovableDirection>(type, true);
            _baseEnemy = movableDirection switch
            {
                MovableDirection.Horizontal => new HorizontallyMovingEnemy(3, x, y, minX, maxX),
                MovableDirection.Vertical => new VerticallyMovingEnemy(3, x, y, minY, maxY),
                _ => throw new ArgumentException("Unknown type of enemy")
            };

            // Initialize the boundaries from the constructor parameters
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;

            StartPosition = new Position(x, y);

            // Initialize LastDirection based on type
            LastDirection = movableDirection == MovableDirection.Horizontal ? Direction.EAST : Direction.SOUTH;
        }

        // Damage the enemy
        public void Damage(int amount)
        {
            if (_baseEnemy == null)
            {
                Console.WriteLine("Error: _baseEnemy is null.");
                return;
            }

            try
            {
                _baseEnemy.DoDamage(amount);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error during damage processing: {ex.Message}");
            }
        }


        // Move the enemy based on the direction
        public void Move(Direction direction)
        {
            Console.WriteLine("Moving in direction: " + direction);
            Position directionValues = GetDirectionValues(direction);

            _baseEnemy.CurrentXLocation += directionValues.GetX();
            _baseEnemy.CurrentYLocation += directionValues.GetY();
            LastDirection = direction;
        }

        // Automatically move the enemy, checking boundaries and adjusting direction if necessary
        public void AutomaticallyMove()
        {
            if (_baseEnemy == null)
            {
                Console.WriteLine("Error: _baseEnemy is not initialized.");
                return;
            }

            // Check boundaries and reverse direction if necessary
            if ((_baseEnemy.CurrentXLocation <= MinX && LastDirection == Direction.WEST) ||
                (_baseEnemy.CurrentXLocation >= MaxX && LastDirection == Direction.EAST))
            {
                LastDirection = LastDirection == Direction.WEST ? Direction.EAST : Direction.WEST;
            }
            else if ((_baseEnemy.CurrentYLocation <= MinY && LastDirection == Direction.NORTH) ||
                     (_baseEnemy.CurrentYLocation >= MaxY && LastDirection == Direction.SOUTH))
            {
                LastDirection = LastDirection == Direction.NORTH ? Direction.SOUTH : Direction.NORTH;
            }

            // Move in the updated direction
            Move(LastDirection);
        }

        // Check if the enemy should attack the player based on their positions
        public bool ShouldAttackPlayer(Player player)
        {
            return Position.Equals(player.Position);
        }

        // Get the last direction the enemy was moving in
        public Direction GetLastDirection()
        {
            return LastDirection;
        }

        // Helper method to get position change based on direction
        private Position GetDirectionValues(Direction direction)
        {
            return direction switch
            {
                Direction.NORTH => new Position(0, -1), // Move up
                Direction.SOUTH => new Position(0, 1), // Move down
                Direction.EAST => new Position(1, 0), // Move right
                Direction.WEST => new Position(-1, 0), // Move left
                _ => throw new ArgumentException("Unknown direction")
            };
        }
    }
}