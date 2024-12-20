
namespace AocRunner;

public class Day1
{
    public static void Run(string input, string[] lines)
    {
        var parsed = lines.Select(l => l.Split("   ")).Select(split => new { Left = int.Parse(split[0]), Right = int.Parse(split[1]) }).ToList();
        var left = parsed.Select(p => p.Left).OrderByDescending(x => x).ToList();
        var right = parsed.Select(p => p.Right).OrderByDescending(x => x).ToList();

        var part1 = left.Zip(right).Select(p => Math.Abs(p.First - p.Second)).Sum();

        System.Console.WriteLine($"Part 1: {part1}");   

        var part2 = left.Select(l => l * right.Count(r => r == l)).Sum();

        System.Console.WriteLine($"Part 2: {part2}");   
    
    }
}