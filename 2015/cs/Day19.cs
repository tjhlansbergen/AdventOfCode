namespace AocRunner;

public class Day19
{
    public static void Run(string input, string[] lines)
    {
        lines = lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

        var replacements = lines.Where(l => l.Contains("=>"))
                        .Select(l => l.Split("=>", StringSplitOptions.TrimEntries))
                        .Select(splits => new { From = splits[0], To = splits[1]})
                        .GroupBy(x => x.From)
                        .ToDictionary(gr => gr.Key, gr => gr.Select(x => x.To).ToArray());

        var original = lines.Where(l => !l.Contains("=>")).Single();
        var length = original.Length;

        var results = new HashSet<string>();

        for (int i = 0; i < length; i++)
        {
            IEnumerable<dynamic> candidates;

            if (i == length - 1)
            {
                // for the last char only, read that
                candidates = new [] { new { Key = original.Substring(i, 1), Len = 1 } };    
            }
            else
            {
                // read strings of lenght 1 & 2
                candidates = new [] { new { Key = original.Substring(i, 1), Len = 1 }, new { Key = original.Substring(i, 2), Len = 2 } };
            }
            
            
            foreach (var candidate in candidates)
            {
                if (replacements.ContainsKey(candidate.Key))
                {
                    foreach(var rep in replacements[candidate.Key])
                    {
                        var copy = original.Remove(i, candidate.Len);
                        results.Add(copy.Insert(i, rep));
                    }
                }
            }
        }

        System.Console.WriteLine($"Part 1: {results.Count}");
    }
}