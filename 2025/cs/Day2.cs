namespace AocRunner;

public class Day2
{
    public static void Run(string input, string[] lines)
    {
        var ranges = input.Split(',').Select(r => r.Split('-').Select(long.Parse)).ToArray();
        var part1 = ranges.SelectMany(r => Proof(r)).Sum();
        Console.WriteLine($"Part 1: {part1}");

        var part2 = ranges.SelectMany(r => Proof2(r)).Sum();
        Console.WriteLine($"Part 2: {part2}");

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
            var len = s.Length;
            
            for (int i = 2; i <= len; i++)
            {
                if (len % i == 0)   // equal parts
                {
                    var pts = parts(s, i);
                    if (pts.All(p => p == pts[0])) return true;
                }   
            }

            string[] parts(string s, int count)
            {
                var partLen = s.Length / count;
                var parts = new string[count];
                for (int i = 0; i < count; i++)
                {
                    parts[i] = s.Substring(i * partLen, partLen);
                }

                return parts;
            }

            return false;
        }
    }
}