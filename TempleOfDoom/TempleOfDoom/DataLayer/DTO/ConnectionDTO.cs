using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.DTO
{
    public class ConnectionDTO: IDTO
    {
        public int NORTH { get; set; }
        public int SOUTH { get; set; }
        public List<DoorDTO> Doors { get; set; } = new List<DoorDTO>();
        public LadderDTO Ladder { get; set; }
        public int WEST { get; set; }
        public int EAST { get; set; }
        public int? UPPER { get; set; }
        public int? LOWER { get; set; }
    }
}
