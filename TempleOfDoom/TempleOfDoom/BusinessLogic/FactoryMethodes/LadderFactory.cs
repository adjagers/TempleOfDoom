using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.DataLayer.DTO;

namespace TempleOfDoom.BusinessLogic.FactoryMethodes;

public class LadderFactory
{
    public ITransition CreateLadder(LadderDTO ladderDto)
    {
        return new Ladder(ladderDto);
    }
}