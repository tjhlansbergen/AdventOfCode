namespace AocRunner;

public class Day6
{
    public static void Run(string input, string[] lines)
    {
        var races = Parse1(lines);

        var part1 = races.Select(race =>
        {
            return Race(race);
        }).Aggregate((a,b) => a * b);
        
        System.Console.WriteLine($"Part 1: {part1}");

        var race = Parse2(lines);
        var part2 = Race(race);

        System.Console.WriteLine($"Part 2: {part2}");

        // I suppose you could optimize this by finding the start and and of the 'winning block'
        // but since this runs in < 1 sec I do not care
        static int Race(long[] race)
        {
            var count = 0;
            for (int i = 0; i < race[0]; i++)
            {
                if (i * (race[0] - i) > race[1]) count++;
            }
            return count;
        }
    }

    private static IEnumerable<long[]> Parse1(string[] lines)
    {
        var splits = lines.Select(line =>
            line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(split => new { Parsed = long.TryParse(split, out var i), Value = i })
            .Where(split => split.Parsed)
            .Select(split => split.Value));

        return splits.SelectMany(inner => inner.Select((item, index) => new { item, index }))
            .GroupBy(i => i.index, i => i.item)
            .Select(g => g.ToArray());
    }

    private static long[] Parse2(string[] lines)
    {
        return lines.Select(line => 
            long.Parse(line.Replace(" ", "").Split(':')[1]))
            .ToArray();
    }
}