using TempleOfDoom.HelperClasses;
using TempleOfDoom.Enums;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.DataLayer.DTO;
using System;
using TempleOfDoom.DataLayer.Models.Items;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.FactoryMethodes
{
    public class ItemFactory
    {
        public IItem CreateItem(ItemDTO itemDTO)
        {
            switch (itemDTO.Type)
            {
                case "key":
                    Color keyColor =
                        Enum.Parse<Color>(itemDTO.Color ?? throw new InvalidDataException("A key needs a color value"),
                            true);
                    return new Key(new Position(itemDTO.X, itemDTO.Y), keyColor);
                case "boobytrap":
                    return new Boobytrap(new Position(itemDTO.X, itemDTO.Y),
                        itemDTO.Damage ?? throw new InvalidDataException("A boobytrap needs a damage value"));
                case "disappearing boobytrap":
                    return new DisappearingBoobytrap(new Position(itemDTO.X, itemDTO.Y),
                        itemDTO.Damage ?? throw new InvalidDataException("A disappearing boobytrap needs a damage value"));
                case "sankara stone":
                    return new SankaraStone(new Position(itemDTO.X, itemDTO.Y));
                case "pressure plate":
                    return new PressurePlate(new Position(itemDTO.X, itemDTO.Y));
                default:
                    throw new ArgumentException("Invalid item type");
            }
        }
    }

   
}