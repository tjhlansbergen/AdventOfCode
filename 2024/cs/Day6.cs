namespace AocRunner;

public class Day6
{
    public record Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public static void Run(string input, string[] lines)
    {
        var width = lines[0].Length;
        var height = lines.Length;

        var position = FindStart(lines);
        var direction = new Coordinate(0, -1); // north
        var visited = new List<Coordinate>();

        while (true)
        {
            visited.Add(new Coordinate(position.X, position.Y));

            // move
            position.X += direction.X;
            position.Y += direction.Y;

            if (!Inbounds(position, width, height))
            {
                break;
            }

            if (lines[position.Y][position.X] == '#')
            {
                // turn back
                position.X -= direction.X;
                position.Y -= direction.Y;

                // turn right
                direction = Rotate(direction);
            }
        }

        var part1 = visited.Distinct().Count();
        Console.WriteLine($"Part 1: {part1}");
    }

    private static bool Inbounds(Coordinate position, int width, int height)
    {
        return position.X >= 0 && position.X < width && position.Y >= 0 && position.Y < height;
    }

    private static Coordinate FindStart(string[] lines)
    {
        var width = lines[0].Length;
        var height = lines.Length;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (lines[y][x] == '^')
                {
                    return new Coordinate(x, y);
                }
            }
        }

        throw new ArgumentException("Start not found");
    }

    private static Coordinate Rotate(Coordinate current)
    {
        // north -> east
        if (current.X == 0 && current.Y == -1)
        {
            return new Coordinate(1, 0);
        }

        // east -> south
        if (current.X == 1 && current.Y == 0)
        {
            return new Coordinate(0, 1);
        }

        // south -> west
        if (current.X == 0 && current.Y == 1)
        {
            return new Coordinate(-1, 0);
        }

        // west -> north
        if (current.X == -1 && current.Y == 0)
        {
            return new Coordinate(0, -1);
        }

        throw new ArgumentException("Invalid rotation");
    }
}