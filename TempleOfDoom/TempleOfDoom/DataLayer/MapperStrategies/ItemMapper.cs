using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;

namespace TempleOfDoom.DataLayer.MapperStrategies
{
    public class ItemMapper
    {
        internal Item Map(ItemDTO itemDTO)
        {
            if (itemDTO == null)
            {
                Console.WriteLine("ItemMapper: Received a null ItemDTO.");
                return null;
            }

            Console.WriteLine($"ItemMapper: Mapping ItemDTO with type={itemDTO.Type}, damage={itemDTO.Damage}");

            // Maak een nieuw Item-object en zet de velden over van itemDTO
            Item item = new Item
            {
                Type = itemDTO.Type,
                Damage = itemDTO.Damage,
                X = itemDTO.X,
                Y = itemDTO.Y,
                Color = itemDTO.Color
            };

            Console.WriteLine($"ItemMapper: Mapped Item with Type={item.Type}, Damage={item.Damage}, X={item.X}, Y={item.Y}, Color={item.Color}");
            return item;
        }
    }
}
