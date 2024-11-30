internal class ClosingGateDecorator : IDoor
{
    private IDoor door;

    public ClosingGateDecorator(IDoor door)
    {
        this.door = door;
    }

    public void SetInitialState(bool v)
    {
        Console.WriteLine("ja");
    }
}