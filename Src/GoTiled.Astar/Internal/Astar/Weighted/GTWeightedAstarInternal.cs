using GoTiled.Core;

namespace GoTiled.Astar.Internal.Astar.Weighted;

internal static class GTWeightedAstarInternal
{
    internal static float DefaultHeuristic(GTTile origin, GTTile destination)
    {
        return Math.Abs(origin.X - destination.X) + Math.Abs(origin.Y - destination.Y);
    }

    internal static bool Calculate(GTWeightedPathMap map, GTTile origin, GTTile destination, Func<GTTile, GTTile, float> heuristic, out List<GTTile> path)
    {
        var queue = new PriorityQueue<IGTWeightedAstarNode, float>();
        queue.Enqueue(new GTWeightedAstarNode(null, origin, 0), 0);

        var visited = new HashSet<GTTile> { origin };

        while (queue.TryDequeue(out var current, out _))
        {
            if (current.Tile == destination)
            {
                path = GTAstarInternal.ToPath(current);
                return true;
            }

            foreach (var adj in map.GetConnections(current.Tile))
            {
                if (visited.Contains(adj.Tile))
                {
                    continue;
                }

                var cost = current.Cost + adj.Weight;
                queue.Enqueue(new GTWeightedAstarNode(current, adj.Tile, cost), cost + heuristic(adj.Tile, destination));
                visited.Add(adj.Tile);
            }
        }

        path = new List<GTTile>();
        return false;
    }
}