using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.DTO;

public class LadderDTO : IDTO
{
    public int UpperX { get; set; }
    public int UpperY { get; set; }
    public int LowerX { get; set; }
    public int LowerY { get; set; }
}