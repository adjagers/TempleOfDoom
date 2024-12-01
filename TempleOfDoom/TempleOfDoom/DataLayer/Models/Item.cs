using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.Models
{
    public class Item: IGameObject
    {
        public string Type { get; set; }
        public int Damage { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Color { get; set; }
    }
}