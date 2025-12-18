namespace AocRunner;

public class Day4
{
    public static void Run(string input, string[] lines)
    {
        var grid = new Dictionary<(int x, int y), char>();

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                grid[(x, y)] = lines[y][x];
            }
        }

        var part1 = grid.Count(p => p.Value == '@'
                                      && adjacents(p.Key.x, p.Key.y)
                                      .Count(a => roll(a.Item1, a.Item2, grid)) < 4);

        
        Console.WriteLine($"Part 1: {part1}");

        static (int, int)[] adjacents(int x, int y) => [
                (x + 1, y),
                (x + 1, y + 1),
                (x, y + 1),
                (x - 1, y + 1),
                (x - 1, y),
                (x - 1, y - 1),
                (x, y - 1),
                (x + 1, y - 1)
            ];

        static bool roll(int x, int y, Dictionary<(int x, int y), char> grid) 
            => grid.ContainsKey((x, y)) && grid[(x, y)] == '@';
    }
}