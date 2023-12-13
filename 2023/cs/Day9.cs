namespace AocRunner;

public class Day9
{
    public static void Run(string input, string[] lines)
    {
        var solves = lines.Select(line => line.Split(' ').Select(int.Parse).ToArray())
                          .Select(line => SolveLine(line));

        var part1 = solves.Select(s => s.Item2).Sum();
        System.Console.WriteLine($"Part 1: {part1}");

        var part2 = solves.Select(s => s.Item1).Sum();
        System.Console.WriteLine($"Part 2: {part2}");
    }

    private static (int, int) SolveLine(int[] line)
    {
        var firsts = new List<int>();
        var lasts = new List<int>();

        while (!line.All(l => l == 0))
        {
            firsts.Add(line.First());
            lasts.Add(line.Last());

            var next = new List<int>(line.Length - 1);

            for (int i = 1; i < line.Length; i++)
            {
                next.Add(line[i] - line[i-1]);
            }

            line = next.ToArray();
        }

        return (Enumerable.Reverse(firsts).Aggregate((a,b) => b-a), lasts.Sum());
    }
}