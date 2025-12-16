namespace AocRunner;

public class Day2
{
    public static void Run(string input, string[] lines)
    {
        var ranges = input.Split(',').Select(r => r.Split('-').Select(long.Parse)).ToArray();
        var part1 = ranges.SelectMany(r => Proof(r)).Sum();
        Console.WriteLine($"Part 1: {part1}");

        // foreach (var range in ranges)
        // {
        //     var invalids = Proof(range).ToArray();
        //     Console.WriteLine($"Range {range.First()}-{range.Last()}: {string.Join(", ", invalids)}");
        // }

    }

    public static IEnumerable<long> Proof(IEnumerable<long> range)
    {
        for (long i = range.First(); i <= range.Last(); i++)
        {
            if (invalid(i)) yield return i;
        }

        bool invalid(long id)
        {
            var s = id.ToString();
            
            if (s.Length % 2 != 0) return false;    // must be even length

            return s[..(s.Length / 2)] == s[(s.Length / 2)..];
                
        }
    }

    public static IEnumerable<long> Proof2(IEnumerable<long> range)
    {
        for (long i = range.First(); i <= range.Last(); i++)
        {
            if (invalid2(i)) yield return i;
        }

        bool invalid2(long id)
        {
            var s = id.ToString();
    
        }
    }
}