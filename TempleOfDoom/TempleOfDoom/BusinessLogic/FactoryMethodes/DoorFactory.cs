﻿using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.BusinessLogic.Models.Doors;
using TempleOfDoom.DataLayer.DTO;

namespace TempleOfDoom.BusinessLogic.FactoryMethodes;

public class DoorFactory
{
    public IDoor CreateDoor(List<DoorDTO> dtoDoors)
    {
        IDoor door = new BasicDoor(initialState: false);

        foreach (DoorDTO dtoDoor in dtoDoors)
        {
            switch (dtoDoor.Type.ToLower())
            {
                case "colored":
                    // Use a default color if no color is provided
                    Color keyColor = dtoDoor.Color != null
                        ? Enum.Parse<Color>(dtoDoor.Color, true)
                        : Color.Blue; // Default color, change as needed
                    door = new ColoredDoorDecorator(door, keyColor);
                    break;
                case "toggle":
                    door = new ToggleDoorDecorator(door);
                    break;
                case "closing gate":
                    door = new ClosingGateDecorator(door);
                    break;
                case "open on odd":
                    door = new OpenOnOddDecorator(door);
                    break;
                case "open on stones in room":
                    door = new NumberOfStonesRoomDoorDecorator(door, dtoDoor.No_of_stones);
                    break;
                default:
                    Console.WriteLine("Warning: Unrecognized door type.");
                    break;
            }
        }

        return door;
    }
}