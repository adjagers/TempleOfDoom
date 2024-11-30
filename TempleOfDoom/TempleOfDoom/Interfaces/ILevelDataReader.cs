using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.DataLayer.DTO;

namespace TempleOfDoom.Interfaces
{
    public interface ILevelDataReader
    {
        public GameLevelDTO ReadFile(string path);
    }
}
