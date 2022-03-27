namespace GoTiled.Astar.Internal.Astar.Weighted;

internal interface IGTWeightedAstarNode : IGTAstarNode
{
    float Cost { get; }
}