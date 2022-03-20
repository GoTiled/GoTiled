namespace GoTiled.Core;

public readonly struct GTTile : IEquatable<GTTile>
{
    public int X { get; }
    public int Y { get; }

    public GTTile(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(GTTile other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is GTTile other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator ==(GTTile lhs, GTTile rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(GTTile lhs, GTTile rhs)
    {
        return !(lhs == rhs);
    }

    public override string ToString()
    {
        return $"[{X}, {Y}]";
    }
}