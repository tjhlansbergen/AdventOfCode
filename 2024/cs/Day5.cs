using System.Data;

namespace AocRunner;

public class Day5
{
    public static void Run(string input, string[] lines)
    {
        var rules = lines.Where(l => l.Contains('|')).Select(l => l.Split('|').Select(p => int.Parse(p)));
        var updates = lines.Where(l => l.Contains(',')).Select(l => l.Split(',').Select(p => int.Parse(p)));

        var goodUpdates = updates.Where(u => rules.All(r => ProofRule(r, u)));

        var part1 = goodUpdates.Sum(u => u.ToArray()[u.Count() / 2]);
        Console.WriteLine($"Part1: {part1}");
    }

    private static bool ProofRule(IEnumerable<int> rule, IEnumerable<int> update)
    {
        if (!update.Contains(rule.First()))
        {
            return true;
        }
        if (!update.Contains(rule.Last()))
        {
            return true;
        }

        if (update.ToList().IndexOf(rule.First()) < update.ToList().IndexOf(rule.Last()))
        {
            return true;
        }

        return false;
    }
}