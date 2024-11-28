internal class NumberOfStonesInRoomDoorDecorator : IDoor
{
    private IDoor door;
    private object no_of_stones;

    public NumberOfStonesInRoomDoorDecorator(IDoor door, object no_of_stones)
    {
        this.door = door;
        this.no_of_stones = no_of_stones;
    }

    public void SetInitialState(bool v)
    {
        Console.WriteLine("ja goed");
    }
}