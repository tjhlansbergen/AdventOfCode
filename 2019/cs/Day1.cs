namespace AocRunner;

public class Day1
{
    public static void Run(string input, string[] lines)
    {
        var part1 = lines.Select(l => int.Parse(l))
                         .Select(x => Fuel(x))
                         .Sum();

        System.Console.WriteLine($"Part 1: {part1}");

        var part2 = lines.Select(l => int.Parse(l))
                         .Select(x => CumulativeFuel(x))
                         .Sum();

        System.Console.WriteLine($"Part 2: {part2}");
    }

    private static int CumulativeFuel(int mass)
    {
        var c = 0;
        var x = mass;

        while (x > 0)
        {
            x = Fuel(x);
            c+= x;
        }

        return c;
    }

    private static int Fuel(int mass)
    {
        var f = (mass / 3) - 2;
        return f > -1 ? f : 0;
    }
}