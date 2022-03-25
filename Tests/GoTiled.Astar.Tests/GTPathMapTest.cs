using GoTiled.Core;
using Xunit;

namespace GoTiled.Astar.Tests;

public class GTPathMapTest
{
    [Fact]
    public void SingleTileMap()
    {
        // Arrange
        var walkable = new bool[1, 1];
        walkable[0, 0] = true;

        // Act
        var map = new GTPathMap(walkable, true);

        // Assert
        Assert.Equal(1, map.SizeX);
        Assert.Equal(1, map.SizeY);

        Assert.Equal(map.GetConnections(0, 0), map.GetConnections(new GTTile(0, 0)));
    }
}