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
                Id = roomDTO.Id,
                Width = roomDTO.Width,
                Height = roomDTO.Height
            };

            // Map items if present
            if (roomDTO.Items != null && roomDTO.Items.Count > 0)
            {
                Console.WriteLine($"RoomMapper: RoomDTO with Id={roomDTO.Id} contains {roomDTO.Items.Count} items.");
                room.Items = MapItems(roomDTO.Items);
            }
            else
            {
                Console.WriteLine($"RoomMapper: RoomDTO with Id={roomDTO.Id} has no items.");
                room.Items = new List<Item>();
            }

            return room;
        }

        private List<Item> MapItems(List<ItemDTO> itemDTOs)
        {
            List<Item> items = new List<Item>();
            foreach (ItemDTO itemDTO in itemDTOs)
            {
                Console.WriteLine($"RoomMapper: Mapping ItemDTO with damage={itemDTO.Damage}");
                items.Add((Item)_itemMapper.Map(itemDTO));
            }
            return items;
        }
    }
}
