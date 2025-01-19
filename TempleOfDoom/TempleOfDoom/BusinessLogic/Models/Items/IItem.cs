using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.BusinessLogic;
using TempleOfDoom.BusinessLogic.Models;

namespace TempleOfDoom.Interfaces;

public interface IItem : IGameObject
{
      Position? Position { get; set; }
      void Interact(Player player);
}