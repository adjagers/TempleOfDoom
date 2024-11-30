using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.DTO
{
    public class PlayerDTO : IDTO
    {
        public int StartRoomId { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int Lives { get; set; }
    }

}
