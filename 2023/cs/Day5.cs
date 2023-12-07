using System.ComponentModel.Design;
using System.Data;

namespace AocRunner;

public class Day5
{
    public class Mapper
    {
        public required Map[] Maps { get; set; }

        public long Process(long seed, bool reverse = false)
        {
            foreach (var map in Maps)
            {
                seed = map.Process(seed);
            }

            return seed;
        }
    }

    public class Map
    {
        public List<Range> Ranges { get; set; } = new();

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
        var maps = input.Split("\r\n\r\n").Select(block => Parse(block)).Where(map => map != null).Reverse().ToArray();     // in reverse, to traverse the maps buttom to top
        var mapper = new Mapper { Maps = maps };

        var allRanges = maps.SelectMany(m => m.Ranges);
        var seeds = lines[0].Split(": ")[1].Split(' ', StringSplitOptions.TrimEntries).Select(s => long.Parse(s));

        // start with the 'lowest' possbile outcome, test all possible outcomes until one matches an original seed
        var stop = false;
        foreach (var range in allRanges.OrderBy(range => range.From))
        {
            for (long i = range.From; i < range.To; i++)
            {
                var result = mapper.Process(i);

                if (seeds.Any(seed => seed == result))
                {
                    System.Console.WriteLine($"Part 1: {i}");
                    stop = true;
                    break; 
                }
            }
            if (stop) break;
        }

        var seedRanges = lines[0]
            .Split(": ")[1]
            .Split(' ', StringSplitOptions.TrimEntries)
            .Select(s => long.Parse(s))
            .Chunk(2)
            .Select(pair => (pair[0], pair[0] + pair[1]))
            .ToArray();

        // start with the 'lowest' possbile outcome, test all possible outcomes until one matches an original seed from any of the seed ranges
        stop = false;
        foreach (var range in allRanges.OrderBy(range => range.From))
        {
            for (long i = range.From; i < range.To; i++)
            {
                var result = mapper.Process(i);

                if (seedRanges.Any(sr => sr.Item1 <= result && result < sr.Item2))
                {
                    System.Console.WriteLine($"Part 2: {i}");
                    stop = true;
                    break; 
                }
            }
            if (stop) break;
        }
    }

    public static Map Parse(string block)
    {
        if (block.StartsWith("seeds: ")) return null!;

        var ranges = block.Split("\r\n", StringSplitOptions.TrimEntries)
                .Where(line => !line.Contains(':'))
                .Select(line => line.Split(' ').Select(i => long.Parse(i)).ToArray())
                .Select(longs => new Range { From = longs[0], To = longs[0] + longs[2], Conversion = longs[1] - longs[0] });    // this destination and source, since we will be traversing the maps in reverse

        return new Map { Ranges = ranges.ToList() };
    }
}