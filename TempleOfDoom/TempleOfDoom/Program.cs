using System;
using System.Text;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.DataLayer.ReaderStrategies;
using TempleOfDoom.Interfaces;
using TempleOfDoom.BusinessLogic.FactoryMethodes;

namespace TempleOfDoom
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            // Specify the path to the level data (e.g., JSON file)
            string levelPath = "/Users/anton/Desktop/TempleOfDoom.nosync/TempleOfDoom/TempleOfDoom/DataLayer/GameLevels/TempleOfDoom.json";
            // Create an instance of the JsonLevelDataReader
            ILevelDataReader levelDataReader = new JsonLevelDataReader();
            // Read the game level data from the file
            GameLevelDTO gameLevelDTO = levelDataReader.ReadFile(levelPath);
            if (gameLevelDTO == null)
            {
                Console.WriteLine("Failed to load the game level.");
                return;
            }
            // Create an instance of Game using the loaded level
            Game game = new Game(levelPath);
            // Render the game for the current room
            game.Render();
        }
    }
}