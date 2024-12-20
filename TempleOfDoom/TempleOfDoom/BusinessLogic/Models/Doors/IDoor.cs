using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.Interfaces;


public interface IDoor : IGameObject
{
    void OpenDoor();
    void CloseDoor();
    void SetInitialState(bool isOpen);
    public bool GetState();
    public void Interact(Player player);
}