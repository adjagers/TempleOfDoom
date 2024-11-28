using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TempleOfDoom.Interfaces;
namespace TempleOfDoom.DataLayer.DTO
{
    public class GameLevelDTO : IDTO
    {
        public List<RoomDTO> Rooms { get; set; }
        public List<ConnectionDTO> Connections { get; set; }
        public PlayerDTO Player { get; set; }
    }
}
