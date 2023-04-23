namespace AocRunner;

public class Day5
{
    public static void Run(string input, string[] lines)
    {
        var instructions = lines.Select(x => Int16.Parse(x)).ToArray();
        var length = instructions.Length;
        var i = 0;
        var count = 0;

        while (i < length)
        {
            var last = i;
            i += instructions[i];
            instructions[last]++;
            count++;
        }

        System.Console.WriteLine($"Part 1: {count}");

        instructions = lines.Select(x => Int16.Parse(x)).ToArray();
        length = instructions.Length;
        i = 0;
        count = 0;

        while (i < length)
        {
            var last = i;
            i += instructions[i];
            if (instructions[last] > 2) { instructions[last]--; } else { instructions[last]++; }
            count++;
        }

        System.Console.WriteLine($"Part 2: {count}");
    }
}