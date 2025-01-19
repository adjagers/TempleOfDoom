using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.FactoryMethodes
{
    public interface IFactory
    {
        public IGameObject Create(IDTO dto);
    }
}