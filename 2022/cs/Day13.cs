using Newtonsoft.Json.Linq;

namespace AocRunner;

public class Day13
{
    public static void Run(string input, string[] lines)
    {
        // this one turned out well :))

        var c = new Day13Comparer();

        var pairs = lines.Where(l => !string.IsNullOrWhiteSpace(l))
                        .Select(l => JArray.Parse(l))
                        .Chunk(2)
                        .Select((chunk, i) => new { Id = i + 1, Pair = chunk })
                        .ToList();

        var part1 = pairs.Where(p => c.Compare(p.Pair[0], p.Pair[1]) == -1)
                         .Sum(p => p.Id);

        System.Console.WriteLine($"Part 1: {part1}");

        var packets = lines.Where(l => !string.IsNullOrWhiteSpace(l))
                           .Select(l => JArray.Parse(l))
                           .Select(p => new { Div = false, Packet = p })
                           .ToList();

        packets.Add(new { Div = true, Packet = JArray.Parse("[[2]]") });
        packets.Add(new { Div = true, Packet = JArray.Parse("[[6]]") });

        var part2 = packets.OrderBy(p => p.Packet, c)
                           .Select((p, i) => new { Id = i + 1, p.Div })
                           .Where(x => x.Div)
                           .Select(x => x.Id)
                           .Aggregate((a, x) => a * x);

        System.Console.WriteLine($"Part 2: {part2}");
    }

    class Day13Comparer : IComparer<JToken>
    {
        public int Compare(JToken? left, JToken? right)
        {
            if (left == null || right == null) throw new NullReferenceException("Left or right was NULL when comparing");

            if (left.Type == JTokenType.Integer && right.Type == JTokenType.Integer)
            {
                // direct comparison
                if ((int)left == (int)right) return 0;
                return ((int)right > (int)left) ? -1 : 1;
            }

            if (left.Type == JTokenType.Integer && right.Type == JTokenType.Array)
            {
                left = new JArray { (int)left };
                return Compare(left, right);
            }
            if (left.Type == JTokenType.Array && right.Type == JTokenType.Integer)
            {
                right = new JArray { (int)right };
                return Compare(left, right);
            }

            if ((left.Type == JTokenType.Array && right.Type == JTokenType.Array))
            {
                var len = Math.Min(left.Count(), right.Count());

                for (int i = 0; i < len; i++)
                {
                    var result = Compare(left[i]!, right[i]!);
                    if (result != 0) return result;
                }

                if (left.Count() == right.Count()) return 0;
                return (left.Count() < right.Count()) ? -1 : 1;
            }

            throw new InvalidOperationException($"Compare failed, left: {left.Type}, right: {right.Type}");
        }
    }
}

