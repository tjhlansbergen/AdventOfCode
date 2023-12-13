namespace AocRunner;

public class Day9
{
    public static void Run(string input, string[] lines)
    {
        var part1 = lines.Select(line => line.Split(' ').Select(i => int.Parse(i)).ToArray())
             .Select(line => SolveLine(line))
             .Sum();

        System.Console.WriteLine($"Part 1: {part1}");

        var part2 = lines.Select(line => line.Split(' ').Select(i => int.Parse(i)).ToArray())
             .Select(line => SolveLine(line, left: true))
             .Sum();

        System.Console.WriteLine($"Part 2: {part2}");
    }

    private static int SolveLine(int[] line, bool left = false)
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

        if (left)
        {
            return Enumerable.Reverse(firsts).Aggregate((a,b) => b-a);
        }

        return lasts.Sum();
    }
}