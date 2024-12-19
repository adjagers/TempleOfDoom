using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.BusinessLogic.Models
{
    public class Connection(Room connectedRoom, IDoor door) : IGameObject
    {
        public IDoor Door { get; set; } = door;
        public Room ConnectedRoom { get; set; } = connectedRoom;
    }
}
