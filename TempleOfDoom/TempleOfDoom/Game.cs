using TempleOfDoom;
using TempleOfDoom.BusinessLogic;
using TempleOfDoom.BusinessLogic.FactoryMethodes;
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


    private readonly Frame _frame = new();


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
            if (enemy.Position.Equals(_gameLevel.Player.Position))
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
        _frame.Clear();
        BuildRooms(_gameLevel.Player);
        BuildPlayer(_gameLevel.Player);
        BuildEnemy(currentRoom);
        BuildItems(currentRoom);
        BuildLadder(currentRoom);
        BuildDoor(currentRoom);
        _frame.Render();
        currentRoom.ItemCheck(_gameLevel.Player);
        
        // TODO:: Needs to replaced into a different class.
        HandleDoorTransition(currentRoom);
    }

    private void BuildRooms(Player player)
    {
        Room currentRoom = player.CurrentRoom;

        // Draw top and bottom walls
        for (int x = currentRoom.LeftX(); x <= currentRoom.RightX(); x++)
        {
            _frame.SetPixel(new Position(x, currentRoom.TopY()), CharacterFactory.GetWallCharacter());
            _frame.SetPixel(new Position(x, currentRoom.BottomY()), CharacterFactory.GetWallCharacter());
        }

        // Draw left and right walls
        for (int y = currentRoom.BottomY(); y <= currentRoom.TopY(); y++)
        {
            _frame.SetPixel(new Position(currentRoom.LeftX(), y), CharacterFactory.GetWallCharacter());
            _frame.SetPixel(new Position(currentRoom.RightX(), y), CharacterFactory.GetWallCharacter());
        }
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
    private void BuildDoor(Room currentRoom)
    {
        var doorPositions = GetDoorPositions(currentRoom);

        foreach (var direction in currentRoom.AdjacentRooms.Keys)
        {
            if (doorPositions.TryGetValue(direction, out var getPosition))
            {
                var connection = currentRoom.GetConnectionByDirection(direction);
                if (connection != null)
                {
                    char transitionCharacter = CharacterFactory.GetTransitionCharacter(connection.Transition);
                    (int doorX, int doorY) = getPosition();
                    _frame.SetPixel(new Position(doorX, doorY), transitionCharacter);
                }
            }
        }
    }

    private Dictionary<Direction, Func<(int, int)>> GetDoorPositions(Room currentRoom)
    {
        return new Dictionary<Direction, Func<(int, int)>>()
        {
            { Direction.NORTH, () => (currentRoom.Dimensions.getWidth() / 2, 0) },
            { Direction.SOUTH, () => (currentRoom.Dimensions.getWidth() / 2, currentRoom.Dimensions.getHeight() - 1) },
            { Direction.WEST, () => (0, currentRoom.Dimensions.getHeight() / 2) },
            { Direction.EAST, () => (currentRoom.Dimensions.getWidth() - 1, currentRoom.Dimensions.getHeight() / 2) }
        };
    }

    private void BuildEnemy(Room currentRoom)
    {
        foreach (IAutoMovableGameObject enemy in currentRoom.Enemies)
        {
            _frame.SetPixel(enemy.Position, CharacterFactory.GetMovableCharacter(enemy));
        }
    }
    private void BuildLadder(Room currentRoom)
    {
        foreach (var connection in currentRoom.Connections)
        {
            if (connection.Transition is Ladder ladder)
            {
                (int ladderX, int ladderY) = GetLadderCoordinates(currentRoom, connection, ladder);
                _frame.SetPixel(new Position(ladderX, ladderY), CharacterFactory.GetTransitionCharacter(ladder));
            }
        }
    }


    private void BuildItems(Room currentRoom)
    {
        foreach (IItem item in currentRoom.Items)
        {
            if (item.Position != null) _frame.SetPixel(item.Position, CharacterFactory.GetItemDisplay(item));
        }
    }
    private void BuildPlayer(Player player)
    {
        _frame.SetPixel(player.Position, CharacterFactory.GetMovableCharacter(player));
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
