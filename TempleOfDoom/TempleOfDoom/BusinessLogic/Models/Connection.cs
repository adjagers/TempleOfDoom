using TempleOfDoom.DataLayer.Models;


namespace TempleOfDoom.BusinessLogic.Models
{
    public class Connection(Room connectedRoom, ITransition transition)
    {
        public ITransition Transition { get; } = transition;
        public Room ConnectedRoom { get; } = connectedRoom;
    }
}
