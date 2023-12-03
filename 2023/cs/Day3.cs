namespace AocRunner;

public class Day3
{
    // this became a complete mess but at least it works and is fast

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

        //---

        start = -1;
        var numberIsGear = false;
        var gear = (0,0);
        var gears = new Dictionary<(int,int), List<int>>();

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

                    if (!numberIsGear) numberIsGear = IsGear(x, y, lines, out gear);
                }
                else    // . or symbol
                {
                    if (start != -1)    // reached end of number
                    {
                        if (numberIsGear)
                        {
                            var part = int.Parse(lines[y][start..x]);
                            if (gears.ContainsKey(gear))
                            {
                                gears[gear].Add(part);
                            }
                            else
                            {
                                gears.Add(gear, new() { part });
                            }
                        }
                        start = -1;
                        numberIsGear = false;
                    }
                }
            }

            // reached end of line
            if (numberIsGear)
            {
                var part = int.Parse(lines[y][start..width]);
                if (gears.ContainsKey(gear))
                {
                    gears[gear].Add(part);
                }
                else
                {
                    gears.Add(gear, new() { part });
                }
            }
            start = -1;
            numberIsGear = false;
        }

        var part2 = gears.Where(g => g.Value.Count == 2).Select(g => g.Value.First() * g.Value.Last()).Sum();
        System.Console.WriteLine($"Part 2: {part2}");
    }

    private static bool WithinBounds(int x, int y, int width, int height)
    {
        return x > -1 &&
               x < width &&
               y > -1 &&
               y < height;
    }

    private static bool IsPart(int x, int y, string[] grid)
    {
        var noSymbol = new[] { '.', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        var neighbours = new[] {
            new { X = x - 1, Y = y },
            new { X = x - 1, Y = y - 1 },
            new { X = x - 1, Y = y + 1 },
            new { X = x, Y = y - 1 },
            new { X = x, Y = y + 1 },
            new { X = x + 1, Y = y },
            new { X = x + 1, Y = y - 1 },
            new { X = x + 1, Y = y + 1 }
        };

        return neighbours.Where(n => WithinBounds(n.X, n.Y, grid[0].Length, grid.Length))
                         .Any(n => !noSymbol.Contains(grid[n.Y][n.X]));
    }

    private static bool IsGear(int x, int y, string[] grid, out (int, int) gear)
    {
        gear = (0,0);
        
        var neighbours = new[] {
            new { X = x - 1, Y = y },
            new { X = x - 1, Y = y - 1 },
            new { X = x - 1, Y = y + 1 },
            new { X = x, Y = y - 1 },
            new { X = x, Y = y + 1 },
            new { X = x + 1, Y = y },
            new { X = x + 1, Y = y - 1 },
            new { X = x + 1, Y = y + 1 }
        };

        foreach (var n in neighbours.Where(n => WithinBounds(n.X, n.Y, grid[0].Length, grid.Length)))
        {
            if (grid[n.Y][n.X] == '*')
            {
                gear = (n.X,n.Y);
                return true;
            }
        }

        return false;
    }
}