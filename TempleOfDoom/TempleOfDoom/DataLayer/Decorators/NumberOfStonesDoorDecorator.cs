namespace TempleOfDoom.DataLayer.Decorators
{
    public class NumberOfStonesDoorDecorator : DoorDecorator
    {
        private readonly int _requiredStones;
        private int no_of_stones;

        public NumberOfStonesDoorDecorator(IDoor door, int no_of_stones) : base(door)
        {
            this.no_of_stones = no_of_stones;
        }

        public NumberOfStonesDoorDecorator(IDoor door, int requiredStones, Func<int> getStonesInRoom)
            : base(door)
        {
            _requiredStones = requiredStones;
        }

        public override void Open()
        {
            if (HasExactNumberOfStones())
            {
                base.Open();
                Console.WriteLine($"The door opens because the room contains {_requiredStones} stones.");
            }
            else
            {
                Console.WriteLine($"The door remains closed. The room must contain exactly {_requiredStones} stones.");
            }
        }

        private bool HasExactNumberOfStones()
        {
            Console.WriteLine("IMPLEMENT NUMBER OF STONES");
            return false;
        }
    }
}
