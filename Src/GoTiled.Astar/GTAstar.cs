using GoTiled.Astar.Internal.Astar;
using GoTiled.Astar.Internal.Astar.Weighted;
using GoTiled.Core;

namespace GoTiled.Astar;

public static class GTAstar
{
    public static bool Calculate(GTPathMap map, GTTile origin, GTTile destination, out List<GTTile> path)
    {
        return GTAstarInternal.Calculate(map, origin, destination, GTAstarInternal.DefaultHeuristic, out path);
    }

    public static bool Calculate(GTPathMap map, int originX, int originY, int destinationX, int destinationY, out List<GTTile> path)
    {
        return Calculate(map, new GTTile(originX, originY), new GTTile(destinationX, destinationY), out path);
    }

    public static bool Calculate(GTPathMap map, GTTile origin, GTTile destination, Func<GTTile, GTTile, float> heuristic, out List<GTTile> path)
    {
        return GTAstarInternal.Calculate(map, origin, destination, heuristic, out path);
    }

    public static bool Calculate(GTPathMap map, int originX, int originY, int destinationX, int destinationY, Func<GTTile, GTTile, float> heuristic, out List<GTTile> path)
    {
        return Calculate(map, new GTTile(originX, originY), new GTTile(destinationX, destinationY), heuristic, out path);
    }

    public static bool Calculate(GTWeightedPathMap map, GTTile origin, GTTile destination, out List<GTTile> path)
    {
        return GTWeightedAstarInternal.Calculate(map, origin, destination, GTWeightedAstarInternal.DefaultHeuristic, out path);
    }

    public static bool Calculate(GTWeightedPathMap map, int originX, int originY, int destinationX, int destinationY, out List<GTTile> path)
    {
        return Calculate(map, new GTTile(originX, originY), new GTTile(destinationX, destinationY), out path);
    }

    public static bool Calculate(GTWeightedPathMap map, GTTile origin, GTTile destination, Func<GTTile, GTTile, float> heuristic, out List<GTTile> path)
    {
        return GTWeightedAstarInternal.Calculate(map, origin, destination, heuristic, out path);
    }

    public static bool Calculate(GTWeightedPathMap map, int originX, int originY, int destinationX, int destinationY, Func<GTTile, GTTile, float> heuristic, out List<GTTile> path)
    {
        return Calculate(map, new GTTile(originX, originY), new GTTile(destinationX, destinationY), heuristic, out path);
    }
}