namespace AocRunner;

public class Day2
{
    public static void Run(string input, string[] lines)
    {
        var reports = lines.Select(line => line.Split(" ").Select(int.Parse).ToArray()).ToArray();
        var part1 = reports.Count(Safe);

        System.Console.WriteLine(part1);

        var part2 = reports.Count(SortOfSafe);

        System.Console.WriteLine(part2);
    }

    public static bool Safe(int[] report)
    {
        var diffs = Enumerable.Range(0, report.Length - 1).Select(i => report[i] - report[i + 1]).ToArray();
        
        if (diffs.Any(d => Math.Abs(d) < 1 || Math.Abs(d) > 3))
        {
            return false;
        }

        return diffs.All(d => d > 0) || diffs.All(d => d < 0);
    }

    public static bool SortOfSafe(int[] report)
    {
        if (Safe(report)) return true;

        for (int i = 0; i < report.Length; i++)
        {
            var dampenendReport = report.ToList();
            dampenendReport.RemoveAt(i);
            if (Safe(dampenendReport.ToArray()))
            {
                return true;
            }
        }

        return false;
    }


}