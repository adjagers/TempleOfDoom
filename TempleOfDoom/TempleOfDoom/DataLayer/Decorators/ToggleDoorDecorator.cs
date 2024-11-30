internal class ToggleDoorDecorator : IDoor
{
    private IDoor door;

    public ToggleDoorDecorator(IDoor door)
    {
        this.door = door;
    }

    public void SetInitialState(bool v)
    {
        Console.WriteLine("implement the init states");
    }
}