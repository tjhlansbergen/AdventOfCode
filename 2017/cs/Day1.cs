namespace AocRunner;

public class Day1
{
    public static void Run(string input, string[] lines)
    {
        int GetSum(string line, int shift) => 
            Enumerable.Range(0, line.Length).Where(i => input[i] == input[(i + shift < input.Length) ? i + shift : i + shift - input.Length])
                                            .Select(i => input[i] - '0')
                                            .Sum();
        
        System.Console.WriteLine($"Part 1: {GetSum(input, 1)}");
        System.Console.WriteLine($"Part 2: {GetSum(input, input.Length / 2)}");
    }
}