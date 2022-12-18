using Newtonsoft.Json.Linq;

namespace AocRunner;

public class Day13
{
    public static void Run(string input, string[] lines)
    {
        var pairs = lines.Where(l => !string.IsNullOrWhiteSpace(l))
                        .Select(l => JArray.Parse(l))
                        .Chunk(2)
                        .Select((chunk, i) => new {Id = i+1, Pair = chunk})
                        .ToList();

        var part1 = pairs.Where(p => Compare(p.Pair[0], p.Pair[1]) == true)
                         .Sum(p => p.Id);

        System.Console.WriteLine($"Part 1: {part1}");

        bool? Compare(JToken left, JToken right)
        {
            if (left.Type == JTokenType.Integer && right.Type == JTokenType.Integer)
            {
                // direct comparison
                if ((int)left == (int)right) return null;
                return (int)right > (int)left;
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
                    if (result != null) return result;
                }
                
                if (left.Count() == right.Count()) return null;
                return left.Count() < right.Count();
            }
            
            throw new InvalidOperationException($"Compare failed, left: {left.Type}, right: {right.Type}");
        }
    }
}

