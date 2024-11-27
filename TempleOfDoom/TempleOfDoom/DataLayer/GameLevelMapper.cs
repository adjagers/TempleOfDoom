using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;
using TempleOfDoom;

public class GameLevelMapper : IMapper
{
    public IGameObject Map(IDTO dto)
    {
        var player = dto as Player;
            var gameLevel = new GameLevel();
            return gameLevel;
       
    }
}
