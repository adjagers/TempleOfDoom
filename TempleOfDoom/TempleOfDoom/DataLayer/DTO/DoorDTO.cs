using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.DTO
{
    public record DoorDTO(
        string Type,
        string Color,
        int NoOfStones
    ) : IDTO;
}

