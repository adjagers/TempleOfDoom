using System.ComponentModel;
using TempleOfDoom.BusinessLogic.Models.Doors;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.BusinessLogic.FactoryMethodes
{
    public class ConnectionFactory : IFactory
    {

        private DoorFactory _doorFactory; 
        public ConnectionFactory() {
          _doorFactory = new DoorFactory();
        }
        public IGameObject Create(IDTO dto)
        {
            if (dto == null) return null;

            ConnectionDTO connectionDTO = dto as ConnectionDTO;
            IDoor door = new BasicDoor(true);

            foreach (var dtoDoor in connectionDTO.Doors)
            {
                door = _doorFactory.CreateDoor(dtoDoor);
            }

            Connection connection = new Connection
            {
               Doors = door,
               NORTH = connectionDTO.NORTH,
               WEST = connectionDTO.WEST,
               EAST = connectionDTO.EAST,
               SOUTH = connectionDTO.SOUTH,
            };

            return connection;
        }




    }
}