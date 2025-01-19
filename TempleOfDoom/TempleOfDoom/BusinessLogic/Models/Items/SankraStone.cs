using TempleOfDoom.BusinessLogic.HelperClasses;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.Models.Items;

public class SankaraStone : IItem
{
    public Position? Position { get; set; }

    public SankaraStone(Position position)
    { 
        Position = position;
    }
    public void Interact(Player player)
    {
        player.AddItemInventory(this);
        Position = null;
        Console.WriteLine("SankaraStone");
    }
}