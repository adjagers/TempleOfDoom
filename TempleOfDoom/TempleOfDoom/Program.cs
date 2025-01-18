using System.Text;
using TempleOfDoom.PresentationLayer;

namespace TempleOfDoom
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            string levelsDirectory = Path.Combine(AppContext.BaseDirectory, "../../../DataLayer/GameLevels");
            // Try to get the level files
            string[] levelFiles = DialogueSystem.GetLevelFiles(levelsDirectory);

            if (levelFiles == null)
            {
                // If directory doesn't exist or no level files found, exit early
                return;
            }
            // Display available levels
            DialogueSystem.PrintAvailableLevels(levelFiles);
            // Get the user's level selection
            int selectedLevel = DialogueSystem.GetUserLevelSelection(levelFiles.Length);
            if (selectedLevel != -1)
            {
                string levelPath = levelFiles[selectedLevel - 1];

                try
                {
                    // Start the game with the selected level
                    Game game = new Game(levelPath);
                    game.Start();
                }
                catch (Exception ex)
                {
                    // Catch any exception that occurs during game initialization or start
                    DialogueSystem.PrintError($"An error occurred while starting the game: {ex.Message}");
                }
            }
            else
            {
                DialogueSystem.PrintInvalidSelection();
            }
        }
    }
}