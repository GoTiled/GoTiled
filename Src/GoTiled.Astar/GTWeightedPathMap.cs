using GoTiled.Core;

namespace GoTiled.Astar;

public class GTWeightedPathMap<TWeight>
{
    private readonly List<GTWeightedConnection<TWeight>>[,] _nodes;

    public GTWeightedPathMap(int sizeX, int sizeY)
    {
        _nodes = new List<GTWeightedConnection<TWeight>>[sizeX, sizeY];

        for (var x = 0; x < sizeX; x++)
        {
            for (var y = 0; y < sizeY; y++)
            {
                var connections = new List<GTWeightedConnection<TWeight>>();
                _nodes[x, y] = connections;
            }
        }
    }

    public int SizeX => _nodes.GetLength(0);
    public int SizeY => _nodes.GetLength(1);

    public IReadOnlyList<GTWeightedConnection<TWeight>> GetConnections(GTTile tile)
    {
        return _nodes[tile.X, tile.Y];
    }

    public IReadOnlyList<GTWeightedConnection<TWeight>> GetConnections(int x, int y)
    {
        return _nodes[x, y];
    }

    public IReadOnlyList<GTWeightedConnection<TWeight>> this[int x, int y] => _nodes[x, y];

    public void AddConnection(GTTile origin, GTTile destination, TWeight weight)
    {
        _nodes[origin.X, origin.Y].Add(new GTWeightedConnection<TWeight>(destination, weight));
    }

    public bool RemoveConnection(GTTile origin, GTTile destination)
    {
        var connection = _nodes[origin.X, origin.Y].SingleOrDefault(x => x.Tile == destination);
        return connection != null && _nodes[origin.X, origin.Y].Remove(connection);
    }
}