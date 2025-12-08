
namespace AocRunner;

public class Day1
{
    public static void Run(string input, string[] lines)
    {
        var part1 = EndPositions(lines).Count(p => p == 0);
        Console.WriteLine($"Part 1: {part1}");

        var part2 = AllPositions(lines);
        Console.WriteLine($"Part 2: {part2}");
    }

    public static int AllPositions(string[] lines)
    {
        var p = 50;
        var count = 0;

        foreach (var line in lines)
        {
            var move = Move(line);
            for (int i = 0; i < Math.Abs(move); i++)
            {
                p = Rotate(p, move < 0);
                if (p == 0) count++;
            }   
        }


        return count;
    }

    static int Rotate(int p, bool left)
    {
        if (left)
        {
            p--;
            if (p < 0) p = 99;
            return p;
        }
        else
        {
            p++;
            if (p >= 100) p = 0;
            return p;
        }
    }

    public static IEnumerable<int> EndPositions(string[] lines)
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
    }

    private static int Move(string l)
    {
        var distance = int.Parse(l[1..]);
        return l[..1] == "L" ? -distance : distance;
    }
}