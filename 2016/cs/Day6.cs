namespace AocRunner;

public class Day6
{
    public static void Run(string input, string[] lines)
    {
        var columns = lines.Select(l => l.Select(c => c))
            .SelectMany(e => e.Select((item, index) => new { item, index }))
            .GroupBy(i => i.index, i => i.item);

        var mostCommon = columns.Select(c => c.GroupBy(ch => ch).OrderByDescending(g => g.Count()).First().Key);
        System.Console.WriteLine($"part 1: {string.Concat(mostCommon)}");

        var leastCommon = columns.Select(c => c.GroupBy(ch => ch).OrderBy(g => g.Count()).First().Key);
        System.Console.WriteLine($"part 2: {string.Concat(leastCommon)}");
    }
}