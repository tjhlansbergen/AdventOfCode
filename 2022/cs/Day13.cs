using Newtonsoft.Json.Linq;

namespace AocRunner;

public class Day13
{
    public static void Run(string input, string[] lines)
    {
        var pairs = lines.Where(l => !string.IsNullOrWhiteSpace(l))
                        .Select(l => JArray.Parse(l))
                        .Chunk(2)
                        .Select((chunk, i) => new {Id = i, Pair = chunk})
                        .ToList();

        foreach (var p in pairs)
        {
            System.Console.WriteLine($"{p.Id}: {Compare(p.Pair[0], p.Pair[1])}");
        }

        bool Compare(JToken left, JToken right)
        {
            if (left.Type == JTokenType.Integer && right.Type == JTokenType.Integer)
            {
                // direct comparison
                return (int)right > (int)left;
            }

            if (left.Type == JTokenType.Integer && right.Type == JTokenType.Array)
            {
                left = new JArray { (int)left };
            }
            if (left.Type == JTokenType.Array && right.Type == JTokenType.Integer)
            {
                right = new JArray { (int)right };
            }
            
            if ((left.Type == JTokenType.Array && right.Type == JTokenType.Array))
            {
                return Compare(left, right);
            }
            
            throw new InvalidOperationException($"Compare failed, left: {left.Type}, right: {right.Type}");
        }
    }
}

