using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;
using TempleOfDoom;

public class GameObjectMapper : IMapper
{
    public IGameObject Map(IDTO dto)
    {
        if (dto is RootDTO rootDto)
        {
            var gameLevel = new GameLevel();
            return gameLevel;
        }
        else return null;
    }
}
