using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.Models;

public class Inventory
{
    
    private ICollection<IItem> Items { get; } = new List<IItem>();

    public void AddItem(IItem item)
    {
        Items.Add(item);
    }
    
}