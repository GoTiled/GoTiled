using GoTiled.Core;

namespace GoTiled.Astar.Internal.Astar.Weighted;

internal class GTWeightedAstarNode : IGTWeightedAstarNode
{
    public IGTAstarNode? Parent { get; }
    public GTTile Tile { get; }
    public float Cost { get; }

    public GTWeightedAstarNode(IGTAstarNode? parent, GTTile tile, float cost)
    {
        Parent = parent;
        Tile = tile;
        Cost = cost;
    }
}