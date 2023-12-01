namespace AocRunner;

public class Day1
{
    public static void Run(string input, string[] lines)
    {
        System.Console.WriteLine($"Part 1: {GetNumbers(lines).Sum()}");
        System.Console.WriteLine($"Part 2: {GetNumbers(lines.Select(line => Replace(line))).Sum()}");

        static IEnumerable<int> GetNumbers(IEnumerable<string> lines)
        {
            return lines.Select(line => line.Select(c => c.ToString()).Where(s => int.TryParse(s, out _)))
                            .Select(ints => ints.First() + ints.Last())
                            .Select(x => int.Parse(x));
        }

        static string Replace(string input)
        {
            return input.Replace("one", "o1ne")         // this ensures that a string like oneeight4 is parsed as 184
                        .Replace("two", "t2wo")         // which leads to the right solution
                        .Replace("three", "t3hree")     // even though the problem statement does not mention that this
                        .Replace("four", "f4our")       // is the intended behaviour
                        .Replace("five", "f5ive")       
                        .Replace("six", "s6ix")
                        .Replace("seven", "s7even")
                        .Replace("eight", "e8ight")
                        .Replace("nine", "n9ine");
        }
    }
}