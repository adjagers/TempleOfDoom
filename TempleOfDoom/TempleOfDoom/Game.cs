using System;
using TempleOfDoom.DataLayer.Models;
using System.Linq;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.MapperStrategies;
using TempleOfDoom.DataLayer.Models.Items;
using TempleOfDoom.DataLayer.ReaderStrategies;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;
using TempleOfDoom.PresentationLayer; // Ensure the correct namespace for items

namespace TempleOfDoom
{
    public class Game
    {
        private GameLevel _gameLevel;
        private RenderBuffer _renderBuffer;

        public Game(string fileName)
        {
            _gameLevel = LoadGameLevel(fileName);
            _renderBuffer = new RenderBuffer();
        }

        private GameLevel LoadGameLevel(string fileName)
        {
            ILevelDataReader levelDataReader = new JsonLevelDataReader();
            GameLevelDTO gameLevelDTO = levelDataReader.ReadFile(fileName);
            GameLevelMapper gameLevelMapper = new GameLevelMapper();
            return (GameLevel)gameLevelMapper.Map(gameLevelDTO);
        }

        private void WelcomeMessage(string levelPath)
        {
            Console.WriteLine("   Welcome to Temple of Doom                                                            \n" +
                   $"   Current Level: {levelPath}\n" +
                   "----------------------------------------------------------------------------------------\n" +
                   "----------------------------------------------------------------------------------------\n");
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

        internal void Render(string levelFilePath)
        {
            WelcomeMessage(levelFilePath);
            Console.WriteLine(GetPlayerLives(_gameLevel.Player));
            Console.WriteLine(CopyrightMessage());

            // Show the room based on currentRoomId
            ShowCurrentRoom();
            _renderBuffer.Render();  // This will print the room layout to the console
        }



        private void ShowCurrentRoom()
        {
            // Get the room by the player's currentRoomId
            Room currentRoom = _gameLevel.Rooms.FirstOrDefault(r => r.Id == _gameLevel.Player.CurrentRoomId);
            
            if (currentRoom != null)
            {
                Console.WriteLine($"You are in Room: {currentRoom.Id}");
                Console.WriteLine($"Room Size: {currentRoom.Width} x {currentRoom.Height}");

                if (currentRoom.Items != null && currentRoom.Items.Any())
                {
                    Console.WriteLine("Items in this room:");

                    foreach (var item in currentRoom.Items)
                    {
                        // Check if the item has a position
                        if (item.Position != null)
                        {
                            Console.WriteLine($"- {item.GetType().Name} at ({item.Position.getX()}, {item.Position.getY()})");
                        }
                        else
                        {
                            Console.WriteLine($"- {item.GetType().Name} (no position)");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No items in this room.");
                }
            }
            else
            {
                Console.WriteLine("You are in an unknown room.");
            }
            PrintAllRoomsWithItems();
        }
        
        public void PrintAllRoomsWithItems()
        {
            foreach (var room in _gameLevel.Rooms)
            {
                // Print basic room details
                Console.WriteLine($"Room ID: {room.Id}, Width: {room.Width}, Height: {room.Height}");
        
                // Check if the room contains items
                if (room.Items != null && room.Items.Any())
                {
                    Console.WriteLine("   Items in this room:");
            
                    // Print details of each item
                    foreach (var item in room.Items)
                    {
                        // Print item details
                        string itemDetails = $"Item Type: {item.GetType().Name}, Position: ({item.Position?.getX()}, {item.Position?.getY()})";
                        Console.WriteLine(itemDetails);
                    }
                }
                else
                {
                    Console.WriteLine("   No items in this room.");
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
