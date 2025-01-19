using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.DTO
{
    public record RoomDTO(
        int Id,
        string Type,
        int Width,
        int Height,
        List<ItemDTO> Items,
        List<EnemyDTO> Enemies
    ) : IDTO;
}
