using NUnit.Framework;
using Moq;
using TempleOfDoom.DataLayer.Decorators;

namespace UnitTests
{
    [TestFixture]
    public class DoorTests
    {
        private Mock<IDoor> _mockDoor;
        private ClosingGateDecorator _closingGateDecorator;

        [SetUp]
        public void SetUp()
        {
            _mockDoor = new Mock<IDoor>();
            _closingGateDecorator = new ClosingGateDecorator(_mockDoor.Object);
        }

        [Test]
        public void ClosingGate_ShouldClosePermanently()
        {
            // Arrange: Ensure door starts open
            _mockDoor.Object.Open();

            // Act: Close via the decorator
            _closingGateDecorator.Close();

            // Assert: Verify permanent closure
            Assert.That(_mockDoor.Object.IsOpen, Is.False);
            _mockDoor.Verify(d => d.Close(), Times.Once);
        }

        [Test]
        public void ClosingGate_ShouldNotReopenWhenClosedPermanently()
        {
            // Arrange: Allow the mock to have a real backing field for IsOpen
            _mockDoor.SetupProperty(d => d.IsOpen, true); // Default state is open

            // Act: Close the gate permanently
            _closingGateDecorator.Close();

            // Assert: Ensure the door remains closed
            Assert.That(_mockDoor.Object.Close, Is.True, "The door should remain closed after being permanently closed.");

            // Act: Try to reopen the door
            _closingGateDecorator.Open();

            // Assert: Ensure the door cannot be reopened
            Assert.That(_mockDoor.Object.IsOpen, Is.False, "The door should remain closed and IsOpen should return false.");
        }


    }
}