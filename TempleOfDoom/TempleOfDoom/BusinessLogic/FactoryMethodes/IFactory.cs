using TempleOfDoom.Interfaces;

namespace TempleOfDoom.BusinessLogic.FactoryMethodes
{
    public interface IFactory
    {
        public IGameObject Create(IDTO dto);
    }
}