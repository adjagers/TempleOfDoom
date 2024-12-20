﻿using System;
using TempleOfDoom.DataLayer.Models;

namespace TempleOfDoom.BusinessLogic.Models.Doors
{
    public class OpenOnOddDecorator : DoorDecorator
    {

        public OpenOnOddDecorator(IDoor door) : base(door)
        {
        }
        public override void Interact(Player player)
        {
            if (player.NumberOfLivesIsOdd()) base.OpenDoor();
        }
    }
}
