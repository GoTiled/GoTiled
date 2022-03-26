using Xunit;

namespace GoTiled.Core.Tests;

public class GTTileTest
{
    private static void SanityCheck(GTTile tile, int x, int y)
    {
        Assert.Equal(x, tile.X);
        Assert.Equal(y, tile.Y);

        Assert.False(tile.Equals(null));

        Assert.Equal($"[{x}, {y}]", tile.ToString());
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(0, 2)]
    [InlineData(1, 0)]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 0)]
    [InlineData(2, 1)]
    [InlineData(2, 2)]
    public void TileSanity(int x, int y)
    {
        // Arrange
        var tile = new GTTile(x, y);

        // Assert
        SanityCheck(tile, x, y);
    }

    [Fact]
    public void TileInequality()
    {
        // Arrange
        var tileA = new GTTile(0, 0);
        var tileB = new GTTile(0, 1);

        // Assert
        Assert.True(tileA != tileB);
    }

    [Fact]
    public void ObjectComparisonEquality()
    {
        // Arrange
        object tileA = new GTTile(0, 0);
        object tileB = new GTTile(0, 0);

        // Assert
        Assert.True(tileA.Equals(tileB));
    }

    [Fact]
    public void ObjectComparisonInequality()
    {
        // Arrange
        object tile = new GTTile(0, 0);

        // Assert
        Assert.False(tile.Equals(0));
    }
}