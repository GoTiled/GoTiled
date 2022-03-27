using GoTiled.Core;

namespace GoTiled.Astar.Internal.Astar;

internal static class GTWeightedAstarInternal
{
    private sealed class Node
    {
        public Node? Parent { get; }
        public GTTile Tile { get; }

        public Node(Node? parent, GTTile tile)
        {
            Parent = parent;
            Tile = tile;
        }
    }

    internal static float DefaultHeuristic(GTTile origin, GTTile destination)
    {
        return Math.Abs(origin.X - destination.X) + Math.Abs(origin.Y - destination.Y);
    }

    internal static bool Calculate(GTPathMap map, GTTile origin, GTTile destination, Func<GTTile, GTTile, float> heuristic, out List<GTTile> path)
    {
        var queue = new PriorityQueue<Node, float>();
        queue.Enqueue(new Node(null, origin), 0);

        var visited = new HashSet<GTTile> { origin };

        while (queue.TryDequeue(out var current, out _))
        {
            if (current.Tile == destination)
            {
                var tiles = new List<GTTile>();

                var node = current;
                while (node != null)
                {
                    tiles.Add(node.Tile);
                    node = node.Parent;
                }

                tiles.Reverse();

                path = tiles;
                return true;
            }

            foreach (var adj in map.GetConnections(current.Tile))
            {
                if (visited.Contains(adj))
                {
                    continue;
                }

                queue.Enqueue(new Node(current, adj), heuristic(adj, destination));
                visited.Add(adj);
            }
        }

        path = new List<GTTile>();
        return false;
    }
}