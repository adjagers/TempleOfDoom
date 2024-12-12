using System;
using System.Collections.Generic;
using TempleOfDoom.DataLayer.Decorators;
using TempleOfDoom.DataLayer.Models.Items;
using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.PresentationLayer
{
    public class CharacterFactory
    {
        // Mapping dictionaries
        private static readonly Dictionary<Type, char> ItemCharacterMap = new()
        {
            { typeof(SankaraStone), 'S' },
            { typeof(Key), 'K' },
            { typeof(PressurePlate), 'P' },
            { typeof(Boobytrap), 'B' },
            { typeof(DisappearingBoobytrap), 'D' }
        };

        private static readonly Dictionary<Type, char> DoorCharacterMap = new()
        {
            { typeof(ClosingGateDecorator), '|' },
            { typeof(ColoredDoorDecorator), '/' }
        };

        private static readonly char DefaultWallCharacter = '#';
        private static readonly char DefaultEmptyCharacter = ' ';

        public char GetDisplayCharacter(object obj)
        {
            if (obj is IItem item)
            {
                return GetItemCharacter(item);
            }
            else if (obj is IDoor door)
            {
                return GetDoorCharacter(door);
            }

            // Fallback for unknown objects
            return DefaultEmptyCharacter;
        }

        private char GetItemCharacter(IItem item)
        {
            return ItemCharacterMap.TryGetValue(item.GetType(), out char character) 
                ? character 
                : DefaultEmptyCharacter; // Fallback for unknown items
        }

        private char GetDoorCharacter(IDoor door)
        {
            return DoorCharacterMap.TryGetValue(door.GetType(), out char character) 
                ? character 
                : DefaultEmptyCharacter; // Fallback for unknown doors
        }
    }
}