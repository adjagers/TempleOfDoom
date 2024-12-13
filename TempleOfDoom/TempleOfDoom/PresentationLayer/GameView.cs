using TempleOfDoom.HelperClasses;

namespace TempleOfDoom.PresentationLayer;

// NOTE:: Dit is voor de standaard Console Writes..

public static class GameView
{
    
    private static readonly GameFrame _frame = new();
    
    
    private static string GetTopUI(string levelPath)
    {
        return "   Welcome to Temple of Doom                                                             \n" +
               $"   Current Level: {levelPath}															  \n" +
               "----------------------------------------------------------------------------------------\n" +
               "----------------------------------------------------------------------------------------\n";
    }

    private static string GetBottomUI()
    {
        return "----------------------------------------------------------------------------------------\n" +
               "----------------------------------------------------------------------------------------\n" +
               "   A game for the course Code Development (23/24) by Lucas Kooijmans and Sven Goossens  \n" +
               "----------------------------------------------------------------------------------------\n" +
               "----------------------------------------------------------------------------------------\n";
    }
}
    
    