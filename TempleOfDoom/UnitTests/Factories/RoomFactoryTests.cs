using NUnit.Framework;
using TempleOfDoom.BusinessLogic.FactoryMethodes;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.Models;

namespace UnitTests.Factories
{
    [TestFixture]
    public class RoomFactoryTests
    {
        private RoomFactory _roomFactory;

        [SetUp]
        public void SetUp()
        {
            _roomFactory = new RoomFactory();
        }

        [Test]
        public void Create_ValidRoomDTO_ReturnsRoom()
        {
            // Arrange
            var roomDTO = new RoomDTO
            {
                Width = 10,
                Height = 15,
                Items = new List<ItemDTO>
                {
                    new ItemDTO { Type = "key", Color = "green" },
                    new ItemDTO { Type = "sankara stone" }
                }
            };

            // Act
            var result = _roomFactory.Create(roomDTO) as Room;

            // Assert
            Assert.That(result, Is.Not.Null); // Verify the result is not null
            Assert.Equals(10, result.Dimensions.getHeight()); // Verify the width
            Assert.Equals(15, result.Dimensions.getWidth()); // Verify the height
            Assert.Equals(2, result.Items.Count); // Verify the number of items
        }
    }
}