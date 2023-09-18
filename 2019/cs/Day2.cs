using System;

namespace AocRunner;

public class Day2
{
    public static void Run(string input, string[] lines)
    {
        var intcode = PrepInput(input);
        var result = Process(intcode);

        System.Console.WriteLine(string.Join(',', result));
    }

    public static List<int> Process(List<int> intcode)
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

        return result;
    }

    public static List<int> PrepInput(string input)
    {
        var l = input.Split(',').Select(c => int.Parse(c)).ToList();
        l[1] = 12;
        l[2] = 2;
        return l;
    }
}