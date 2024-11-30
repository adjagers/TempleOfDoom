using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.MapperStrategies
{

    public class PlayerMapper : IMapper
    {
        public IGameObject Map(IDTO dto)
        {
            PlayerDTO playerDTO = dto as PlayerDTO;
            return new Player()
            {
                CurrentRoomId = playerDTO.StartRoomId,
                Lives = playerDTO.Lives,
                Position = new Position(playerDTO.StartX, playerDTO.StartY)
            };
        }
    }
}

