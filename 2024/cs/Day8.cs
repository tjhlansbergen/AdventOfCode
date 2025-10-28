namespace AocRunner;

public record Point(int X, int Y);

public class Day8
{
    public static void Run(string input, string[] lines)
    {
        var bounds = new Point(lines[0].Length, lines.Count());
        var grid = Parse(lines);
        var antinodes = Antinodes(grid).Where(n => 
            n.X > -1 &&
            n.X < bounds.X &&
            n.Y > -1 &&
            n.Y < bounds.Y);

        Console.WriteLine($"Part 1: {antinodes.Count()}");

        
    }

    public static Dictionary<Point, char> Parse(string[] lines)
    {
        var result = new Dictionary<Point, char>();

        for (int y = 0; y < lines.Count(); y++)
        {
            for (int x = 0; x < lines[0].Length; x++)
            {
                var c = lines[y][x];

                if (c != '.')
                {
                    result.Add(new Point(x, y), c);
                }
            }
        }

        return result;
    }

    public static HashSet<Point> Antinodes(Dictionary<Point, char> grid)
    {
        var result = new HashSet<Point>();

        // Group by value and generate unique pairs within each group
        var pairs = grid
            .GroupBy(kvp => kvp.Value)
            .SelectMany(group => 
                group.Select((first, i) => 
                    group.Skip(i + 1).Select(second => new { First = first, Second = second }))
                .SelectMany(x => x));

        foreach (var pair in pairs)
        {
            var dx = pair.First.Key.X - pair.Second.Key.X;
            var dy = pair.First.Key.Y - pair.Second.Key.Y;

            var antinode1 = new Point(pair.First.Key.X + dx, pair.First.Key.Y + dy);
            var antinode2 = new Point(pair.Second.Key.X - dx, pair.Second.Key.Y - dy);

            result.Add(antinode1);
            result.Add(antinode2);
        }

        return result;
    }
}