
namespace AocRunner;

public class Day1
{
    public static void Run(string input, string[] lines)
    {
        foreach (var p in Positions(lines))
        {
            Console.WriteLine(p);
        }

        var day1 = Positions(lines).Count(p => p == 0);
        Console.WriteLine($"Day 1: {day1}");    
    }

    public static IEnumerable<int> Positions(string[] lines)
    {
        var p = 50;
        yield return p;

        foreach (var line in lines)
        {
            p += Move(line);
            while (p < 0) p += 100;
            while (p >= 100) p -= 100;
            yield return p;
        }

        static int Move(string l)
        {
            var distance = int.Parse(l[1..]);
            return l[..1] == "L" ? -distance : distance;
        }
    }
}