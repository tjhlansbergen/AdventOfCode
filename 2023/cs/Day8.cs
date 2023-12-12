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
    }

    private static Dictionary<string, string[]> ParseMap(IEnumerable<string> lines)
    {
        return lines.Select(l => l.Replace('=', ',').Replace('(', ' ').Replace(')', ' '))
             .Select(l => l.Split(',', StringSplitOptions.TrimEntries))
             .Select(splits => new { Key = splits[0], Value = new [] {splits[1], splits[2]} } )
             .ToDictionary(x => x.Key, x => x.Value);
    }
}