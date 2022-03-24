using System;
using GoTiled.Core;
using Xunit;

namespace GoTiled.Astar.Tests;

public class GTAstarTest
{
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
        var path = GTAstar.Calculate(map, new GTTile(0, 0), new GTTile(0, 2)) ?? throw new ArgumentException();

        // Assert
        Assert.Equal(3, path.Count);
        Assert.Equal(new GTTile(0, 0), path[0]);
        Assert.Equal(new GTTile(0, 1), path[1]);
        Assert.Equal(new GTTile(0, 2), path[2]);
    }
}