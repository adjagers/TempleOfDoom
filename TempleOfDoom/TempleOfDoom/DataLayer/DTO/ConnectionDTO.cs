using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.DTO
{
    public record ConnectionDTO(
        int NORTH,
        int SOUTH,
        List<DoorDTO> Doors,
        LadderDTO Ladder,
        int WEST,
        int EAST,
        int? UPPER,
        int? LOWER
    ) : IDTO;

}
