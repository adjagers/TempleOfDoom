using TempleOfDoom;
using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.BusinessLogic.FactoryMethodes;
using TempleOfDoom.BusinessLogic.HelperClasses;
using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.DataLayer;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;
using TempleOfDoom.PresentationLayer;

public class Game : IObserver<Player>
{
    private GameLevel _gameLevel;
    private InputHandler _movementHandler;
    private DebugPrinter _debugPrinter;


    public Game(string fileName)
    {
        _gameLevel = LoadGameLevel(fileName);
        _movementHandler = new InputHandler(_gameLevel);
        _gameLevel.Player.Subscribe(this);
        
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
        Render();
        while (isPlaying)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            isPlaying = !_gameLevel.Player.GameOverCheck();
            if (!isPlaying) RenderGameOver();
            _movementHandler.HandleMovement(keyInfo.Key);
            _movementHandler.QuitGame(keyInfo.Key, ref isPlaying);
        }
    }

    private void RenderGameOver()
    {
        Console.Clear();
        Console.WriteLine("Game over");
    }


    public void Render()
    {
        Console.Clear(); // Clear the console to render again
        Room currentRoom = _gameLevel.Player.CurrentRoom;

        Console.WriteLine($"lives {_gameLevel.Player.Lives}");
        
        _debugPrinter = new DebugPrinter(currentRoom);
        RenderRoomGrid(currentRoom);
        currentRoom.ItemCheck(_gameLevel.Player);
        
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
                // Get the item at the current position (if any)
                IItem itemAtPosition =
                    currentRoom.Items.FirstOrDefault(item => item.Position?.GetX() == x && item.Position?.GetY() == y);
                // Determine which type of object is in the current cell (Player, Item, Door, Wall, or Empty)
                if (currentRoom.IsDoor(x, y, currentRoom))
                {
                    RenderCell(characterFactory.GetDoorCharacterAndColor(currentRoom.Connections));
                }
                else if (itemAtPosition != null)
                {
                    RenderCell(characterFactory.GetCharacterWithColor(itemAtPosition));
                }
                else if (_gameLevel.Player.IsPlayerPosition(x, y))
                {
                    RenderCell(characterFactory.GetCharacterWithColor(_gameLevel.Player));
                }
                else if (currentRoom.IsWall(x, y, currentRoom))
                {
                    RenderCell(characterFactory.GetWallCharacterAndColor());
                }
                else
                {
                    Console.Write("   "); // Empty space for other positions
                }
            }

            Console.WriteLine();
        }
    }

    private void RenderCell((char character, ConsoleColor color) characterAndColor)
    {
        Console.ForegroundColor = characterAndColor.color;
        Console.Write($" {characterAndColor.character} ");
        Console.ResetColor();
    }


// Method to check if the position is a wall (implement your wall check logic here)


    private void HandleDoorTransition(Room currentRoom)
    {
        // Check if the player is in the current room and is on the door
        if (_gameLevel.Player.CurrentRoom == currentRoom && currentRoom.IsPlayerOnDoor(_gameLevel.Player.Position))
        {
            Direction? doorDirection = currentRoom.GetDoorDirection(_gameLevel.Player.Position);
        
            if (doorDirection.HasValue)
            {
                // Get the adjacent room from the door direction
                Room nextRoom = currentRoom.AdjacentRooms[doorDirection.Value];
                Console.WriteLine($"Moving player through the {doorDirection.Value} door to the room.");

                // Move the player through the door to the next room
                _gameLevel.Player.MoveThroughDoor(nextRoom, doorDirection.Value);
            }
            else
            {
                Console.WriteLine("Player is not on any door.");
            }
        }
    }


    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(Player value)
    {
        Console.WriteLine("Observer triggered: Player's state has changed.");
        Console.WriteLine($"New Position: X = {value.Position.GetX()}, Y = {value.Position.GetY()}");

        // Optionally add a counter or timestamp to debug how many times this is called
        int callCount = 0;
        callCount++;
        Console.WriteLine($"OnNext called {callCount} times.");
        Render(); // Re-render the game state based on updated player position
    }



}
