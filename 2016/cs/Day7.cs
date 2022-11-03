using System.Text;

namespace AocRunner;

public class Day7
{
    public static void Run(string input, string[] lines)
    {
        var r = lines.Select(l => Parse(l))
             .Count(parts => parts.Any(p => ContainsAbba(p.Key) && !p.Value) 
                    && !parts.Any(p => ContainsAbba(p.Key) && p.Value));

        System.Console.WriteLine($"Part 1: {r}");

        // helpers
        List<KeyValuePair<string, bool>> Parse(string s)
        {
            var result = new List<KeyValuePair<string, bool>>();    // true denotes its a hypernet sequence
            var buffer = new StringBuilder();

            foreach (var c in s)
            {
                if (new[]{'[', ']'}.Contains(c))
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
    }
}