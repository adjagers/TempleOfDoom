using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.DTO
{
    public record PlayerDTO(
        int StartRoomId,
        int StartX,
        int StartY,
        int Lives) : IDTO;
}
