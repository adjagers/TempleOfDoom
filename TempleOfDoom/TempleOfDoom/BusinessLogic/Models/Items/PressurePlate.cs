using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.Models.Items;

public class PressurePlate : IItem
{
    public Position? Position { get; set; }
    
    public PressurePlate(Position position)
    {
        Position = position;
    }
    public void Interact(Player player)
    {
       Console.WriteLine($"Pressure Plate triggerd on NOT IMPLEMENTED (OBSERVER PATTERN): {Position}");
    }
}