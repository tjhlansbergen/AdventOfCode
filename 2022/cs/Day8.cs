namespace AocRunner;

public class Day8
{
    public static void Run(string input, string[] lines)
    {
        if (lines[0].Length != lines.Count()) throw new InvalidDataException("Grid is not square");

        Part1(lines);
        Part2(lines);
    }

    private static void Part1(string[] lines)
    {

        var size = lines.Count();
        var grid = new bool[size, size];
        var tallest = -1;

        for (int y = 0; y < size; y++)
        {
            tallest = -1;

            // rows forwards
            for (int x = 0; x < size; x++)
            {
                if (lines[y][x] > tallest)
                {
                    grid[x, y] = true;
                    tallest = lines[y][x];
                }
            }

            tallest = -1;

            // rows backwards
            for (int x = size - 1; x > -1; x--)
            {
                if (lines[y][x] > tallest)
                {
                    grid[x, y] = true;
                    tallest = lines[y][x];
                }
            }
        }

        for (int x = 0; x < size; x++)
        {
            tallest = -1;

            // cols forwards
            for (int y = 0; y < size; y++)
            {
                if (lines[y][x] > tallest)
                {
                    grid[x, y] = true;
                    tallest = lines[y][x];
                }
            }

            tallest = -1;

            // cols backwards
            for (int y = size - 1; y > -1; y--)
            {
                if (lines[y][x] > tallest)
                {
                    grid[x, y] = true;
                    tallest = lines[y][x];
                }
            }
        }

        var part1 = grid.Cast<bool>().Count(x => x);
        System.Console.WriteLine($"Part 1: {part1}");

    }

    private static void Part2(string[] lines)
    {
        var size = lines.Count();
        var scenicScore = 0;


        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                var score = GetScore(lines, x, y);
                if (score > scenicScore) scenicScore = score;
            }
        }

        System.Console.WriteLine($"Part 2: {scenicScore}");

        int GetScore(string[] grid, int x, int y)
        {
            // wat een puinhoop :()
            
            int z;
            var subScores = new List<int>();

            // look up
            z = 1;
            while (y - z > 0 && grid[y - z][x] < grid[y][x])
            {
                z++;
            }
            subScores.Add(y > 0 ? z : 0);

            z = 1;
            // look left
            while (x - z > 0 && grid[y][x - z] < grid[y][x])
            {
                z++;
            }
            subScores.Add(x > 0 ? z : 0);

            // look down
            z = 1;
            while (y + z < size - 1 && grid[y + z][x] < grid[y][x])
            {
                z++;
            }
            subScores.Add(y < size - 1 ? z : 0);

            // look right
            z = 1;
            while (x + z < size - 1 && grid[y][x + z] < grid[y][x])
            {
                z++;
            }
            subScores.Add(x < size - 1 ? z : 0);

            return subScores.Aggregate((a, x) => a * x);
        }
    }
}