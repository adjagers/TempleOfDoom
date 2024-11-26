using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.ReaderStrategies
{
    public class JsonLevelDataReader : ILevelDataReader
    {
        public RootDTO readFile(string path)
        {
           {
               try
               {
                    string json = File.ReadAllText(path);
                    RootDTO root = JsonSerializer.Deserialize<RootDTO>(json);
                    return root;
               }
               catch (Exception e)
               {
                    Console.WriteLine(e.Message);
                    return null;
               }
           }
        }
    }
}