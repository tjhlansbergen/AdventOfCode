namespace AocRunner;

public class Day1
{
    public static void Run(string input, string[] lines)
    {
        var groupedLines = new List<KeyValuePair<int, int>>();
        var group = 0;

        foreach (var line in lines)
        {
            if( string.IsNullOrWhiteSpace(line)) group++;
            else groupedLines.Add(new KeyValuePair<int,int>(group, int.Parse(line)));
        }

        var part1 = groupedLines.GroupBy(g => g.Key).Select(gr => gr.Sum(v => v.Value)).Max();
        System.Console.WriteLine($"Part 1: {part1}");

        var part2 = groupedLines
                    .GroupBy(g => g.Key)
                    .Select(gr => gr
                    .Sum(v => v.Value))
                    .OrderByDescending(s => s)
                    .Take(3)
                    .Sum();

        System.Console.WriteLine($"Part 2: {part2}");
    }
}