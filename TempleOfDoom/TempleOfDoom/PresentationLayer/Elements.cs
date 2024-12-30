using TempleOfDoom.DataLayer.Models.Items;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.PresentationLayer;

public class Elements
{
    public static readonly char PlayerChar = 'P';
    public static readonly char EmptyChar = ' ';
    public static readonly char SankaraChar = 'S';
    public static readonly char DisappearingBoobytrapChar = 'D';
    public static readonly char PressureplateChar = 'D';
    public static readonly char BoobytrapChar = 'B';
    public static readonly char KeyChar = 'P';



    public static char GetItemOnScreen(IItem item)
    {
        return item switch
        {
            SankaraStone => SankaraChar,
            DisappearingBoobytrap => DisappearingBoobytrapChar,
            Boobytrap => BoobytrapChar,
            Key => SankaraChar,
            PressurePlate => PressureplateChar
        };
    }
}