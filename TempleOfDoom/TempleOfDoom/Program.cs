﻿using System;
using System.Text;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.DataLayer.ReaderStrategies;
using TempleOfDoom.DataLayer.MapperStrategies;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            // Specify the path to the level data (e.g., JSON file)
            string levelPath = "/Users/anton/Documents/GitHub/Tempeltje/TempleOfDoom/TempleOfDoom/GameLevels/TempleOfDoom.json";

            // Create an instance of the JsonLevelDataReader
            ILevelDataReader levelDataReader = new JsonLevelDataReader();

            // Read the game level data from the file
            GameLevelDTO gameLevelDTO = levelDataReader.ReadFile(levelPath);

            if (gameLevelDTO == null)
            {
                Console.WriteLine("Failed to load the game level.");
                return;
            }

            // Create an instance of the GameLevelMapper to map the DTO to GameLevel
            GameLevelMapper gameLevelMapper = new GameLevelMapper();

            // Map the GameLevelDTO to GameLevel
            GameLevel gameLevel = (GameLevel)gameLevelMapper.Map(gameLevelDTO);

            // Create an instance of Game using the loaded level
            Game game = new Game(levelPath);

            // Render the game (could include details about the player, rooms, etc.)
            game.Render(levelPath);
        }
    }
}