using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.BusinessLogic.FactoryMethodes
{
    public class RoomFactory
    {
        public IGameObject Create(IDTO dto)
        {
            if (dto is not RoomDTO roomDTO)
                throw new ArgumentException("Invalid DTO type provided for Room creation.", nameof(dto));

            ItemFactory itemFactory = new();

            Room room = new Room
            {
                
                Dimensions = new Dimensions(roomDTO.Width, roomDTO.Height),
                Items = roomDTO.Items?
                            .Select(itemDTO => itemFactory.CreateItem(itemDTO))
                            .ToList<IItem>() 
                        ?? new List<IItem>()
            };

            return room;
        }

        public void addConnectionsToRoom()
        {
        }
        
        
    }
}