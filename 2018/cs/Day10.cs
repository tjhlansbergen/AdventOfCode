using System.Runtime.CompilerServices;

namespace AocRunner;

public class Day10
{
    public class Pixel
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Xs { get; set; } = 0;
        public int Ys { get; set; } = 0;

        public void Move()
        {
            X += Xs;
            Y += Ys;
        }

        public void Revert()
        {
            X -= Xs;
            Y -= Ys;
        }
    }

    public static void Run(string input, string[] lines)
    {
        var pixels = lines.Select(line => Parse(line)).ToHashSet();
        var bounds = (int.MaxValue, int.MaxValue);
        var count = 0;

        System.Console.WriteLine("Part 1:");

        while (true)
        {
            count++;
            foreach(var pixel in pixels) pixel.Move();
            var newBounds = (pixels.Max(p => p.X) - pixels.Min(p => p.X), pixels.Max(p => p.Y) - pixels.Min(p => p.Y));

            if (newBounds.Item1 > bounds.Item1 || newBounds.Item2 > bounds.Item2)  // it grew
            {
                // one step back
                foreach(var pixel in pixels) pixel.Revert();
                count--;
                
                Draw(pixels);
                break;
            }

            bounds.Item1 = newBounds.Item1;
            bounds.Item2 = newBounds.Item2;
        }

        System.Console.WriteLine();
        System.Console.WriteLine($"Part 2: {count}");
    }

    public static Pixel Parse(string line)
    {
        var splits = line.Replace("position=<", "").Replace("> velocity=<", ",").Replace(">", "").Split(',').Select(x => int.Parse(x.Trim())).ToArray();
        return new Pixel { X = splits[0], Y = splits[1], Xs = splits[2], Ys = splits[3] };
    }

    private static void Draw(IEnumerable<Pixel> pixels)
    {
        var left = pixels.Min(p => p.X);
        var right = pixels.Max(p => p.X);
        var top = pixels.Min(p => p.Y);
        var bottom = pixels.Max(p => p.Y);

        for (int y = top; y <= bottom; y++)
        {
            for (int x = left; x <= right; x++)
            {
                if (pixels.Any(p => p.X == x && p.Y == y))
                    System.Console.Write('#');
                else
                    System.Console.Write(' ');
            }
            System.Console.WriteLine();
        }

        System.Console.WriteLine();
        System.Console.WriteLine();
    }
}