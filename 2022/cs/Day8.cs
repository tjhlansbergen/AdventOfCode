namespace AocRunner;

public class Day8
{
    public static void Run(string input, string[] lines)
    {
        if (lines[0].Length != lines.Count()) throw new InvalidDataException("Grid is not square");

        var size = lines.Count();
        var grid = new bool[size, size];
        var tallest = -1;

        Print(grid);
        System.Console.WriteLine();

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

        Print(grid);

        var part1 = grid.Cast<bool>().Count(x => x);
        System.Console.WriteLine($"Part 1: {part1}");

        void Print(bool[,] grid)
        {
            for (int y = 0; y < size; y++)
            {
                // rows forwards
                for (int x = 0; x < size; x++)
                {
                    System.Console.Write($"{grid[x, y]} \t");
                }
                System.Console.WriteLine();
            }
        }

    }
}