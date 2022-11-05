using System.Text;

namespace AocRunner;

public class Day7
{
    public static void Run(string input, string[] lines)
    {
        var one = lines.Select(l => Parse(l))
             .Count(parts => parts.Any(p => ContainsAbba(p.Key) && !p.Value)
                    && !parts.Any(p => ContainsAbba(p.Key) && p.Value));

        System.Console.WriteLine($"Part one: {one}");

        var two = lines.Select(l => Parse(l))
            .Where(parts => parts.Any(p => !p.Value && ABAs(p.Key, false).Any()))
            .Select(a => new { ParsedLine = a, ABAs = a.Where(part => !part.Value).SelectMany(part => ABAs(part.Key, false))})
            .Count(aba => aba.ParsedLine.Where(p => p.Value).SelectMany(p => ABAs(p.Key, true)).Intersect(aba.ABAs).Any());

        System.Console.WriteLine($"Part two: {two}");


        // helpers
        List<KeyValuePair<string, bool>> Parse(string s)
        {
            var result = new List<KeyValuePair<string, bool>>();    // true denotes its a hypernet sequence
            var buffer = new StringBuilder();

            foreach (var c in s)
            {
                if (new[] { '[', ']' }.Contains(c))
                {
                    if (buffer.Length > 0) result.Add(new KeyValuePair<string, bool>(buffer.ToString(), c == ']'));
                    buffer.Clear();
                    continue;
                }

                buffer.Append(c);
            }
            // dont forget the remainder
            if (buffer.Length > 0) result.Add(new KeyValuePair<string, bool>(buffer.ToString(), false));

            return result;
        }

        bool ContainsAbba(string s)
        {
            if (s.Length < 4) return false;

            for (int i = 0; i < s.Length - 3; i++)
            {
                var block = s.Substring(i, 4);
                if (block[0] == block[3] && block[1] == block[2] && block[0] != block[1])
                {
                    return true;
                }
            }
            return false;
        }

        IEnumerable<string> ABAs(string s, bool reverse)
        {
            var result = new List<string>();
            if (s.Length < 3) return result;

            for (int i = 0; i < s.Length - 2; i++)
            {
                var block = s.Substring(i, 3);
                if (block[0] == block[2] && block[1] != block[2])
                {
                    if (reverse)
                    {
                        result.Add($"{block[1]}{block[0]}{block[1]}");
                    }
                    else
                    {
                        result.Add(block);
                    }
                }
            }

            return result;
        }
    }
}