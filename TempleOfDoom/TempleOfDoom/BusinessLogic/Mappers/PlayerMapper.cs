using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.BusinessLogic.MapperStrategies
{

    public class PlayerMapper : IMapper
    {
        public IGameObject Map(IDTO dto)
        {
            PlayerDTO playerDTO = dto as PlayerDTO;
            return new Player
            {
                CurrentRoom = new Room(),
                Lives = playerDTO.Lives,
                Position = new Position(playerDTO.StartX, playerDTO.StartY)
            };
        }
    }
}

