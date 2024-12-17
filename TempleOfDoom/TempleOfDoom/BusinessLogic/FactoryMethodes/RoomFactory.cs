using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.BusinessLogic.FactoryMethodes
{
    public class RoomFactory : IFactory
    {
        private readonly ItemFactory _itemFactory;


        public RoomFactory() {
         _itemFactory = new ItemFactory();
        }

        public IGameObject Create(IDTO dto)
        {
            RoomDTO roomDTO = dto as RoomDTO;


            Room room = new Room
            {
                Dimensions = new Dimensions(roomDTO.Width, roomDTO.Height),
                Items = MapItems(roomDTO.Items)
            };

            return room;
        }

        private List<IItem> MapItems(IEnumerable<ItemDTO> itemDTOs)
        {
            return itemDTOs?.Select(itemDTO => _itemFactory.CreateItem(itemDTO)).ToList<IItem>() ?? new List<IItem>();
        }


    }
}