using GoTiled.Core;

namespace GoTiled.Astar;

public static class GTAstar
{
    private class Node
    {
        public Node? Parent { get; }
        public GTTile Position { get; }

        public Node(Node? parent, GTTile position)
        {
            Parent = parent;
            Position = position;
        }
    }

    private static int Heuristic(GTTile origin, GTTile destination)
    {
        return Math.Abs(origin.X - destination.X) + Math.Abs(origin.Y - destination.Y);
    }

    public static List<GTTile>? Calculate(GTPathMap map, GTTile origin, GTTile destination)
    {
        var queue = new PriorityQueue<Node, int>();
        queue.Enqueue(new Node(null, origin), 0);

        var visited = new HashSet<GTTile> { origin };

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var adj in map.GetConnections(current.Position))
            {
                if (adj == destination)
                {
                    var tiles = new List<GTTile> { adj };

                    var node = current;
                    while (node != null)
                    {
                        tiles.Add(node.Position);
                        node = node.Parent;
                    }

                    tiles.Reverse();

                    return tiles;
                }

                if (visited.Contains(adj))
                {
                    continue;
                }

                queue.Enqueue(new Node(current, adj), Heuristic(adj, destination));
                visited.Add(adj);
            }
        }

        return null;
    }
}