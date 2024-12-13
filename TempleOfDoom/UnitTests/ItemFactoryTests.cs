using System;
using NUnit.Framework;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.DataLayer.FactoryMethodes;
using TempleOfDoom.DataLayer.Models.Items;
using TempleOfDoom.Enums;


[TestFixture]
public class ItemFactoryTests
{
    private ItemFactory _itemFactory;

    [SetUp]
    public void SetUp()
    {
        _itemFactory = new ItemFactory();
    }

    [Test]
    public void CreateItem_Key_ReturnsCorrectKey()
    {
        // Arrange
        var itemDTO = new ItemDTO { Type = "key", X = 1, Y = 2, Color = "Red" };

        // Act
        var item = _itemFactory.CreateItem(itemDTO);

        // Assert
        Assert.That(item, Is.InstanceOf<Key>());
        var key = (Key)item;
        Assert.That(key.Position.getX(), Is.EqualTo(1), "Expected X coordinate is incorrect.");
        Assert.That(key.Position.getY(), Is.EqualTo(2), "Expected Y coordinate is incorrect.");
        Assert.That(key.Color, Is.EqualTo(Color.Red), "Expected Color is incorrect.");
    }

    [Test]
    public void CreateItem_Boobytrap_ThrowsExceptionWhenDamageIsNull()
    {
        // Arrange
        var itemDTO = new ItemDTO { Type = "boobytrap", X = 1, Y = 2, Damage = null };

        // Act & Assert
        Assert.Throws<InvalidDataException>(() => _itemFactory.CreateItem(itemDTO));
    }

    [Test]
    public void CreateItem_InvalidType_ThrowsArgumentException()
    {
        // Arrange
        var itemDTO = new ItemDTO { Type = "invalid", X = 0, Y = 0 };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _itemFactory.CreateItem(itemDTO));
    }

    [Test]
    public void CreateItem_NullColorForKey_ThrowsException()
    {
        // Arrange
        var itemDTO = new ItemDTO { Type = "key", X = 1, Y = 1, Color = null };

        // Act & Assert
        Assert.Throws<InvalidDataException>(() => _itemFactory.CreateItem(itemDTO));
    }

    [Test]
    public void CreateItem_DisappearingBoobytrap_ReturnsCorrectItem()
    {
        // Arrange
        var itemDTO = new ItemDTO { Type = "disappearing boobytrap", X = 3, Y = 3, Damage = 10 };

        // Act
        var item = _itemFactory.CreateItem(itemDTO);

        // Assert
        Assert.That(item, Is.InstanceOf<DisappearingBoobytrap>());
        var boobytrap = (DisappearingBoobytrap)item;
        Assert.That(boobytrap.Position.getX(), Is.EqualTo(3), "Expected X coordinate is incorrect.");
        Assert.That(boobytrap.Position.getY(), Is.EqualTo(3), "Expected Y coordinate is incorrect.");
        Assert.That(boobytrap._damage, Is.EqualTo(10), "Expected Damage is incorrect.");
    }

    [Test]
    public void CreateItem_SankaraStone_ReturnsCorrectItem()
    {
        // Arrange
        var itemDTO = new ItemDTO { Type = "sankara stone", X = 4, Y = 4 };

        // Act
        var item = _itemFactory.CreateItem(itemDTO);

        // Assert
        Assert.That(item, Is.InstanceOf<SankaraStone>());
        var stone = (SankaraStone)item;
        Assert.That(stone.Position.getX(), Is.EqualTo(4), "Expected X coordinate is incorrect.");
        Assert.That(stone.Position.getY(), Is.EqualTo(4), "Expected Y coordinate is incorrect.");
    }
}
