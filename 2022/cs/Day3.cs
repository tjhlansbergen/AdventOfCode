namespace AocRunner;

public class Day3
{
    public static void Run(string input, string[] lines)
    {
        var part1 = lines.Select(l => new { Left = l.Substring(0, l.Length / 2), Right = l.Substring(l.Length / 2, l.Length / 2)})
                         .Select(pair => pair.Left.First(c => pair.Right.Contains(c)))
                         .Select(x => Char.IsLower(x) ? (int)x - 96 : (int)x - 38)
                         .Sum();

        System.Console.WriteLine($"Part 1: {part1}");


        var part2 = lines.Chunk(3)
                         .Select(ch => ch[0].First(c => ch[1].Contains(c) && ch[2].Contains(c)))
                         .Select(x => Char.IsLower(x) ? (int)x - 96 : (int)x - 38)
                         .Sum();

        System.Console.WriteLine($"Part 2: {part2}");

    }
}