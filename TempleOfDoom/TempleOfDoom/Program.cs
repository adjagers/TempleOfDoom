using System.Text;

namespace TempleOfDoom
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            
            // TODO:: Making sure the level path is dynamic for now its easier because of the testing.
            
            string levelPath = "/Users/anton/Documents/GitHub/TempleOfDoom/TempleOfDoom/TempleOfDoom/DataLayer/GameLevels/TempleOfDoom.json";
            
            // TODO:: Ik denk dat je de level path net zo goed alleen mee kan geven in de .Start
            Game game = new Game(levelPath);
            game.Start();
        }
    }
}