using GoTiled.Core;

namespace GoTiled.Astar;

public class GTWeightedConnection
{
    public GTTile Tile { get; }
    public float Weight { get; }

    public GTWeightedConnection(GTTile tile, float weight)
    {
        Tile = tile;
        Weight = weight;
    }
}