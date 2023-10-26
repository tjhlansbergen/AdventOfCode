using System;

namespace AocRunner;

public class Day6
{
    public record Coord
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public record Bounds
    {
        public int Left { get; set; }
        public int Right { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }
    }

    public static void Run(string input, string[] lines)
    {
        var coords = lines.Select(l => new Coord
        {
            X = int.Parse(l.Split(',')[0].Trim()),
            Y = int.Parse(l.Split(',')[1].Trim())
        });

        var bounds = new Bounds
        {
            Left = coords.Select(c => c.X).Min(),
            Right = coords.Select(c => c.X).Max(),
            Top = coords.Select(c => c.Y).Min(),
            Bottom = coords.Select(c => c.Y).Max()
        };

        var grid = new Dictionary<Coord, Coord?>();
        var countPt2 = 0;

        for (int y = bounds.Top; y <= bounds.Bottom; y++)
        {
            System.Console.WriteLine($"{y}/{bounds.Bottom}");

            for (int x = bounds.Left; x <= bounds.Right; x++)
            {
                var manhattans = coords.Select(c => new { c, Manhattan = Manhattan(c, x, y) });

                if (manhattans.Select(m => m.Manhattan).Sum() < 10000) countPt2++;

                var closests = manhattans.Where(m => m.Manhattan == manhattans.Min(m => m.Manhattan));


                if (closests.Count() == 1)
                    grid.Add(new Coord { X = x, Y = y }, closests.Single().c);
                else
                    grid.Add(new Coord { X = x, Y = y }, null);
            }
        }

        var infiniteCoords = grid.Where(
                g => g.Key.X == bounds.Left || 
                g.Key.X == bounds.Right ||
                g.Key.Y == bounds.Top ||
                g.Key.Y == bounds.Bottom)
            .Select(g => g.Value)
            .Distinct();
            
        var max = coords.Where(c => !infiniteCoords.Contains(c))
            .Select(c => grid.Count(g => g.Value == c))
            .Max();

        System.Console.WriteLine($"Part 1: {max}");
        System.Console.WriteLine($"Part 2: {countPt2}");
        

        static int Manhattan(Coord c, int x, int y)
        {
            return Math.Abs(c.X - x) + Math.Abs(c.Y - y);
        }
    }
}