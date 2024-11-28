using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.MapperStrategies
{
    public class RoomMapper : IMapper
    {
        public RoomMapper(ItemMapper itemMapper) {
        
        }

        public IGameObject Map(IDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
