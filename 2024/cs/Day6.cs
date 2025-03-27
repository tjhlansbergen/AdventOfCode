using System.Text;

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
        var start = FindStart(lines);

        var part1 = Looped(lines, start.X, start.Y).Item2;
        Console.WriteLine($"Part 1: {part1}");


        var countLooped = 0;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (lines[y][x] == '.')
                {
                    var newLines = lines.ToArray();
                    var sb = new StringBuilder(newLines[y]);
                    sb[x] = '#'; // Directly modify the character
                    newLines[y] = sb.ToString(); // Update the list with the modified string

                    if (Looped(newLines, start.X, start.Y).Item1)
                    {
                        countLooped++;
                    }
                }
            }
        }

        Console.WriteLine($"Part 2: {countLooped}");
    }

    private static void Print(string[] lines)
    {
        Console.WriteLine();
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }
        Console.WriteLine();
    }

    private static (bool, int) Looped(string[] lines, int startX, int startY)
    {
        var width = lines[0].Length;
        var height = lines.Length;
        

        var position = new Coordinate(startX, startY);
        var direction = new Coordinate(0, -1); // north
        var visited = new Dictionary<Coordinate, Coordinate>();

        while (true)
        {
            if (visited.TryGetValue(position, out Coordinate? value))
            {
                if (value == direction)
                {
                    // looped
                    return (true, -1);
                }
            }
            else
            {
                // mark as visited
                visited.Add(new Coordinate(position.X, position.Y), new Coordinate(direction.X, direction.Y)); 
            }
            
            // move
            position.X += direction.X;
            position.Y += direction.Y;

            if (!Inbounds(position, width, height))
            {
                return (false, visited.Count);
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