using System.Security.AccessControl;

namespace AocRunner;

public class Day6
{
    public class Object(string name)
    {
        public List<Object> Children { get; set; } = [];
        public string Name { get; set; } = name;
        public int Distance { get; set; }
        public List<Object> Route { get; set; } = [];
    }

    public static void Run(string input, string[] lines)
    {
        // map out the universe
        var universe = BuildUniverse(lines);

        // find distances for all objects in universe
        var com = universe.Single(o => o.Name == "COM");
        Visit(com, 0, []);

        // part 1, sum up all distances
        var totalDistances = universe.Sum(o => o.Distance);
        System.Console.WriteLine($"Part 1: {totalDistances}");

        // part 2, compare routes to find common 'ancestor'
        var you = universe.Single(o => o.Name == "YOU");
        var san = universe.Single(o => o.Name == "SAN");
        var sharedRoute = you.Route.Intersect(san.Route);
        var transfers = (you.Route.Count - sharedRoute.Count()) + (san.Route.Count - sharedRoute.Count());
        System.Console.WriteLine($"Part 2: {transfers}");
    }

    private static void Visit(Object o, int depth, List<Object> route)
    {
        o.Distance = depth;
        o.Route = route;
        
        foreach (var child in o.Children)
        {
            Visit(child, depth + 1, [.. route, o]);
        }
    }

    private static List<Object> BuildUniverse(string[] lines)
    {
        var universe = new List<Object>();

        Object AddOrGet(string name)
        {
            if (universe.Any(o => o.Name == name))
            {
                return universe.Single(o => o.Name == name);
            }
            else
            {
                var o = new Object(name);
                universe.Add(o);
                return o;
            }
        }

        foreach (var line in lines)
        {
            var left = AddOrGet(line.Split(')')[0]);
            var right = AddOrGet(line.Split(')')[1]);

            left.Children.Add(right);
        }

        return universe;
    }
}