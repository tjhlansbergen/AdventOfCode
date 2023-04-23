namespace AocRunner;

public class Day1
{
    public static void Run(string input, string[] lines)
    {
        var part1 = lines.Select(l => int.Parse(l))
                         .Select(x => (x / 3) -2)
                         .Sum();

        System.Console.WriteLine($"Part 1: {part1}");
    }
}