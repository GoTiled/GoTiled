using Xunit;
using GoTiled.Utils.Collections.Array;

namespace GoTiled.Utils.Tests.Collections.Array;

public class Array2DExtensionsTest
{
    [Fact]
    public void InsideArray()
    {
        // Arrange
        var array = new[,]
        {
            {10, 10, 10},
            {10, 10, 10},
            {10, 10, 10},
        };

        // Act
        var value = array.GetValueOrDefault(0, 0, 1);

        // Assert
        Assert.Equal(10, value);
    }

    [Fact]
    public void OutsideArray()
    {
        // Arrange
        var array = new[,]
        {
            {10, 10, 10},
            {10, 10, 10},
            {10, 10, 10},
        };

        // Act
        var value = array.GetValueOrDefault(4, 0, 1);

        // Assert
        Assert.Equal(1, value);
    }
}