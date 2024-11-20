using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.DTO
{
    public class RootDTO
    {
        public RoomDTO[] rooms { get; set; }
        public ConnectionDTO[] connections { get; set; }
        public PlayerDTO player { get; set; }
    }
}
