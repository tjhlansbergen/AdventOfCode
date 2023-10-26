using System;

namespace AocRunner;

public class Day7
{
    public static void Run(string input, string[] lines)
    {
        var steps = new Dictionary<char, List<char>>();

        foreach (var line in lines)
        {
            var required = line[5];
            var step = line[36];

            if (steps.ContainsKey(step))
                steps[step].Add(required);
            else
                steps.Add(step, new List<char> { required });

            if (!steps.ContainsKey(required))
                steps.Add(required, new List<char>());
        }

        var result = new List<char>();

        System.Console.Write("Part 1: ");

        while (result.Count != steps.Count)
        {
            var next = steps.Where(s => !result.Contains(s.Key) && !s.Value.Any())
            .Select(s => s.Key)
            .Min();

            result.Add(next);
            foreach (var key in steps.Keys)
            {
                steps[key].Remove(next);
            }

            System.Console.Write(next);

        }

        System.Console.WriteLine();
    }
}