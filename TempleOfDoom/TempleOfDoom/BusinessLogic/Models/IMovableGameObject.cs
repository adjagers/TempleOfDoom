using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.BusinessLogic.HelperClasses;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.BusinessLogic.Models;

public interface IMovableGameObject : IGameObject
{
    public Position Position { get; }
    public int Lives { get; }

    public bool IsDead { get; }

    public void Damage(int amount);

    public void Move(Direction direction);

    public Direction GetLastDirection();
}