using System;
using TempleOfDoom.Interfaces;
using TempleOfDoom.DataLayer.ReaderStrategies;
using TempleOfDoom;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer;
using TempleOfDoom.DataLayer.MapperStrategies;
using TempleOfDoom.DataLayer.Models;

namespace TempleOfDoom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a JsonLevelDataReader to read the level data from a file

     

            // Read the level data from a JSON file
            string path = "C:\\Users\\Anton Jagers\\OneDrive\\Documenten\\Documenten\\GitHub\\TempleOfDoom\\TempleOfDoom\\TempleOfDoom\\TempleOfDoom.json";
            LevelReader reader = new LevelReader(path);
            PlayerDTO levelData = reader.GameLevelDTO.Player;
            if (levelData != null)
            {
                // Map the RootDTO to a GameLevel object
                IMapper mapper = new PlayerMapper();
                Player player = (Player)mapper.Map(levelData);
                Console.WriteLine($"Player CurrentRoomID {player.CurrentRoomId}");
            }
            else
            {
                Console.WriteLine("Failed to load level data.");
            }
        }
    }
}
