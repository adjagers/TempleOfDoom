using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DTO
{
    public class ConnectionDTO: IDTO
    {
        public int NORTH { get; set; }
        public int SOUTH { get; set; }
        public DoorDTO[] doors { get; set; }
        public int WEST { get; set; }
        public int EAST { get; set; }
    }
}
