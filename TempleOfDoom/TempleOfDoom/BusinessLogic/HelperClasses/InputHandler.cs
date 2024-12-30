using TempleOfDoom.HelperClasses;

namespace TempleOfDoom.BusinessLogic.HelperClasses;

public class InputHandler(GameLevel gameLevel)
{
    public void HandleMovement(ConsoleKey key)
    {
        int currentX = gameLevel.Player.Position.GetX();
        int currentY = gameLevel.Player.Position.GetY();

        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (currentY > 0)
                    gameLevel.Player.Position = new Position(currentX, currentY - 1);
                break;

            case ConsoleKey.DownArrow:
                if (currentY < gameLevel.Player.CurrentRoom.Dimensions.getHeight() - 1)
                    gameLevel.Player.Position = new Position(currentX, currentY + 1);
                break;

            case ConsoleKey.LeftArrow:
                if (currentX > 0)
                    gameLevel.Player.Position = new Position(currentX - 1, currentY);
                break;

            case ConsoleKey.RightArrow:
                if (currentX < gameLevel.Player.CurrentRoom.Dimensions.getWidth() - 1)
                    gameLevel.Player.Position = new Position(currentX + 1, currentY);
                break;

            default:
                break;
        }
    }

    public void QuitGame(ConsoleKey keyInfoKey, ref bool isPlaying)
    {
        if (keyInfoKey == ConsoleKey.Q)
        {
            isPlaying = false;
            Console.Clear();
            Console.WriteLine("Spel is afgesloten bedankt voor het spelen!");
        }
    }

}