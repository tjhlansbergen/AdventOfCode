using System.Text;

namespace AocRunner;

public class Day3
{
    public static void Run(string input, string[] lines)
    {
        var part1 = lines.Sum(Joltage);
        Console.WriteLine($"Part 1: {part1}");

        var part2 = lines.Sum(Joltage2);
        Console.WriteLine($"Part 2: {part2}");
    }

    public static int Joltage(string bank)
    {
        var batts = bank.Select(c => c - '0').ToList();
        
        var a = batts[..^1].OrderByDescending(b => b).First();
        var b = batts[(batts.IndexOf(a) + 1)..].OrderByDescending(b => b).First();
        return int.Parse(string.Join("", [a, b]));
    }

    public static long Joltage2(string bank)
    {
        var batts = bank.Select(c => c - '0').ToList();

        var result = new StringBuilder();

        for (int i = 0; i < 12; i++)
        {
            var peek = batts[..^(11-i)];
            var a = peek.OrderByDescending(b => b).First();
            batts = batts[(batts.IndexOf(a) + 1)..];
            result.Append(a);
        }

        return long.Parse(result.ToString());
    }
}