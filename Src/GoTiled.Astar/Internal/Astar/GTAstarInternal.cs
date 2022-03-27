using GoTiled.Core;

namespace GoTiled.Astar.Internal.Astar;

internal static class GTAstarInternal
{
    internal static float DefaultHeuristic(GTTile origin, GTTile destination)
    {
        return Math.Abs(origin.X - destination.X) + Math.Abs(origin.Y - destination.Y);
    }

    internal static bool Calculate(GTPathMap map, GTTile origin, GTTile destination, Func<GTTile, GTTile, float> heuristic, out List<GTTile> path)
    {
        var queue = new PriorityQueue<IGTAstarNode, float>();
        queue.Enqueue(new GTAstarNode(null, origin), 0);

        var visited = new HashSet<GTTile> { origin };

        while (queue.TryDequeue(out var current, out _))
        {
            if (current.Tile == destination)
            {
                path = ToPath(current);
                return true;
            }

            foreach (var adj in map.GetConnections(current.Tile))
            {
                if (visited.Contains(adj))
                {
                    continue;
                }

                queue.Enqueue(new GTAstarNode(current, adj), heuristic(adj, destination));
                visited.Add(adj);
            }
        }

        path = new List<GTTile>();
        return false;
    }

    internal static List<GTTile> ToPath(IGTAstarNode lastNode)
    {
        var tiles = new List<GTTile>();

        var node = lastNode;
        while (node != null)
        {
            tiles.Add(node.Tile);
            node = node.Parent;
        }

        tiles.Reverse();

        return tiles;
    }
}