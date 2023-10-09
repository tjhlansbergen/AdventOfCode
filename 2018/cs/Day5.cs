using System;

namespace AocRunner;

public class Day5
{
    public static void Run(string input, string[] lines)
    {
        var result = new Stack<char>();


        static bool React(IEnumerable<char> units)
        {
            return units.Distinct().Count() == 2
            && units.Select(char.ToLower).Distinct().Count() == 1;
        }

        //part 1
        for (int i = 0; i < input.Length; i++)
        {
            result.Push(input[i]);

            if (React(result.Take(2)))
            {
                result.Pop();
                result.Pop();
            }
        }

        System.Console.WriteLine($"Part 1: {result.Count}");

        // part 2
        var count = long.MaxValue;

        foreach (char c in input.Select(i => char.ToLower(i)).Distinct())
        {
            result.Clear();

            for (int i = 0; i < input.Length; i++)
            {
                if (char.ToLower(input[i]) == c) continue;

                result.Push(input[i]);

                if (React(result.Take(2)))
                {
                    result.Pop();
                    result.Pop();
                }
            }

            count = Math.Min(count, result.Count);
        }



        System.Console.WriteLine($"Part 2: {count}");
    }
}