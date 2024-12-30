using System;
using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models.Items;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.Models
{
    public class Player : IGameObject
    {
        public Room CurrentRoom { get; set; }
        public Position Position { get; set; }
        public int Lives { get; set; }

        public Inventory Inventory { get; }
        public Player()
        {
            this.Inventory = new Inventory();
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

        public void MoveTo(Position newPosition)
        {
            Position = newPosition;
        }
        
        public bool IsPlayerPosition(int x, int y)
        {
            return Position.GetX() == x && Position.GetY() == y;
        }
        
        public void MoveThroughDoor(Room nextRoom, Direction direction)
        {
            CurrentRoom = nextRoom;

            switch (direction)
            {
                case Direction.NORTH:
                    Position = new Position(nextRoom.Dimensions.getWidth() / 2, nextRoom.Dimensions.getHeight() - 2);
                    break;
                case Direction.SOUTH:
                    Position = new Position(nextRoom.Dimensions.getWidth() / 2, 1);
                    break;
                case Direction.WEST:
                    Position = new Position(nextRoom.Dimensions.getWidth() - 2, nextRoom.Dimensions.getHeight() / 2);
                    break;
                case Direction.EAST:
                    Position = new Position(1, nextRoom.Dimensions.getHeight() / 2);
                    break;
            }
        }
    }
}
