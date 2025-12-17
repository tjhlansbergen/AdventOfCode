namespace AocRunner;

public class Day3
{
    public static void Run(string input, string[] lines)
    {
        var part1 = lines.Sum(Joltage);
        Console.WriteLine($"Part 1: {part1}");
    }

    public static int Joltage(string bank)
    {
        var batts = bank.Select(c => c - '0').ToList();
        
        var a = batts[..^1].OrderByDescending(b => b).First();
        var b = batts[(batts.IndexOf(a) + 1)..].OrderByDescending(b => b).First();
        return int.Parse(string.Join("", [a, b]));
    }
}