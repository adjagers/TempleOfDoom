using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.Models
{
    public class Room : IGameObject
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public Dimensions Dimensions { get; set; }
        public List<IItem> Items { get; set; }
    }
}

