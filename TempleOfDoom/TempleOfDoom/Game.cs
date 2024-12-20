using System;
using TempleOfDoom.DataLayer.Models;
using System.Linq;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models.Items;
using TempleOfDoom.DataLayer.ReaderStrategies;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;
using TempleOfDoom.PresentationLayer;
using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.BusinessLogic.FactoryMethodes;

namespace TempleOfDoom
{
    public class Game
    {
        private GameLevel _gameLevel;
        private GameLevelFactory _gameLevelFactory;


        public Game(string fileName)
        {
            _gameLevel = LoadGameLevel(fileName);

        }

        private GameLevel LoadGameLevel(string fileName)
        {
            ILevelDataReader levelDataReader = new JsonLevelDataReader();
            GameLevelDTO gameLevelDTO = levelDataReader.ReadFile(fileName);
            _gameLevelFactory = new GameLevelFactory();
           return (GameLevel)_gameLevelFactory.Create(gameLevelDTO);
        }



        

        private string GetPlayerLives(Player player)
        {
            return $"Lives: {player.Lives}\n";
        }

        private string CopyrightMessage()
        {
            return "----------------------------------------------------------------------------------------\n" +
                   "----------------------------------------------------------------------------------------\n" +
                   "   A game for the course Code Development (24/25) by Marco van Spengen and Anton Jagers  \n" +
                   "----------------------------------------------------------------------------------------\n" +
                   "----------------------------------------------------------------------------------------\n";
        }
                
        private readonly Dictionary<Position, char> _frame = new();
        
        
        public void SetPixel(Position pos, char value)
        {
            _frame[pos] = value;
            Console.WriteLine(value);
        }
        
        
        private void BuildItems(Room playerCurrentRoom)
        {
            foreach (IItem item in playerCurrentRoom.Items)
            {
                SetPixel(item.Position, Elements.GetItemOnScreen(item));
            }
        }
        
            public void Render()
            {
            }
           
        public void PrintAllRoomsWithItems()
        {
            foreach (var room in _gameLevel.Rooms)
            {

        
                // Check if the room contains items
                if (room.Items != null && room.Items.Any())
                {
                    Console.WriteLine("   Items in this room:");
            
                    // Print details of each item
                    foreach (var item in room.Items)
                    {
                        // Print item details
                        string itemDetails = $"Item details hier printen zoals dimension)";
                        Console.WriteLine(itemDetails);
                    }
                }
                else
                {
                    Console.WriteLine("No items in this room.");
                }
        
                Console.WriteLine(); // Space between rooms
            }
        }


        // Example of interacting with an item (you can trigger this based on player action)
        private void InteractWithItem(Player player, IItem item)
        {
            item.Interact(player);
            Console.WriteLine($"You interacted with a {item.GetType().Name}");
        }
    }
}
