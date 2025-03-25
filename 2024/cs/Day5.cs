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

        var badUpdates = updates.Where(u => rules.Any(r => !ProofRule(r, u)));
        var correctedUpdates = new List<IEnumerable<int>>();

        foreach (var badUpdate in badUpdates)
        {
            var correctedUpdate = badUpdate;
            
            do {
                foreach (var rule in rules)
                {
                    if (!ProofRule(rule, correctedUpdate))
                    {
                        correctedUpdate = ApplyRule(correctedUpdate, rule);
                        break;
                    }
                }
            } while (rules.Any(r => !ProofRule(r, correctedUpdate)));

            correctedUpdates.Add(correctedUpdate);
        }

        var part2 = correctedUpdates.Sum(u => u.ToArray()[u.Count() / 2]);
        Console.WriteLine($"Part2: {part2}");
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

    private static IEnumerable<int> ApplyRule(IEnumerable<int> update, IEnumerable<int> rule)
    {
        var updateList = update.ToList();
        
        var pageToMove = updateList.IndexOf(rule.First());
        var page = updateList[pageToMove];

        updateList.RemoveAt(pageToMove);
        updateList.Insert(updateList.IndexOf(rule.Last()), page);

        return updateList;
    }
}