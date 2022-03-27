using GoTiled.Core;

namespace GoTiled.Astar;

public class GTWeightedPathMap
{
    private readonly List<GTWeightedConnection>[,] _nodes;

    public GTWeightedPathMap(int sizeX, int sizeY)
    {
        _nodes = new List<GTWeightedConnection>[sizeX, sizeY];

        for (var x = 0; x < sizeX; x++)
        {
            for (var y = 0; y < sizeY; y++)
            {
                var connections = new List<GTWeightedConnection>();
                _nodes[x, y] = connections;
            }
        }
    }

    public int SizeX => _nodes.GetLength(0);
    public int SizeY => _nodes.GetLength(1);

    public IReadOnlyList<GTWeightedConnection> GetConnections(GTTile tile)
    {
        return _nodes[tile.X, tile.Y];
    }

    public IReadOnlyList<GTWeightedConnection> GetConnections(int x, int y)
    {
        return _nodes[x, y];
    }

    public IReadOnlyList<GTWeightedConnection> this[int x, int y] => _nodes[x, y];

    public void AddConnection(GTTile origin, GTTile destination, float weight)
    {
        _nodes[origin.X, origin.Y].Add(new GTWeightedConnection(destination, weight));
    }

    public bool RemoveConnection(GTTile origin, GTTile destination)
    {
        var connection = _nodes[origin.X, origin.Y].SingleOrDefault(x => x.Tile == destination);
        return connection != null && _nodes[origin.X, origin.Y].Remove(connection);
    }
}