using System;
using FluentAssertions;
using PaintStore.Models;

namespace PaintStore.Tests;

public class OrderModelTests
{
    [Fact]

    public void TotalPrice_WhenNoPaintProducts_ReturnsZero()
    {
        // Arrange
        var order = new Order();
        // Assert
        order.TotalPrice.Should().Be(0);
    }
    [Fact]
    public void TotalPrice_WhenOnePaintProduct_ReturnsPriceOfThatProduct()
    {
        // Arrange
        var order = new Order();
        var paintProduct = new PaintProduct("Red Paint", 19.99m);
        order.PaintProducts.Add(paintProduct);
        // Assert
        order.TotalPrice.Should().Be(19.99m);
    }
}
