using TempleOfDoom.BusinessLogic;
using TempleOfDoom.BusinessLogic.Models;
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

    // TODO:: No time for it but make sure this cant be done such its setting the Player Lives that should not be public
    public virtual void Interact(Player player)
    {
        Console.WriteLine("Oh no! You hit a Boobytrap!");
        player.Lives -= _damage;
    }
}