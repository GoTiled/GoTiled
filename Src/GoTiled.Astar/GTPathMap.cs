using GoTiled.Core;
using GoTiled.Utils.Collections.Array;

namespace GoTiled.Astar;

public class GTPathMap
{
    private readonly List<GTTile>[,] _nodes;

    private static void AddDiagonalConnections(bool[,] walkable, ICollection<GTTile> connections, int x, int y)
    {
        if (walkable.GetValueOrDefault(x - 1, y - 1, false))
        {
            connections.Add(new GTTile(x - 1, y - 1));
        }

        if (walkable.GetValueOrDefault(x + 1, y - 1, false))
        {
            connections.Add(new GTTile(x + 1, y - 1));
        }

        if (walkable.GetValueOrDefault(x - 1, y + 1, false))
        {
            connections.Add(new GTTile(x - 1, y + 1));
        }

        if (walkable.GetValueOrDefault(x + 1, y + 1, false))
        {
            connections.Add(new GTTile(x + 1, y + 1));
        }
    }

    private static void AddStraightConnections(bool[,] walkable, ICollection<GTTile> connections, int x, int y)
    {
        if (walkable.GetValueOrDefault(x, y - 1, false))
        {
            connections.Add(new GTTile(x, y - 1));
        }
        if (walkable.GetValueOrDefault(x - 1, y, false))
        {
            connections.Add(new GTTile(x - 1, y));
        }
        if (walkable.GetValueOrDefault(x + 1, y, false))
        {
            connections.Add(new GTTile(x + 1, y));
        }
        if (walkable.GetValueOrDefault(x, y + 1, false))
        {
            connections.Add(new GTTile(x, y + 1));
        }
    }

    public GTPathMap(int sizeX, int sizeY)
    {
        _nodes = new List<GTTile>[sizeX, sizeY];

        for (var x = 0; x < sizeX; x++)
        {
            for (var y = 0; y < sizeY; y++)
            {
                var connections = new List<GTTile>();
                _nodes[x, y] = connections;
            }
        }
    }

    public GTPathMap(bool[,] walkable) : this(walkable, false)
    { }

    public GTPathMap(bool[,] walkable, bool diagonal)
    {
        _nodes = new List<GTTile>[walkable.GetLength(0), walkable.GetLength(1)];

        for (var x = 0; x < walkable.GetLength(0); x++)
        {
            for (var y = 0; y < walkable.GetLength(1); y++)
            {
                if (!walkable[x, y])
                    continue;

                var connections = new List<GTTile>();
                _nodes[x, y] = connections;

                // Diagonal
                if (diagonal) AddDiagonalConnections(walkable, connections, x, y);
                AddStraightConnections(walkable, connections, x, y);
            }
        }
    }

    public int SizeX => _nodes.GetLength(0);
    public int SizeY => _nodes.GetLength(1);

    public IReadOnlyList<GTTile> GetConnections(GTTile tile)
    {
        return _nodes[tile.X, tile.Y];
    }

    public IReadOnlyList<GTTile> GetConnections(int x, int y)
    {
        return _nodes[x, y];
    }

    public IReadOnlyList<GTTile> this[int x, int y] => _nodes[x, y];

    public void AddConnection(GTTile origin, GTTile destination)
    {
        _nodes[origin.X, origin.Y].Add(destination);
    }

    public bool RemoveConnection(GTTile origin, GTTile destination)
    {
        return _nodes[origin.X, origin.Y].Remove(destination);
    }
}