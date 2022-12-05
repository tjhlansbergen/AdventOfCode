namespace AocRunner;

public class Day5
{
    public static void Run(string input, string[] lines)
    {
        // parse starting crates
        var stacks = lines.Where(l => l.Contains('['))
                          .SelectMany(l => Enumerable.Range(1, (l.Length + 1)/4).Select(r => new { StackId = r, Value = l[r * 4 - 3]}))
                          .Where(c => !char.IsWhiteSpace(c.Value))
                          .GroupBy(x => x.StackId)
                          .ToDictionary(x=> x.Key, x => new Stack<char>(x.Select(v => v.Value).Reverse())); 

        // parse instructions
        var moves = lines.Where(l => l.StartsWith("move"))
                         .Select(l => l.Split(' ').Where(s => int.TryParse(s, out _)).Select(i => int.Parse(i)).ToArray())
                         .Select(m => new { Amount = m[0], From = m[1], To = m[2]});

        // operate instructions
        foreach (var move in moves)
        {
            for (int i = 0; i < move.Amount; i++)
            {
                stacks[move.To].Push(stacks[move.From].Pop());
            }
        }

        var part1 = string.Join("", stacks.OrderBy(s => s.Key).Select(s => s.Value.Peek()).ToArray());
        Console.WriteLine($"Part 1: {part1}");


        // reset
        var lists = lines.Where(l => l.Contains('['))
                          .SelectMany(l => Enumerable.Range(1, (l.Length + 1)/4).Select(r => new { StackId = r, Value = l[r * 4 - 3]}))
                          .Where(c => !char.IsWhiteSpace(c.Value))
                          .GroupBy(x => x.StackId)
                          .ToDictionary(x=> x.Key, x => new List<char>(x.Select(v => v.Value))); 

        // operate instructions
        foreach (var move in moves)
        {
            var range = lists[move.From].Take(move.Amount).ToArray();
            lists[move.From].RemoveRange(0, move.Amount);
            lists[move.To].InsertRange(0, range);
        }

        var part2 = string.Join("", lists.OrderBy(s => s.Key).Select(s => s.Value.First()).ToArray());
        Console.WriteLine($"Part 2: {part2}");
    }
}