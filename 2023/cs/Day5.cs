using System.Data;

namespace AocRunner;

public class Day5
{
    public class Map
    {
        public List<Range> Ranges { get; set; } = new ();

        public long Process(long seed)
        {
            var match = Ranges.SingleOrDefault(range => range.Match(seed));
            return match != null ? seed + match.Conversion : seed;
        }
    }

    public class Range
    {
        public long From { get; set; }   // inclusive
        public long To { get; set; }     // exclusive
        public long Conversion { get; set; }
        public bool Match(long source) => From <= source && source < To;
    }

    public static void Run(string input, string[] lines)
    {
        var seeds = lines[0].Split(": ")[1].Split(' ', StringSplitOptions.TrimEntries).Select(s => long.Parse(s));
        var maps = input.Split("\r\n\r\n").Select(block => Parse(block)).Where(map => map != null);
        var lowest = long.MaxValue;

        foreach (var seed in seeds)
        {
            var result = seed;

            foreach (var map in maps)
            {
                result = map.Process(result);
            }
            lowest = Math.Min(lowest, result);
        }
        System.Console.WriteLine($"Part 1: {lowest}");
    }

    public static Map Parse(string block)
    {
        if (block.StartsWith("seeds: ")) return null;

        var ranges = block.Split("\r\n", StringSplitOptions.TrimEntries)
                .Where(line => !line.Contains(':'))
                .Select(line => line.Split(' ').Select(i => long.Parse(i)).ToArray())                
                .Select(longs => new Range { From =  longs[1], To = longs[1] + longs[2], Conversion = longs[0] - longs[1] });

        return new Map{ Ranges = ranges.ToList() };
    }
}