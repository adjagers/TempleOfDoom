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
            string path = "C:\\Users\\Anton Jagers\\OneDrive\\Documenten\\Documenten\\GitHub\\TempleOfDoom\\TempleOfDoom\\TempleOfDoom\\TempleOfDoom.json";
            LevelReader reader = new LevelReader(path);
            GameLevelDTO levelData = reader.GameLevelDTO;

            Console.WriteLine($"{levelData.Connections.Count}");

            if (levelData != null)
            {
                IMapper mapper = new GameLevelMapper();
                GameLevel gameLevel = (GameLevel)mapper.Map(levelData);
                Console.WriteLine($"Player CurrentRoomID {gameLevel.Player.CurrentRoomId}");
            }
            else
            {
                Console.WriteLine("Failed to load level data.");
            }
        }
    }
}
