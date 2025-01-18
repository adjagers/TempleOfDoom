namespace TempleOfDoom.BusinessLogic.Models;

public interface IAutoMovableGameObject : IMovableGameObject
{
    public void AutomaticallyMove();
}