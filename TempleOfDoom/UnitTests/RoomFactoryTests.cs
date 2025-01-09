/*using NUnit.Framework;
using TempleOfDoom.BusinessLogic.Enums;
using TempleOfDoom.BusinessLogic.FactoryMethodes;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;
using TempleOfDoom.HelperClasses;

[TestFixture]
public class RoomFactoryTests
{
    [Test]
    public void CreateRoomConnectionsWithDoors_ShouldCreateConnectionsCorrectly()
    {
        // Arrange
        var roomDict = new Dictionary<int, Room>
        {
            { 1, new Room { Dimensions = new Dimensions(10, 10) } },
            { 2, new Room { Dimensions = new Dimensions(12, 12) } },
            { 3, new Room { Dimensions = new Dimensions(8, 8) } }
        };

        var connectionDtos = new List<ConnectionDTO>
        {
            new ConnectionDTO
            {
                NORTH = 1,
                SOUTH = 2,
                EAST = 3,
                WEST = 2,
                Doors = new List<DoorDTO> { new DoorDTO { Type = "colored" } }
            }
        };

        var roomFactory = new RoomFactory();

        // Act
        var connections = roomFactory.CreateRoomConnectionsWithDoors(roomDict, connectionDtos);


        // Assert
        Assert.Equals(connections.Count, Is.EqualTo(3)); // Three connections should be created
        Assert.That(roomDict[1].AdjacentRooms.ContainsKey(Direction.SOUTH), Is.True);
        Assert.That(roomDict[2].AdjacentRooms.ContainsKey(Direction.NORTH), Is.True);
        Assert.That(roomDict[1].AdjacentRooms.ContainsKey(Direction.EAST), Is.False);
    }
}*/

