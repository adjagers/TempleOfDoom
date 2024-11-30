using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.Models
{
    public class Connection : IGameObject
    {
        public int NORTH { get; set; }
        public int SOUTH { get; set; }
        public IDoor Doors { get; set; }
        public int WEST { get; set; }
        public int EAST { get; set; }
    }
}
