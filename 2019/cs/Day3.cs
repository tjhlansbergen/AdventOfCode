using System;

namespace AocRunner;

public class Day3
{
    public struct Point
    {
        public Point() {}

        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
    }

    public static void Run(string input, string[] lines)
    {
        var parsedLines = lines.Select(l => ParseLine(l.Split(','))).ToList();
        var intersections = parsedLines[0].Keys.Intersect(parsedLines[1].Keys);

        // part 1
        var manhattans = intersections.Select(i => Manhattan(i));
        System.Console.WriteLine($"Part 1: {manhattans.Min()}");

        // part 2
        var counts = intersections.Select(i => parsedLines[0][i] + parsedLines[1][i]);
        System.Console.WriteLine($"Part 2: {counts.Min()}");
    }

    private static int Manhattan(Point point)
    {
        return Math.Abs(point.X) + Math.Abs(point.Y);
    }

    private static Dictionary<Point, int> ParseLine(string[] instructions)
    {
        var position = new Point();
        var count = 0;
        var result = new Dictionary<Point, int>();

        foreach(string i in instructions)
        {
            var direction = i[..1];
            var distance = int.Parse(i[1..]);

            for (int j = 0; j < distance; j++)
            {
                switch(direction)
                {
                    case "U":
                        position.Y--;
                        break;
                    case "D":
                        position.Y++;
                        break;
                    case "L":
                        position.X--;
                        break;
                    case "R":
                        position.X++;
                        break;
                }

                count++;

                if (!result.ContainsKey(position)) 
                {
                    result.Add(position, count);
                }
            }
        }

        return result;
    }
}