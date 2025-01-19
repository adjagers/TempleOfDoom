using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.DTO
{
    public record ItemDTO(
        string Type,
        int? Damage,
        int X,
        int Y,
        string Color
    ) : IDTO;
}