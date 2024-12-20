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
    }
}

