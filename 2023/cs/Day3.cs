namespace AocRunner;

public class Day3
{
    public static void Run(string input, string[] lines)
    {
        var width = lines[0].Length;
        var height = lines.Length;

        var count = 0;
        var start = -1;
        var numberIsPart = false;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (char.IsDigit(lines[y][x])) 
                {
                    if (start == -1)
                    {
                        // start of number
                        start = x;
                    }
                    
                    if (!numberIsPart) numberIsPart = IsPart(x, y, lines);
                }
                else    // . or symbol
                {
                    if (start != -1)    // reached end of number
                    {
                        if (numberIsPart)
                        {
                            var part = int.Parse(lines[y][start..x]);
                            count += part;
                        }
                        start = -1;
                        numberIsPart = false;
                    }
                }
            }

            // reached end of line
            if (numberIsPart)
            {
                var part = int.Parse(lines[y][start..width]);
                count += part;
            }
            start = -1;
            numberIsPart = false;
        }

        System.Console.WriteLine($"Part 1: {count}");
    }

    private static bool IsPart(int x, int y, string[] grid)
    {
        var noSymbol = new [] { '.','0','1','2','3','4','5','6','7','8','9' };

        var neighbours = new [] { 
            new { X = x - 1, Y = y },
            new { X = x - 1, Y = y - 1 },
            new { X = x - 1, Y = y + 1 },
            new { X = x, Y = y - 1 },
            new { X = x, Y = y + 1 },
            new { X = x + 1, Y = y },
            new { X = x + 1, Y = y - 1 },
            new { X = x + 1, Y = y + 1 }
            };

        bool WithinBounds(int x, int y)
        {
            return x > -1 && 
                   x < grid[0].Length && 
                   y > -1 && 
                   y < grid.Length;
        }

        return neighbours.Where(n => WithinBounds(n.X, n.Y))
                         .Any(n => !noSymbol.Contains(grid[n.Y][n.X]));
    }
}