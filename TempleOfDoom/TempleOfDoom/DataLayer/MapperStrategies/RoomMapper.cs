using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.MapperStrategies
{
    public class RoomMapper : IMapper
    {
        private readonly ItemMapper _itemMapper;

        public RoomMapper(ItemMapper itemMapper)
        {
            _itemMapper = itemMapper;
        }

        public IGameObject Map(IDTO dto)
        {
            if (dto == null)
            {
                Console.WriteLine("RoomMapper: Received a null DTO.");
                return null;
            }

            RoomDTO roomDTO = dto as RoomDTO;
            if (roomDTO == null)
            {
                throw new InvalidCastException("RoomMapper: Invalid DTO type for RoomMapper.");
            }

            Console.WriteLine($"RoomMapper: Mapping RoomDTO with Id={roomDTO.Id}, Width={roomDTO.Width}, Height={roomDTO.Height}");

            // Map basic properties
            Room room = new Room
            {
                Width = roomDTO.Width,
                Height = roomDTO.Height
            };

            // Map items if present
            if (roomDTO.Items != null && roomDTO.Items.Count > 0)
            {
                room.Items = MapItems(roomDTO.Items);
            }
            else
            {
                Console.WriteLine($"RoomMapper: RoomDTO with Id={roomDTO.Id} has no items.");
                room.Items = new List<IItem>();
            }

            return room;
        }

        private List<IItem> MapItems(List<ItemDTO> itemDTOs)
        {
            List<IItem> items = new List<IItem>();
            foreach (ItemDTO itemDTO in itemDTOs)
            {
                items.Add(_itemMapper.Map(itemDTO));
            }
            return items;
        }
    }
}
