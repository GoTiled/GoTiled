using GoTiled.Core;
using Xunit;

namespace GoTiled.Astar.Tests;

public class GTPathMapTest
{
    private static void SanityCheck(GTPathMap map, int sizeX, int sizeY)
    {
        Assert.Equal(sizeX, map.SizeX);
        Assert.Equal(sizeY, map.SizeY);

        for (var x = 0; x < map.SizeX; x++)
        {
            for (var y = 0; y < map.SizeY; y++)
            {
                Assert.Equal(map[x, y], map.GetConnections(new GTTile(x, y)));
                Assert.Equal(map[x, y], map.GetConnections(x, y));
            }
        }
    }

    [Fact]
    public void TileMap3X3Empty()
    {
        // Arrange
        var map = new GTPathMap(3, 3);

        // Assert
        SanityCheck(map, 3, 3);
    }

    [Fact]
    public void TileMap1X1()
    {
        // Arrange
        var walkable = new[,]
        {
            { true },
        };

        // Act
        var map = new GTPathMap(walkable, false);

        // Assert
        SanityCheck(map, 1, 1);
    }

    [Fact]
    public void TileMap1X1Diagonal()
    {
        // Arrange
        var walkable = new[,]
        {
            { true },
        };

        // Act
        var map = new GTPathMap(walkable, true);

        // Assert
        SanityCheck(map, 1, 1);
    }

    [Fact]
    public void TileMap3X3()
    {
        // Arrange
        var walkable = new[,]
        {
            { true, true, true },
            { true, true, true },
            { true, true, true },
        };

        // Act
        var map = new GTPathMap(walkable, false);

        // Assert
        SanityCheck(map, 3, 3);
    }

    [Fact]
    public void TileMap3X3Diagonal()
    {
        // Arrange
        var walkable = new[,]
        {
            { true, true, true },
            { true, true, true },
            { true, true, true },
        };

        // Act
        var map = new GTPathMap(walkable, true);

        // Assert
        SanityCheck(map, 3, 3);
    }

    [Fact]
    public void AddConnection()
    {
        // Arrange
        var map = new GTPathMap(new[,]
        {
            {true,true,true},
            {true,true,true},
            {true,true,true},
        });

        // Act
        map.AddConnection(new GTTile(0, 0), new GTTile(2, 2));

        // Assert
        SanityCheck(map, 3, 3);

        Assert.Contains(new GTTile(2, 2), map.GetConnections(0, 0));
    }

    [Fact]
    public void RemoveConnection()
    {
        // Arrange
        var map = new GTPathMap(new[,]
        {
            {true,true,true},
            {true,true,true},
            {true,true,true},
        });

        // Act
        var removed = map.RemoveConnection(new GTTile(0, 0), new GTTile(0, 1));

        // Assert
        SanityCheck(map, 3, 3);

        Assert.True(removed);
        Assert.DoesNotContain(new GTTile(0, 1), map.GetConnections(0, 0));
    }
}