using TempleOfDoom.Interfaces;


public interface IDoor : IGameObject
{
    bool IsOpen { get; set; }
    void Open();
    void Close();
}