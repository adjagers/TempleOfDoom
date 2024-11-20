using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.DTO;

namespace TempleOfDoom.Interfaces
{
    public interface ILevelDataReader
    {
        public RootDTO readFile(string path);
    }
}
