using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.DTO
{
   public class DoorDTO : IDTO
    {
        public string Type { get; set; }
        public string Color { get; set; }
        public int No_of_stones { get; set; }
    }
}
