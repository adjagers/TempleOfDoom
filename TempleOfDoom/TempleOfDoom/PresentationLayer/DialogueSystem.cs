namespace TempleOfDoom.PresentationLayer;

 public static class DialogueSystem
    {
        public static string[] GetLevelFiles(string levelsDirectory)
        {
            // Check if the directory exists
            if (!Directory.Exists(levelsDirectory))
            {
                PrintDirectoryDoesNotExist();
                return null;  // Return null if directory doesn't exist
            }

            // Get all JSON files in the directory
            string[] levelFiles = Directory.GetFiles(levelsDirectory, "*.json");

            if (levelFiles.Length == 0)
            {
                PrintInvalidFileSelection();
                return null;  // Return null if no level files are found
            }

            return levelFiles;
        }

        public static void PrintAvailableLevels(string[] levelFiles)
        {
            Console.WriteLine("Available Levels:");
            for (int i = 0; i < levelFiles.Length; i++)
            {
                Console.WriteLine($"{i + 1}: {Path.GetFileNameWithoutExtension(levelFiles[i])}");
            }
        }

        public static int GetUserLevelSelection(int levelCount)
        {
            Console.WriteLine("Select a level (enter the number):");

            if (int.TryParse(Console.ReadLine(), out int selectedLevel) && selectedLevel > 0 && selectedLevel <= levelCount)
            {
                return selectedLevel;
            }

            return -1;  // Return -1 for invalid selection
        }

        public static void PrintDirectoryDoesNotExist()
        {
            Console.WriteLine("Directory does not exist.");
        }

        public static void PrintInvalidFileSelection()
        {
            Console.WriteLine("No level files found in the directory.");
        }

        public static void PrintInvalidSelection()
        {
            Console.WriteLine("Invalid selection. Exiting...");
        }

        public static void PrintError(string errorMessage)
        {
            Console.WriteLine($"Error: {errorMessage}");
        }
    }
