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

        foreach (var node in grid)
        {
            var siblings = grid.Where(g => g.Key != node.Key && g.Value == node.Value);
            foreach (var other in siblings)
            {
                var xx = Math.Abs(node.Key.X - other.Key.X);
                var yy = Math.Abs(node.Key.Y - other.Key.Y);

                var xn = node.Key.X;
                var yn = node.Key.Y;
                var xo = other.Key.X;
                var yo = other.Key.Y;

                if (node.Key.X > other.Key.X)
                {
                    xn = node.Key.X + xx;
                    xo = other.Key.X - xx;    
                }
                else if (node.Key.X < other.Key.X)
                {
                    xn = node.Key.X - xx; 
                    xo = other.Key.X + xx; 
                }

                if (node.Key.Y > other.Key.Y)
                {
                    yn = node.Key.Y + yy; 
                    yo = other.Key.Y - yy;   
                }
                else if (node.Key.Y < other.Key.Y)
                {
                    yn = node.Key.Y - yy; 
                    yo = other.Key.Y + yy; 
                }

                result.Add(new Point(xn,yn));
                result.Add(new Point(xo, yo));
            }
        }

        return result;
    }
}