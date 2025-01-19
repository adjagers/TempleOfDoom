using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.BusinessLogic.HelperClasses;
using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models.Items;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.Models
{
    public class Room : IGameObject
    {
        public Dimensions Dimensions { get; set; }
        public int LeftX() => 0;
        public int RightX() => Dimensions.getWidth() - 1;
        public int TopY() => Dimensions.getHeight() - 1;
        public int BottomY() => 0;
        public List<Connection> Connections { get; set; } = new List<Connection>();
        public List<IItem> Items { get; set; }
        public Dictionary<Direction, Room> AdjacentRooms { get; set; } = new();
        public List<IAutoMovableGameObject> Enemies { get; set; } = new List<IAutoMovableGameObject>();
        public int CountSankraStonesInRoom()
        {
            return Items.OfType<SankaraStone>().Count();
        }

        public bool HasMovableGameObject(int x, int y, out IMovableGameObject gameObject)
        {
            gameObject = Enemies.FirstOrDefault(obj => obj.Position.GetX() == x && obj.Position.GetY() == y);
            return gameObject != null;
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
                AdjacentRooms.TryGetValue(direction, out var adjacentRoom) &&
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
            int x = playerPosition.GetX();
            int y = playerPosition.GetY();
    
            if (IsDoor(x, y, this))
            {
                if (y == 0 && x == Dimensions.getWidth() / 2 && AdjacentRooms.ContainsKey(Direction.NORTH)) return Direction.NORTH;
                if (y == Dimensions.getHeight() - 1 && x == Dimensions.getWidth() / 2 && AdjacentRooms.ContainsKey(Direction.SOUTH)) return Direction.SOUTH;
                if (x == 0 && y == Dimensions.getHeight() / 2 && AdjacentRooms.ContainsKey(Direction.WEST)) return Direction.WEST;
                if (x == Dimensions.getWidth() - 1 && y == Dimensions.getHeight() / 2 && AdjacentRooms.ContainsKey(Direction.EAST)) return Direction.EAST;
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
                if(player.Position.GetX()==item.Position.GetX()&&player.Position.GetY()==item.Position.GetY())
                {
                    item.Interact(player);
                }
            }
        }
    }
}