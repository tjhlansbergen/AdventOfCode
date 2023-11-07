namespace AocRunner;

public class Day6
{
    public static void Run(string input, string[] lines)
    {
        var blocks = input.Split('\t').Select(s => int.Parse(s)).ToArray();
        var size = blocks.Count();
        
        var results = new List<int[]> { blocks };

        do {
            var last = results.Last();
            var max = last.Max();
            var dist = last.First(block => block == max);
            
            
        } while (true);

    }

    public int Next(int i, int size)
    {
        return (i == size - 1) ? 0 : i++;
    }
}