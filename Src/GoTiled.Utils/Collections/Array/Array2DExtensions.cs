namespace GoTiled.Utils.Collections.Array;

internal static class Array2DExtensions
{
    public static T GetValueOrDefault<T>(this T[,] array, int x, int y, T defaultValue)
    {
        if (0 <= x && x < array.GetLength(0) && 0 <= y && y < array.GetLength(1))
        {
            return array[x, y];
        }
        return defaultValue;
    }
}