using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.HelperClasses;

namespace TempleOfDoom.BusinessLogic.HelperClasses
{
    public class InputHandler
    {
        private GameLevel gameLevel;

        public InputHandler(GameLevel gameLevel)
        {
            this.gameLevel = gameLevel;
        }

        public void HandleMovement(ConsoleKey key)
        {
            // Map key to direction
            Direction? direction = DirectionExtension.ToDirectionOrNull(key);

            if (direction.HasValue)
            {
                // Let the player decide if it can move
                gameLevel.Player.Move(direction.Value);
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
}
