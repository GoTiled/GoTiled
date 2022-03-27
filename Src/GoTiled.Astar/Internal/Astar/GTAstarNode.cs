using GoTiled.Core;

namespace GoTiled.Astar.Internal.Astar;

internal class GTAstarNode : IGTAstarNode
{
    public IGTAstarNode? Parent { get; }
    public GTTile Tile { get; }

    public GTAstarNode(IGTAstarNode? parent, GTTile tile)
    {
        Parent = parent;
        Tile = tile;
    }
}