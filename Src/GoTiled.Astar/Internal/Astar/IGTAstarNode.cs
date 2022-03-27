using GoTiled.Core;

namespace GoTiled.Astar.Internal.Astar;

internal interface IGTAstarNode
{
    IGTAstarNode? Parent { get; }
    GTTile Tile { get; }
}