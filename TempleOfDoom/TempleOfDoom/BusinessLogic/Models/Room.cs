using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models.Items;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.Models
{
    public class Room : IGameObject
    {
        public string Type { get; set; }
        public Dimensions Dimensions { get; set; }
        public List<IItem> Items { get; set; }

        public Dictionary<Direction, Room> AdjacentRooms { get; set; } = new();
        public int CountSankraStonesInRoom()
        {
            return Items.OfType<SankaraStone>().Count();
        }
        
        public bool IsWallOrDoor(int x, int y, Room currentRoom)
        {
            return x == 0 || x == currentRoom.Dimensions.getWidth() - 1 ||
                   y == 0 || y == currentRoom.Dimensions.getHeight() - 1;
        }
        
        public bool IsDoor(int x, int y, Room currentRoom)
        {
            return (y == 0 && x == currentRoom.Dimensions.getWidth() / 2 && currentRoom.AdjacentRooms.ContainsKey(Direction.NORTH)) ||
                   (y == currentRoom.Dimensions.getHeight() - 1 && x == currentRoom.Dimensions.getWidth() / 2 && currentRoom.AdjacentRooms.ContainsKey(Direction.SOUTH)) ||
                   (x == 0 && y == currentRoom.Dimensions.getHeight() / 2 && currentRoom.AdjacentRooms.ContainsKey(Direction.WEST)) ||
                   (x == currentRoom.Dimensions.getWidth() - 1 && y == currentRoom.Dimensions.getHeight() / 2 && currentRoom.AdjacentRooms.ContainsKey(Direction.EAST));
        }
        
        public bool IsPlayerOnDoor(Position playerPosition)
        {
            int x = playerPosition.GetX();
            int y = playerPosition.GetY();
            return IsDoor(x, y, this); 
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