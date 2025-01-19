﻿using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.BusinessLogic.HelperClasses;
using TempleOfDoom.BusinessLogic.Models;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.Models.Items;

public class Key : IItem
{
    public Position? Position { get; set; }
    public Color Color { get; }

    public Key(Position position, Color color)
    {
        Position = position;
        Color = color;
    }
    public void Interact(Player player)
    {
        player.AddItemInventory(this);
        Position = null;
    }
}