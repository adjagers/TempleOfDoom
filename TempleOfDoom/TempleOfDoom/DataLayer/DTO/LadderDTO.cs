using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.DTO
{
    public record LadderDTO(
        int UpperX,
        int UpperY,
        int LowerX,
        int LowerY
    ) : IDTO; 
}

