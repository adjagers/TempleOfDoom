using TempleOfDoom.BusinessLogic.HelperClasses;
using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.Models.Items;

public class Boobytrap : IItem
{
    public Position? Position { get; set; }
    public int _damage;

    public Boobytrap(Position position, int damage)
    {
        Position = position;
        _damage = damage;
    }

    public virtual void Interact(Player player)
    {
        Console.WriteLine("Oh no! You hit a Boobytrap!");
        player.Lives -= _damage;
    }
}