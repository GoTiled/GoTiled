using GoTiled.Core;
using Xunit;

namespace GoTiled.Map.Tests;

public class GTMapTest
{
    private enum Tile
    {
        Ground,
        Wall,
    }


    private static void SanityCheck(GTMap<Tile> map, int sizeX, int sizeY)
    {
        Assert.Equal(sizeX, map.SizeX);
        Assert.Equal(sizeY, map.SizeY);

        for (var x = 0; x < map.SizeX; x++)
        {
            for (var y = 0; y < map.SizeY; y++)
            {
                Assert.Equal(map[x, y], map.Get(new GTTile(x, y)));
                Assert.Equal(map[x, y], map.Get(x, y));
            }
        }
    }

    [Fact]
    public void TileInequality()
    {
        // Arrange
        var tiles = new[,]
        {
            { Tile.Ground, Tile.Ground, Tile.Ground },
            { Tile.Ground, Tile.Wall, Tile.Ground },
            { Tile.Ground, Tile.Ground, Tile.Ground },
        };

        // Act
        var map = new GTMap<Tile>(tiles);

        // Assert
        SanityCheck(map, 3, 3);

        Assert.Equal(Tile.Ground, map[0, 0]);
        Assert.Equal(Tile.Wall, map[1, 1]);
        Assert.Equal(Tile.Ground, map[2, 2]);
    }
}