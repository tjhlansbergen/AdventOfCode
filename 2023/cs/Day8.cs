namespace AocRunner;

public class Day8
{
    public static void Run(string input, string[] lines)
    {
        var directions = new LinkedList<int>(lines[0].Select(c => (c == 'L') ? 0 : 1));
        var map = ParseMap(lines.Where(line => line.Contains('=')));

        var currentDirection = directions.First;
        var here = "AAA";
        var count = 0;

        while (here != "ZZZ")
        {
            here = map[here][currentDirection!.Value];
            currentDirection = currentDirection.Next ?? directions.First;
            count++;
        }

        System.Console.WriteLine($"Part 1: {count}");

        //


        var starts = map.Keys.Where(k => k[2] == 'A');
        var counts = new List<long>();

        foreach (var start in starts)
        {
            currentDirection = directions.First;
            here = start;
            count = 0;

            while (here[2] != 'Z')
            {
                here = map[here][currentDirection!.Value];
                currentDirection = currentDirection!.Next ?? directions.First;
                count++;
            }
            counts.Add(count);
        }

        System.Console.WriteLine($"Part 2: {LCM(counts.ToArray())}");

    }

    private static Dictionary<string, string[]> ParseMap(IEnumerable<string> lines)
    {
        return lines.Select(l => l.Replace('=', ',').Replace('(', ' ').Replace(')', ' '))
             .Select(l => l.Split(',', StringSplitOptions.TrimEntries))
             .Select(splits => new { Key = splits[0], Value = new[] { splits[1], splits[2] } })
             .ToDictionary(x => x.Key, x => x.Value);
    }

    // https://stackoverflow.com/questions/147515/least-common-multiple-for-3-or-more-numbers/29717490#29717490
    private static long LCM(long[] numbers)
    {
        return numbers.Aggregate(lcm);
    }
    private static long lcm(long a, long b)
    {
        return Math.Abs(a * b) / GCD(a, b);
    }
    private static long GCD(long a, long b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }
}