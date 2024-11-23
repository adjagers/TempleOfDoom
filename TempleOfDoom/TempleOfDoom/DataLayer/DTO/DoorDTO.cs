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
        public string type { get; set; }
        public string color { get; set; }
        public int no_of_stones { get; set; }
    }
}
