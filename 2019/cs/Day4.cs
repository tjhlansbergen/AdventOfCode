using System;

namespace AocRunner;

public class Day4
{
    public static void Run(string input, string[] lines)
    {
        var from = int.Parse(input.Split('-')[0]);
        var to = int.Parse(input.Split('-')[1]);

        var part1 = Enumerable.Range(from, to - from).Count(i => Proof(i));
        System.Console.WriteLine($"Part 1: {part1}");

        var part2 = Enumerable.Range(from, to - from).Count(i => Proof(i, true));
        System.Console.WriteLine($"Part 2: {part2}");
    }

    private static bool Proof(int pwd, bool two = false)
    {
        var str = pwd.ToString();
        var adjacent = false;

        for (int i = 0; i < 5; i++)
        {
            if (str[i] > str[i + 1]) return false;

            if (two)
            {
                if (str[i] == str[i + 1] &&
                    (i == 0 || str[i] != str[i - 1]) &&
                    (i == 4 || str[i] != str[i + 2]))
                {
                    adjacent = true;
                }
            }
            else
            {
                if (str[i] == str[i + 1]) adjacent = true;
            }

        }

        return adjacent;
    }
}
