namespace AocRunner;

public class Day4
{
    public static void Run(string input, string[] lines)
    {
        var ranges = lines.Select(l => l.Split(','))
                          .Select(s => s.Select(r => r.Split('-')
                                        .Select(c => int.Parse(c))))
                          .Select(x => new
                          {
                              Left = Enumerable.Range(x.First().First(), x.First().Last() - x.First().First() + 1),
                              Right = Enumerable.Range(x.Last().First(), x.Last().Last() - x.Last().First() + 1)
                          });

        var part1 = ranges.Select(r => new { r.Left, r.Right, I = r.Left.Intersect(r.Right) })
                          .Count(i => Enumerable.SequenceEqual(i.Left, i.I) || Enumerable.SequenceEqual(i.Right, i.I));

        System.Console.WriteLine($"Part 1: {part1}");

        var part2 = ranges.Count(r => r.Left.Intersect(r.Right).Any());

        System.Console.WriteLine($"Part 2: {part2}");
    }
}