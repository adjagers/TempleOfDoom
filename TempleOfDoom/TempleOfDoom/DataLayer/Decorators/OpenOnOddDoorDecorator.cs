using TempleOfDoom.DataLayer.Models;

internal class OpenOnOddDoorDecorator : IDoor
{
    private IDoor door;
    private Player player;

    public OpenOnOddDoorDecorator(IDoor door, Player player)
    {
        this.door = door;
        this.player = player;
    }

    public void SetInitialState(bool v)
    {
        Console.WriteLine("ja goed");
    }
}