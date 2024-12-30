using TempleOfDoom;
using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.BusinessLogic.FactoryMethodes;
using TempleOfDoom.BusinessLogic.HelperClasses;
using TempleOfDoom.DataLayer;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;
using TempleOfDoom.PresentationLayer;

public class Game
{
    private GameLevel _gameLevel;
    private InputHandler _movementHandler;
    private DebugPrinter _debugPrinter;


    public Game(string fileName)
    {
        _gameLevel = LoadGameLevel(fileName);
        _movementHandler = new InputHandler(_gameLevel);
    }

    private GameLevel LoadGameLevel(string fileName)
    {
        LevelReader levelReader = new LevelReader(fileName);
        
        // TODO:: levelReader.Gamelevel little bit janky maybe later on if we have time....
        GameLevelDTO gameLevelDto = levelReader.GameLevelDTO;
        
        GameLevelFactory gameLevelFactory = new GameLevelFactory();
        
        // TODO:: Look for a different approach so we do not use the cast everytime...
        
        return (GameLevel)gameLevelFactory.Create(gameLevelDto);
    }


    public void Start()
    {
        bool isPlaying = true;
        while (isPlaying)
        {
            Console.Clear();
            Render();
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            
            
            
            _movementHandler.HandleMovement(keyInfo.Key);
            _movementHandler.QuitGame(keyInfo.Key, ref isPlaying);
        }
    }

    public void Render()
    {
        Room currentRoom = _gameLevel.Player.CurrentRoom;
        _debugPrinter = new DebugPrinter(currentRoom);
        RenderRoomGrid(currentRoom);
        
        // TODO:: Needs to replaced into a different class.
        HandleDoorTransition(currentRoom);
    }

    
    
    //TODO:: Maak dit simpeler en per apart object dit ziet er nog een beetje rommelig uit...
    // TODO:: Zorg dat deuren op horizontale en verticale manier anders geprint worden...
    private void RenderRoomGrid(Room currentRoom)
    {
        CharacterFactory characterFactory = new CharacterFactory();
        for (int y = 0; y < currentRoom.Dimensions.getHeight(); y++)
        {
            for (int x = 0; x < currentRoom.Dimensions.getWidth(); x++)
            {
                IItem itemAtPosition = currentRoom.Items.FirstOrDefault(item => item.Position?.GetX() == x && item.Position?.GetY() == y);

                if (itemAtPosition != null)
                {
                    Console.Write($" {characterFactory.GetCharacter(itemAtPosition)} "); // Toon item-karakter
                }
                else if (_gameLevel.Player.IsPlayerPosition(x, y))
                {
                    Console.Write($" {characterFactory.GetCharacter(_gameLevel.Player)} "); // Speler weergeven
                }
                else
                {
                    RenderWallOrDoor(x, y, currentRoom); // Toon muren of deuren
                }
            }
            Console.WriteLine();
        }
    }
    private void RenderWallOrDoor(int x, int y, Room currentRoom)
    {
        if (currentRoom.IsWallOrDoor(x, y, currentRoom))
        {
            Console.Write(currentRoom.IsDoor(x, y, currentRoom) ? " D " : " # ");
        }
        else
        {
            Console.Write("   "); // Lege ruimte
        }
    }
    private void HandleDoorTransition(Room currentRoom)
    {
        // Check of de speler zich op een deur bevindt
        if (_gameLevel.Player.CurrentRoom == currentRoom && currentRoom.IsPlayerOnDoor(_gameLevel.Player.Position))
        {
            Direction? doorDirection = currentRoom.GetDoorDirection(_gameLevel.Player.Position);
        
            if (doorDirection.HasValue)
            {
                Room nextRoom = currentRoom.AdjacentRooms[doorDirection.Value];
                Console.WriteLine($"Moving player through the {doorDirection.Value} door to the {nextRoom.Type} room.");

                // Verplaats de speler naar de volgende kamer
                _gameLevel.Player.MoveThroughDoor(nextRoom, doorDirection.Value);
            }
            else
            {
                Console.WriteLine("Player is not on any door.");
            }
        }
    }
}
