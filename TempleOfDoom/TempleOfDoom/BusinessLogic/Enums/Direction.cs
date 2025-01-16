using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.BusinessLogic.Enums
{
    
    
    public enum Direction
    {
        NORTH,
        SOUTH,
        EAST,
        WEST  
        
        
        
    }
    public class DirectionHelper
    {
        public Direction GetReverseDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.NORTH:
                    return Direction.SOUTH;
                case Direction.SOUTH:
                    return Direction.NORTH;
                case Direction.EAST:
                    return Direction.WEST;
                case Direction.WEST:
                    return Direction.EAST;
                default:
                    throw new ArgumentException("Invalid direction");
            }
        }
    }

    
    

}
