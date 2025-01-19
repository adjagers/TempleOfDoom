using TempleOfDoom.DataLayer.Models.Items;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.BusinessLogic.Models;
public class Inventory
{
    private ICollection<IItem> _Items { get; } = new List<IItem>();
    public void AddItem(IItem item)
    {
        _Items.Add(item);
    }
    public int GetSankaraStonesCount()
    {
        return _Items.Count(i => i is SankaraStone);
    }
    public bool HasKey(Color color)
    {
        return _Items.OfType<Key>().Any(key => key.Color.Equals(color));
    }
}