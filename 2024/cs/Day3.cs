using System.Text;

namespace AocRunner;

public class Day3
{
    public static void Run(string input, string[] lines)
    {
        var part1 = Multiply(input);
        System.Console.WriteLine($"Part 1: {part1}");

        var sanitized = Sanitize(input);
        
        var part2 = Multiply(sanitized);
        System.Console.WriteLine($"Part 2: {part2}");
    }

    public static long Multiply(string input)
    {
        var muls = input.Split("mul(")
                .Select(m => m.Split(")")[0].Split(","))
                .Where(m => m.Length == 2)
                .Select(m => new { Left = int.TryParse(m[0], out int m0) ? (int?)m0 : null, Right = int.TryParse(m[1], out int m1) ? (int?)m1 : null })
                .Where(m => m.Left.HasValue && m.Right.HasValue)
                .Select(m => new { Left = m.Left!.Value, Right = m.Right!.Value });

        return muls.Sum(m => m.Left * m.Right);
    }

    public static string Sanitize(string input)
    {
        var doo = true;
        var result = new StringBuilder();

        for (int i = 0; i < input.Length; i++)
        {
            if (i < input.Length - 7)
            {
                if (input.Substring(i, 4) == "do()")
                {
                    doo = true;
                }
                if (input.Substring(i, 7) == "don't()")
                {
                    doo = false;
                }
            }

            if (doo)
            {
                result.Append(input[i]);
            }
        }

        return result.ToString();
    }
}