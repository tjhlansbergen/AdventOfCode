namespace AocRunner;

public class Day1
{
    public static void Run(string input, string[] lines)
    {
        var part1 = lines.Select(l => int.Parse(l)).Sum();
        Console.WriteLine($"Part 1: {part1}");

        var changes = lines.Select(l => int.Parse(l)).ToArray();
        var freqs = new HashSet<int>();
        var freq = 0;
        var index = 0;

        while(true)
        {
            freq += changes[index];
            
            if (!freqs.Add(freq))
            {
                System.Console.WriteLine($"Part 2: {freq}");
                break;
            }

            index += 1;
            if (index == changes.Length) index = 0;
        }
    }
}