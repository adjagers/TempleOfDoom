namespace TempleOfDoom
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string filePath = "./Levels/TempleOfDoom1.json";


            string fileName = Path.GetFileName(filePath);


            Game game = new Game(fileName);
            game.Render(fileName);
        }



    }
}