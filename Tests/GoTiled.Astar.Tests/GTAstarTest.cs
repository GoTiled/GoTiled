using GoTiled.Core;
using Xunit;

namespace GoTiled.Astar.Tests;

public class GTAstarTest
{
    [Fact]
    public void PointPath()
    {
        // Arrange
        var map = new GTPathMap(new[,]
        {
            {true,true,true},
            {true,true,true},
            {true,true,true},
        });

        // Act
        var complete = GTAstar.Calculate(map, 0, 0, 0, 0, out var path);

        // Assert
        Assert.True(complete);
        Assert.Single(path);
        Assert.Equal(new GTTile(0, 0), path[0]);
    }

    [Fact]
    public void AdjacentPath()
    {
        // Arrange
        var map = new GTPathMap(new[,]
        {
            {true,true,true},
            {true,true,true},
            {true,true,true},
        });

        // Act
        var complete = GTAstar.Calculate(map, 0, 0, 0, 1, out var path);

        // Assert
        Assert.True(complete);
        Assert.Equal(2, path.Count);
        Assert.Equal(new GTTile(0, 0), path[0]);
        Assert.Equal(new GTTile(0, 1), path[1]);
    }

    [Fact]
    public void StraightPath()
    {
        // Arrange
        var map = new GTPathMap(new[,]
        {
            {true,true,true},
            {true,true,true},
            {true,true,true},
        });

        // Act
        var complete = GTAstar.Calculate(map, 0, 0, 0, 2, out var path);

        // Assert
        Assert.True(complete);
        Assert.Equal(3, path.Count);
        Assert.Equal(new GTTile(0, 0), path[0]);
        Assert.Equal(new GTTile(0, 1), path[1]);
        Assert.Equal(new GTTile(0, 2), path[2]);
    }

    [Fact]
    public void BlockedPath()
    {
        // Arrange
        var map = new GTPathMap(new[,]
        {
            {true,false,true},
            {true,false,true},
            {true,false,true},
        });

        // Act
        var complete = GTAstar.Calculate(map, 0, 0, 0, 2, out var path);

        // Assert
        Assert.False(complete);
        Assert.Empty(path);
    }
}