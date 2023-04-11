namespace AocRunner;

public class Day4
{
    public static void Run(string input, string[] lines)
    {
        var part1 = lines.Select(l => l.Split(' '))
                        .Count(s => s.Distinct().Count() == s.Count());

        System.Console.WriteLine($"Part 1: {part1}");

        var part2 = lines.Select(l => l.Split(' '))
                        .Select(s => s.Select(w => string.Join(string.Empty, w.OrderBy(c => c))))
                        .Count(s => s.Distinct().Count() == s.Count());

        System.Console.WriteLine($"Part 2: {part2}");
    }
}