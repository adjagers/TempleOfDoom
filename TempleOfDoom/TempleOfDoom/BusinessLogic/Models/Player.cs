using System;
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
        
        public Inventory Inventory { get;}

        public void AddItemInventory(IItem item)
        {
           Inventory.AddItem(item);
        }
        public bool NumberOfLivesIsOdd()
        {
            if(Lives%2==1) return true;
            return false;
        }
    }
}
