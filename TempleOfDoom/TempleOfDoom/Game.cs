using System;
using TempleOfDoom.DataLayer.Models;
using System.Linq;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.MapperStrategies;
using TempleOfDoom.DataLayer.Models.Items;
using TempleOfDoom.DataLayer.ReaderStrategies;
using TempleOfDoom.Enums;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;
using TempleOfDoom.PresentationLayer; // Ensure the correct namespace for items

namespace TempleOfDoom
{
    public class Game
    {
        private GameLevel _gameLevel;


        public Game(string fileName)
        {
            _gameLevel = LoadGameLevel(fileName);
        }

        private GameLevel LoadGameLevel(string fileName)
        {
            ILevelDataReader levelDataReader = new JsonLevelDataReader();
            GameLevelDTO gameLevelDTO = levelDataReader.ReadFile(fileName);
            GameLevelMapper gameLevelMapper = new GameLevelMapper();
            return (GameLevel)gameLevelMapper.Map(gameLevelDTO);
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
        
            public void Render(int currentRoomId)
            {
                // Find the current room using the currentRoomId
                Room currentRoom = _gameLevel.Rooms.FirstOrDefault(room => room.Id == currentRoomId);

                if (currentRoom == null)
                {
                    Console.WriteLine($"Room with ID {currentRoomId} not found.");
                    return;
                }

                // Call BuildItems with the current room
                BuildItems(currentRoom);

                // Render other game elements here...
                // For example, you can render walls, the player character, etc.
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
