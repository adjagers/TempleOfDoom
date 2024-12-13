using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.DTO
{
    public class ItemDTO : IDTO
    {
        public string Type { get; set; }
        public int? Damage { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Color { get; set; }
    }
}
