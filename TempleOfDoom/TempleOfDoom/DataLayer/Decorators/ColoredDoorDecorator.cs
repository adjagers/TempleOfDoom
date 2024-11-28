internal class ColoredDoorDecorator : IDoor
{
    private IDoor door;
    private object color;
    private object inventory;

    public ColoredDoorDecorator(IDoor door, object color, object inventory)
    {
        this.door = door;
        this.color = color;
        this.inventory = inventory;
    }

    public void SetInitialState(bool v)
    {
        Console.WriteLine("ja goed");
    }
}