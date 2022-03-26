using GoTiled.Core;

namespace GoTiled.Astar;

public class GTWeightedConnection<TWeight>
{
    public GTTile Tile { get; }
    public TWeight Weight { get; }

    public GTWeightedConnection(GTTile tile, TWeight weight)
    {
        Tile = tile;
        Weight = weight;
    }
}