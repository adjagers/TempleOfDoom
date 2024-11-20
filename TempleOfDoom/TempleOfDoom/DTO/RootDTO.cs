using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DTO
{
    public class RootDTO : IDTO
    {
        public RoomDTO[] rooms { get; set; }
        public ConnectionDTO[] connections { get; set; }
        public PlayerDTO player { get; set; }
    }
}
