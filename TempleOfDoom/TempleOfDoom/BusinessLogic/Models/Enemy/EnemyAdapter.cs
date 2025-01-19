using CODE_TempleOfDoom_DownloadableContent;

namespace TempleOfDoom.BusinessLogic.Models.Enemy
{
    public class EnemyAdapter : IAutoMovableGameObject
    {
        private readonly CODE_TempleOfDoom_DownloadableContent.Enemy _baseEnemy;
        private int MinX { get; }
        private int MaxX { get; }
        private int MinY { get; }
        private int MaxY { get; }
        public Position Position => new Position(_baseEnemy.CurrentXLocation, _baseEnemy.CurrentYLocation);

        public bool IsDead { get; }
        
        public int Lives => _baseEnemy.NumberOfLives;
        public Position StartPosition;
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
            
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
            StartPosition = new Position(x, y);

            LastDirection = GetLastDirection(movableDirection);
        }
        public void Damage(int amount)
        {
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
            Position directionValues = DirectionExtension.GetDirectionValues(direction);

            _baseEnemy.CurrentXLocation += directionValues.GetX();
            _baseEnemy.CurrentYLocation += directionValues.GetY();
            LastDirection = direction;
        }

        public Direction GetLastDirection(MovableDirection movableDirection)
        {
            return movableDirection == MovableDirection.Horizontal ? Direction.EAST : Direction.SOUTH;
        }

        // Automatically move the enemy, checking boundaries and adjusting direction if necessary
        public void AutomaticallyMove()
        {
            if ((_baseEnemy.CurrentXLocation <= MinX && LastDirection == Direction.WEST) ||
                (_baseEnemy.CurrentXLocation >= MaxX && LastDirection == Direction.EAST) ||
                (_baseEnemy.CurrentYLocation <= MinY && LastDirection == Direction.NORTH) ||
                (_baseEnemy.CurrentYLocation >= MaxY && LastDirection == Direction.SOUTH))
            {
                LastDirection = LastDirection.Opposite();
            }
            Move(LastDirection);
        }
        
    }
}