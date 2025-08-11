namespace AocRunner;

public class Day7
{
    public static void Run(string input, string[] lines)
    {
        // part 1
        var part1 = lines.Select(l => Proof(l)).Sum();
        Console.WriteLine("Part 1: " + part1);
    }

    public static long Proof(string input)
    {
        var target = Parse(input).Item1;
        var numbers = Parse(input).Item2;

        var intermediates = new[] { numbers[0] };
        for (long i = 1; i < numbers.Length; i++)
        {
            if (intermediates.Length == 0)
            {
                break;
            }

            var next = new List<long>();
            foreach (var intermediate in intermediates)
            {
                var sum = intermediate + numbers[i];
                if (sum <= target)
                {
                    next.Add(sum);
                }

                var product = intermediate * numbers[i];
                if (product <= target)
                {
                    next.Add(product);
                }
            }

            intermediates = next.Distinct().ToArray();
        }

        if (intermediates.Any(x => x == target))
        {
            return target;
        }

        return 0;

        // Helper method to parse the input string into a target number and an array of integers.
        static (long, long[]) Parse(string input)
        {
            var parts = input.Replace(':', ' ').Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var ints = parts.Select(long.Parse).ToArray();
            return (ints[0], ints[1..]);
        }
    }
}