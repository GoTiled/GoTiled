using GoTiled.Core;

namespace GoTiled.Map;

public class GTMap<TTile> where TTile : struct
{
    private readonly TTile[,] _tiles;

    public GTMap(TTile[,] tiles)
    {
        _tiles = tiles;
    }

    public int SizeX => _tiles.GetLength(0);
    public int SizeY => _tiles.GetLength(1);

    public TTile Get(GTTile tile)
    {
        return _tiles[tile.X, tile.Y];
    }

    public TTile Get(int x, int y)
    {
        return _tiles[x, y];
    }

    public TTile this[int x, int y] => _tiles[x, y];
}