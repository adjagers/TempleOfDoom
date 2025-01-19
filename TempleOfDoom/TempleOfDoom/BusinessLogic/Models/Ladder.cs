using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.BusinessLogic.Models;

public class Ladder : ITransition
{
    public int UpperX { get; }
    public int UpperY { get; }
    public int LowerX { get; }
    public int LowerY { get; }
    
    public Ladder(LadderDTO ladderDto)
    {
        UpperX = ladderDto.UpperX;
        UpperY = ladderDto.UpperY;
        LowerX = ladderDto.LowerY;
        LowerY = ladderDto.LowerY;
    }
}