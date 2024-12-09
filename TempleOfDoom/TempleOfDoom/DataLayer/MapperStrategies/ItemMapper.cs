using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.DataLayer.FactoryMethodes;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.MapperStrategies
{
    public class ItemMapper
    {
        private readonly ItemFactory _itemFactory;

        public ItemMapper()
        {
            _itemFactory = new ItemFactory(); // Initialize the factory
        }
        public IItem Map(ItemDTO itemDTO)
        {
            if (itemDTO == null)
            {
                Console.WriteLine("ItemMapper: Received a null ItemDTO.");
                return null;
            }

            Console.WriteLine($"ItemMapper: Mapping ItemDTO with Type={itemDTO.Type}, Damage={itemDTO.Damage}");

            // Use the factory to create the Item object
            IItem item = _itemFactory.CreateItem(itemDTO);

            if (item != null)
            {
                Console.WriteLine($"ItemMapper: Mapped Positie:  X={item.Position?.getX()}, Y={item.Position?.getY()}");
            }

            return item;
        }
    }
}