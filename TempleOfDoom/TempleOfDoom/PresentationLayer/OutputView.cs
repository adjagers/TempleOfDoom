using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.PresentationLayer
{
    internal class OutputView
    {
        Player player;
        private void WelcomeMessage(string levelPath)
        {
            Console.WriteLine("   Welcome to Temple of Doom                                                            \n" +
                   $"   Current Level: {levelPath}\n" +
                   "----------------------------------------------------------------------------------------\n" +
                   "----------------------------------------------------------------------------------------\n");
        }

        private string GetPlayerLives(Player player)
        {
            return $"Lives: {player.Lives}\n";
        }
        private string CopyrightMessage()
        {
            return "----------------------------------------------------------------------------------------\n" +
                   "----------------------------------------------------------------------------------------\n" +
                   "   A game for the course Code Development (24/25) by Marco van Spengen and Anton Jagers  \n" +
                   "----------------------------------------------------------------------------------------\n" +
                   "----------------------------------------------------------------------------------------\n";
        }

        internal void Render(String fileName)
        {
            WelcomeMessage(fileName);
            GetPlayerLives(player);
            CopyrightMessage();
        }


    }
}
