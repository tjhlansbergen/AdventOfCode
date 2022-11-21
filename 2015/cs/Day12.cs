using Newtonsoft.Json.Linq;

namespace Runner;
public class Day12
{
    public static void Run()
    {
        var json = File.ReadAllText("../inputs/day12");
        var jObject = JObject.Parse(json);

        var part1 = jObject.DescendantsAndSelf().Where(c => c.Type == JTokenType.Integer).Select(j => (int)j).Sum();
        System.Console.WriteLine($"Part 1: {part1}");

        var part2 = jObject.DescendantsAndSelf()
            .Where(t => !t.AncestorsAndSelf().Any(a => a.Type == JTokenType.Object && IsRed((JObject)a)))
            .Where(c => c.Type == JTokenType.Integer)
            .Select(j => (int)j)
            .Sum();

        System.Console.WriteLine($"Part 2: {part2}");
    }

    public static bool IsRed(JObject jobject)
    {
        return jobject.Properties()
            .Select(p => p.Value).OfType<JValue>()
            .Select(j => j.Value).OfType<string>()
            .Any(j => j == "red");
    }
}