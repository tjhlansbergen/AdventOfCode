namespace Runner;

public class Day14
{
    public static void Run()
    {
        var lines = File.ReadAllLines("../inputs/day14");

        var part1 = lines.Select(l => l.Split(' '))
                            .Select(s => Calc(int.Parse(s[3]), int.Parse(s[6]), int.Parse(s[13]), 2503))
                            .Max();

        System.Console.WriteLine($"Part 1: {part1}");

        // this is the lazy inefficient way I guess
        var splitLines = lines.Select(l => l.Split(' ')).ToArray();
        var results = splitLines.Select(s => s[0]).ToDictionary(s => s, s => 0);

        for (int i = 1; i <= 2503; i++)
        {
            var winners = splitLines.Select(s => new { Score = Calc(int.Parse(s[3]), int.Parse(s[6]), int.Parse(s[13]), i), Name = s[0] })
                    .GroupBy(s => s.Score)
                    .OrderByDescending(gr => gr.Key)    // where key is the score
                    .First()
                    .Select(i => i.Name)
                    .ToList();

            foreach (var winner in winners) { results[winner]++; }
        }

        var part2 = results.Values.Max();
        System.Console.WriteLine($"Part 2: {part2}");

        int Calc(int speed, int fly, int rest, int duration)
        {
            return (duration % (fly + rest) <= fly) ?
                ((duration / (fly + rest)) * fly * speed) + ((duration % (fly + rest)) * speed) :
                ((duration / (fly + rest)) * fly * speed) + (fly * speed);
        }
    }
}