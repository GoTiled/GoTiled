using GoTiled.Core;

namespace GoTiled.Astar;

public class GTPathMap
{
    private readonly List<GTTile>[,] _nodes;

    public GTPathMap(bool[,] walkable) : this(walkable, false)
    { }

    public GTPathMap(bool[,] walkable, bool diagonal)
    {
        _nodes = new List<GTTile>[walkable.GetLength(0), walkable.GetLength(1)];

        bool GetWalkable(bool[,] walkable, int x, int y)
        {
            if (0 <= x && x < walkable.GetLength(0) && 0 <= y && y < walkable.GetLength(1))
            {
                return walkable[x, y];
            }
            return false;
        }

        for (var x = 0; x < walkable.GetLength(0); x++)
        {
            for (var y = 0; y < walkable.GetLength(1); y++)
            {
                if (!GetWalkable(walkable, x, y))
                    continue;

                var connections = new List<GTTile>();
                _nodes[x, y] = connections;

                // Diagonal
                if (diagonal)
                {
                    if (GetWalkable(walkable, x - 1, y - 1))
                    {
                        connections.Add(new GTTile(x - 1, y - 1));
                    }

                    if (GetWalkable(walkable, x + 1, y - 1))
                    {
                        connections.Add(new GTTile(x + 1, y - 1));
                    }

                    if (GetWalkable(walkable, x - 1, y + 1))
                    {
                        connections.Add(new GTTile(x - 1, y + 1));
                    }

                    if (GetWalkable(walkable, x + 1, y + 1))
                    {
                        connections.Add(new GTTile(x + 1, y + 1));
                    }
                }

                // Straight
                if (GetWalkable(walkable, x, y - 1))
                {
                    connections.Add(new GTTile(x, y - 1));
                }
                if (GetWalkable(walkable, x - 1, y))
                {
                    connections.Add(new GTTile(x - 1, y));
                }
                if (GetWalkable(walkable, x + 1, y))
                {
                    connections.Add(new GTTile(x + 1, y));
                }
                if (GetWalkable(walkable, x, y + 1))
                {
                    connections.Add(new GTTile(x, y + 1));
                }
            }
        }
    }

    public int SizeX => _nodes.GetLength(0);
    public int SizeY => _nodes.GetLength(1);

    public IReadOnlyList<GTTile> Get(GTTile tile)
    {
        return _nodes[tile.X, tile.Y];
    }

    public IReadOnlyList<GTTile> Get(int x, int y)
    {
        return _nodes[x, y];
    }

    public IReadOnlyList<GTTile> this[int x, int y] => _nodes[x, y];
}