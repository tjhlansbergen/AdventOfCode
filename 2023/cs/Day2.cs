namespace AocRunner;

public class Day2
{
    public static void Run(string input, string[] lines)
    {
        var games = lines.Select(l => ParseLine(l));
        var bag = new Dictionary<string, int> {
            {"red", 12},
            {"green", 13},
            {"blue", 14},
        };

        var part1 = games.Select(game =>
            game.Value.All(set =>
                set.All(x => x.Value <= bag[x.Key])
            )
            ? game.Key
            : 0
        ).Sum();

        var part2 = games.Select(game =>
            game.Value
                .SelectMany(set => set)
                .GroupBy(set => set.Key)
                .Select(group => group.Max(color => color.Value))
                .Aggregate((a, b) => a * b)
        ).Sum();

        System.Console.WriteLine($"Part 1: {part1}");
        System.Console.WriteLine($"Part 2: {part2}");
    }

    private static KeyValuePair<int, IEnumerable<Dictionary<string, int>>> ParseLine(string line)
    {
        var parts = line.Split(':');
        var key = int.Parse(parts[0].Replace("Game ", string.Empty));

        var sets = parts[1].Split(';').Select(set =>
                set.Split(',')
                .Select(s =>
                    s.Trim()
                    .Split(' '))
                    .Select(x => new { Color = x[1], Count = int.Parse(x[0]) })
                    .ToDictionary(x => x.Color, x => x.Count)
            );

        return new KeyValuePair<int, IEnumerable<Dictionary<string, int>>>(key, sets);
    }
}