using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<Room> AdjacentRooms { get; set; }
        public int CountSankraStonesInRoom()
        {
            return Items.OfType<SankaraStone>().Count();
        }

        public void AddAdjacentRoom(Room room)
        {
            if (!AdjacentRooms.Contains(room))
            {
                AdjacentRooms.Add(room);
            }
        }
    }
}

