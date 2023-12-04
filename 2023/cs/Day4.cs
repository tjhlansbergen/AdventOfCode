namespace AocRunner;

public class Day4
{
    public static void Run(string input, string[] lines)
    {
        var cards = lines.Select(line => ParseLine(line));
        var part1 = cards.Select(card => Math.Floor(Math.Pow(2,card.Last().Count(i => card.First().Contains(i))-1))).Sum();

        System.Console.WriteLine($"Part 1: {part1}");
    }

    private static IEnumerable<IEnumerable<int>> ParseLine(string line)
    {
        return line.Split(':', StringSplitOptions.TrimEntries)[1]
                        .Split('|', StringSplitOptions.TrimEntries)
                        .Select(part => part.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)));
        
    }
}