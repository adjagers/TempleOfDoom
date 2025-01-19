using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.BusinessLogic.HelperClasses;
using TempleOfDoom.HelperClasses;

namespace TempleOfDoom.Interfaces;

public interface IItem : IGameObject
{
      Position? Position { get; set; }
      void Interact(Player player);
}