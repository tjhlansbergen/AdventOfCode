namespace AocRunner;

public class Day6
{
    public static void Run(string input, string[] lines)
    {
        for (int i = 3; i < input.Length; i++)
        {
            if (new[] { input[i], input[i - 1], input[i - 2], input[i - 3] }.Distinct().Count() == 4)
            {
                System.Console.WriteLine($"Part 1: {i + 1}");
                break;
            }
        }

        for (int i = 0; i < input.Length - 14; i++)
        {
            var test = input.Substring(i, 14);

            if(input.Substring(i, 14).Distinct().Count() == 14)
            {
                System.Console.WriteLine($"Part 2: {i + 14}");
                return;
            }
        }
    }
}