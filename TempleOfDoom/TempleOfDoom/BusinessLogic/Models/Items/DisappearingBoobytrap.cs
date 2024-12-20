﻿using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.Models.Items;

public class DisappearingBoobytrap : Boobytrap
{
    public DisappearingBoobytrap(Position position, int damage) : base(position, damage)
    {
        
    }
    // overrides BoobyTrap Interaction
    public override void Interact(Player player)
    {
        base.Interact(player);
        Position = null;
    }
    
    
}