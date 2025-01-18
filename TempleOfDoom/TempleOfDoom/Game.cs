using TempleOfDoom;
using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.BusinessLogic.FactoryMethodes;
using TempleOfDoom.BusinessLogic.HelperClasses;
using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.BusinessLogic.Models.Enemy;
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
        bool playerMoved = false; // Flag to track if player moved
        while (!_gameLevel.Player.IsDone)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            isPlaying = !_gameLevel.Player.GameOverCheck();
            if (!isPlaying) RenderGameOver();


            // Check if the player moved
            if (keyInfo.Key != ConsoleKey.Spacebar)
            {
                _movementHandler.HandleMovement(keyInfo.Key);
                playerMoved = true; // Player moved, so update enemies
            }

            // If the player shot, update damage but don't move enemies
            if (keyInfo.Key == ConsoleKey.Spacebar)
            {
                DamageEnemies();
                playerMoved = false; // Don't update enemies when shooting
            }


            else if (_gameLevel.HasPlayerCollectedRequiredStones(_gameLevel.GetTotalSankaraStones()))
            {
                Console.Clear();
                Console.WriteLine($"Player's Sankara Stones: {_gameLevel.Player.SankaraStones}");
                Console.WriteLine($"Required Sankara Stones: {_gameLevel.GetTotalSankaraStones()}");
                Console.WriteLine("You won!");
                Thread.Sleep(3000);
            }

            // Only move enemies if the player moved
            if (playerMoved)
            {
                foreach (var enemy in _gameLevel.Player.CurrentRoom.Enemies)
                {
                    if (enemy == null)
                    {
                        Console.WriteLine("Error: Enemy is null in the current room.");
                        continue;
                    }

                    enemy.AutomaticallyMove();
                    CheckPlayerEnemyCollision(); // Update enemy movement
                }
            }
        }
    }

    private void CheckPlayerEnemyCollision()
    {
        foreach (var enemy in _gameLevel.Player.CurrentRoom.Enemies)
        {
            if (enemy.Position.GetX() == _gameLevel.Player.Position.GetX() &&
                enemy.Position.GetY() == _gameLevel.Player.Position.GetY())
            {
                Console.WriteLine("Player collided with an enemy!");
                _gameLevel.Player.Damage(1); // Player loses a life
                break; // Only process one collision per move
            }
        }
    }


    private void DamageEnemies()
    {
        Console.WriteLine("SHOOT");

        // Null check for _gameLevel, _gameLevel.Player, and _gameLevel.Player.CurrentRoom
        if (_gameLevel?.Player?.CurrentRoom?.Enemies == null)
        {
            Console.WriteLine("Error: Enemies list is null.");
            return; // Early return if Enemies is null
        }

        // Iterate over the enemies in the current room
        _gameLevel.Player.CurrentRoom.Enemies.ForEach(entity =>
        {
            // Check if the entity is of type EnemyAdapter and not null
            if (entity is not EnemyAdapter enemy) return;

            // Check if the enemy's position is adjacent to the player's position
            if (enemy.Position.IsAdjacentTo(_gameLevel.Player.Position))
            {
                _gameLevel.Player.Attack(enemy);
            }
        });
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
        Console.WriteLine($" Amount of Sankara stones in game: {_gameLevel.GetTotalSankaraStones()}");
        Console.WriteLine($"lives {_gameLevel.Player.Lives}");

        int requiredStones = _gameLevel.GetTotalSankaraStones(); // Or any other number based on your game logic

        if (_gameLevel.HasPlayerCollectedRequiredStones(requiredStones))
        {
            Console.WriteLine("Player has collected enough Sankara Stones!");
        }

        
        _debugPrinter = new DebugPrinter(currentRoom);
        RenderRoomGrid(currentRoom);
        currentRoom.ItemCheck(_gameLevel.Player);
        
        // TODO:: Needs to replaced into a different class.
        HandleDoorTransition(currentRoom);
    }


    private void RenderRoomGrid(Room currentRoom)
    {
        foreach (var connection in currentRoom.Connections)
        {
            Console.WriteLine(connection.Transition);
        }
        CharacterFactory characterFactory = new CharacterFactory();

        for (int y = 0; y < currentRoom.Dimensions.getHeight(); y++)
        {
            for (int x = 0; x < currentRoom.Dimensions.getWidth(); x++)
            {
                RenderCellContent(currentRoom, x, y, characterFactory);
            }

            Console.WriteLine();
        }
    }

    private void RenderCellContent(Room currentRoom, int x, int y, CharacterFactory characterFactory)
    {
        if (RenderDoor(currentRoom, x, y, characterFactory)) return;
        if (RenderLadder(currentRoom, x, y, characterFactory)) return;
        if (RenderItem(currentRoom, x, y, characterFactory)) return;
        if (RenderPlayer(x, y, characterFactory)) return;
        if (RenderWall(currentRoom, x, y, characterFactory)) return;
        if (RenderMovableGameObject(currentRoom, x, y, characterFactory)) return;

        Console.Write("   "); // Empty space for other positions
    }

    private bool RenderDoor(Room currentRoom, int x, int y, CharacterFactory characterFactory)
    {
        if (currentRoom.IsDoor(x, y, currentRoom))
        {
            RenderCell(characterFactory.GetDoorCharacterAndColor(currentRoom.Connections));
            return true;
        }

        return false;
    }

    private (int x, int y) GetLadderCoordinates(Room currentRoom, Connection connection, Ladder ladder)
    {
        // Determine if the ladder's coordinates should be upper or lower based on the connected room
        if (currentRoom == connection.ConnectedRoom)
        {
            return (ladder.UpperX, ladder.UpperY);
        }
        else
        {
            return (ladder.LowerX, ladder.LowerY);
        }
    }

    private bool RenderLadder(Room currentRoom, int x, int y, CharacterFactory characterFactory)
    {
        foreach (Connection connection in currentRoom.Connections)
        {
            if (connection.Transition is Ladder ladder)
            {
                // Determine ladder coordinates for the current room
                (int ladderX, int ladderY) = GetLadderCoordinates(currentRoom, connection, ladder);

                if (x == ladderX && y == ladderY)
                {
                    RenderCell(characterFactory.GetCharacterWithColor(connection.Transition));
                    return true;
                }
            }
        }

        return false;
    }


    private bool RenderItem(Room currentRoom, int x, int y, CharacterFactory characterFactory)
    {
        IItem itemAtPosition =
            currentRoom.Items.FirstOrDefault(item => item.Position?.GetX() == x && item.Position?.GetY() == y);
        if (itemAtPosition != null)
        {
            RenderCell(characterFactory.GetCharacterWithColor(itemAtPosition));
            return true;
        }

        return false;
    }

    private bool RenderPlayer(int x, int y, CharacterFactory characterFactory)
    {
        if (_gameLevel.Player.IsPlayerPosition(x, y))
        {
            RenderCell(characterFactory.GetCharacterWithColor(_gameLevel.Player));
            return true;
        }

        return false;
    }

    private bool RenderWall(Room currentRoom, int x, int y, CharacterFactory characterFactory)
    {
        if (currentRoom.IsWall(x, y, currentRoom))
        {
            RenderCell(characterFactory.GetWallCharacterAndColor());
            return true;
        }

        return false;
    }

    private bool RenderMovableGameObject(Room currentRoom, int x, int y, CharacterFactory characterFactory)
    {
        if (currentRoom.HasMovableGameObject(x, y, out IMovableGameObject gameObject))
        {
            RenderCell(characterFactory.GetMovableGameObjectCharacterWithColor(gameObject));
            return true;
        }

        return false;
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
        // Handle door transitions
        if (_gameLevel.Player.CurrentRoom == currentRoom && currentRoom.IsPlayerOnDoor(_gameLevel.Player.Position))
        {
            Direction? doorDirection = currentRoom.GetDoorDirection(_gameLevel.Player.Position);

            if (doorDirection.HasValue)
            {
                Room nextRoom = currentRoom.AdjacentRooms[doorDirection.Value];
                Console.WriteLine($"Moving player through the {doorDirection.Value} door to the next room.");
                _gameLevel.Player.MoveThroughDoor(nextRoom, doorDirection.Value);
                return; // Exit after door transition
            }
        }

        // Handle ladder transitions
        foreach (var connection in currentRoom.Connections)
        {
            if (connection.Transition is Ladder ladder)
            {
                (int ladderX, int ladderY) = GetLadderCoordinates(currentRoom, connection, ladder);

                // Check if player is on the ladder's starting position (top or bottom)
                if (_gameLevel.Player.Position.GetX() == ladderX && _gameLevel.Player.Position.GetY() == ladderY)
                {
                    Room nextRoom = connection.ConnectedRoom;

                    // Determine if the ladder leads up or down
                    Direction ladderDirection = ladderY < currentRoom.Dimensions.getHeight() / 2
                        ? Direction.UPPER
                        : Direction.LOWER;

                    Console.WriteLine($"Moving player via ladder to the next room ({ladderDirection}).");
                    _gameLevel.Player.MoveThroughDoor(nextRoom, ladderDirection);
                    return; // Exit after ladder transition
                }
            }

            Console.WriteLine("Player is not on any door or ladder.");
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
