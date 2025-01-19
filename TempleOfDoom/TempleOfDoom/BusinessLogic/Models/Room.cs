using TempleOfDoom.DataLayer.Models.Items;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.BusinessLogic.Models
{
    public class Room : IGameObject
    {
        public Dimensions Dimensions { get; set; }
        public int LeftX() => 0;
        public int RightX() => Dimensions.getWidth() - 1;
        public int TopY() => Dimensions.getHeight() - 1;
        public int BottomY() => 0;
        public List<Connection> Connections { get; } = new();
        public List<IItem> Items { get; }
        public Dictionary<Direction, Room> AdjacentRooms { get; } = new();
        public List<IAutoMovableGameObject> Enemies { get; }
        public int CountSankraStonesInRoom()
        {
            return Items.OfType<SankaraStone>().Count();
        }

        public Room(Dimensions dimensions, List<IItem> items, List<IAutoMovableGameObject> enemies)
        {
            Dimensions = dimensions;
            Items = items;
            Enemies = enemies;
        }
        public bool IsDoor(int x, int y, Room currentRoom)
        {
            return (y == 0 && x == currentRoom.Dimensions.getWidth() / 2 && currentRoom.AdjacentRooms.ContainsKey(Direction.NORTH)) ||
                   (y == currentRoom.Dimensions.getHeight() - 1 && x == currentRoom.Dimensions.getWidth() / 2 && currentRoom.AdjacentRooms.ContainsKey(Direction.SOUTH)) ||
                   (x == 0 && y == currentRoom.Dimensions.getHeight() / 2 && currentRoom.AdjacentRooms.ContainsKey(Direction.WEST)) ||
                   (x == currentRoom.Dimensions.getWidth() - 1 && y == currentRoom.Dimensions.getHeight() / 2 && currentRoom.AdjacentRooms.ContainsKey(Direction.EAST));
        }
        public Connection? GetConnectionByDirection(Direction direction)
        {
            return Connections.FirstOrDefault(conn =>
                AdjacentRooms.TryGetValue(direction, out Room adjacentRoom) &&
                conn.ConnectedRoom == adjacentRoom);
        }
        public bool IsPlayerOnDoor(Position playerPosition)
        {
            int x = playerPosition.GetX();
            int y = playerPosition.GetY();
            return IsDoor(x, y, this); 
        }
        public bool IsWall(int x, int y, Room currentRoom)
        {
            // Check if the position is at any of the room boundaries (walls)
            return x == 0 || x == currentRoom.Dimensions.getWidth() - 1 ||
                   y == 0 || y == currentRoom.Dimensions.getHeight() - 1;
        }
        public Direction? GetDoorDirection(Position playerPosition)
        {
            // Get the width and height of the room or map (assuming this is part of your object)
            int width = Dimensions.getWidth();
            int height = Dimensions.getHeight();

            // Using the extension to get the x and y offsets for each direction
            Position doorPosition = new Position(playerPosition.GetX(), playerPosition.GetY());

            if (IsDoor(doorPosition.GetX(), doorPosition.GetY(), this))
            {
                // Try to find matching direction dynamically using the Direction extension
                foreach (var direction in Enum.GetValues(typeof(Direction)).Cast<Direction>())
                {
                    // Check if adjacent room exists in that direction
                    if (AdjacentRooms.ContainsKey(direction))
                    {
                        // Get the corresponding door position for that direction using the extension method
                        Position expectedPosition = direction.GetPositionForDirection(width, height);

                        // Compare door position with expected position dynamically
                        if (doorPosition.Equals(expectedPosition))
                        {
                            return direction;
                        }
                    }
                }
            }
            return null;
        }
        public void AddConnection(Connection connection)
        {
            if (connection != null)
            {
                Connections.Add(connection);
            }
        }
        public void ItemCheck(Player player)
        {
            foreach(IItem item in Items)
            {
                if(item.Position==null) continue;
                if (player.Position.Equals(item.Position))
                {
                    item.Interact(player);
                }
            }
        }
        public Position GetPositionForDoor(Direction direction)
        {
            return direction switch
            {
                Direction.NORTH => new Position(Dimensions.getWidth() / 2, Dimensions.getHeight() - 2),
                Direction.SOUTH => new Position(Dimensions.getWidth() / 2, 1),
                Direction.WEST => new Position(Dimensions.getWidth() - 2, Dimensions.getHeight() / 2),
                Direction.EAST => new Position(1, Dimensions.getHeight() / 2),
                Direction.UPPER => new Position(Dimensions.getWidth() / 2, Dimensions.getHeight() / 2 - 1),
                Direction.LOWER => new Position(Dimensions.getWidth() / 2, Dimensions.getHeight() / 2 + 1),
                _ => throw new ArgumentException("Invalid direction", nameof(direction))
            };
        }
    }
}