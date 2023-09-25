using System;

namespace AocRunner;

public class Day2
{
    public static void Run(string input, string[] lines)
    {
        // part 1
        var intcode = PrepInput(input, 12, 2);
        var result = Process(intcode);

        System.Console.WriteLine($"Part 1: {result}");

        // part 2
        for (int v = 0; v <= 99; v++)
        {
            for (int n = 0; n <= 99; n++)
            {
                intcode = PrepInput(input, n, v);
                result = Process(intcode);

                if (result == 19690720)
                {
                    System.Console.WriteLine($"Part 2: {100 * n + v}");
                    Environment.Exit(0);
                }
            }
        }
    }

    public static int Process(List<int> intcode)
    {
        var position = 0;
        var result = new List<int>(intcode);

        while (true)
        {
            if (result[position] == 99) break;
            if (!new[] { 1, 2 }.Contains(result[position])) throw new ArgumentException();

            result[result[position + 3]] = result[position] == 1
                    ? result[result[position + 1]] + result[result[position + 2]]
                    : result[result[position + 1]] * result[result[position + 2]];
            position += 4;
        }

        return result[0];
    }

    public static List<int> PrepInput(string input, int noun, int verb)
    {
        var l = input.Split(',').Select(c => int.Parse(c)).ToList();
        l[1] = noun;
        l[2] = verb;
        return l;
    }
}