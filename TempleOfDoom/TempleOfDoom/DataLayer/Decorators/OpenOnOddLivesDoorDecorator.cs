using System;

namespace TempleOfDoom.DataLayer.Decorators
{
    public class OpenOnOddDecorator : DoorDecorator
    {

        public OpenOnOddDecorator(IDoor door) : base(door)
        {
        }

        public override void Open()
        {
            if (PlayerHasOddLives())
            {
                base.Open();
                Console.WriteLine("The door is open because you have an odd number of lives.");
            }
            else
            {
                Console.WriteLine("The door remains closed. You need an odd number of lives to open it.");
            }
        }

        private bool PlayerHasOddLives()
        {
            return false;
        }
    }
}
