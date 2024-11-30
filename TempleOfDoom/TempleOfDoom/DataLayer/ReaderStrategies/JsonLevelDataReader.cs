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
        public GameLevelDTO ReadFile(string path)
        {
            try
            {
                string json = File.ReadAllText(path);
                GameLevelDTO root = JsonSerializer.Deserialize<GameLevelDTO>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return root;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during JSON deserialization: {e.Message}");
                return null;
            }
        }

    }
}