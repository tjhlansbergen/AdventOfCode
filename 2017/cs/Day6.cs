namespace AocRunner;

public class Day6
{
    public static void Run(string input, string[] lines)
    {
        var current = input.Split('\t').Select(s => int.Parse(s)).ToList();
        var size = current.Count;
        
        var results = new HashSet<string>();

        while (results.Add(string.Join('.', current))) 
        {
            var maxValue = current.Max();
            var maxBlock = current.First(block => block == maxValue);
            var maxBlockIndex = current.IndexOf(maxBlock);

            current[maxBlockIndex] = 0;
            int i = maxBlockIndex;

            for (int c = 0; c < maxValue; c++)
            {
                i = Next(i, size);
                current[i]++;
            }
        }

        System.Console.WriteLine($"Part 1: {results.Count}" );
        System.Console.WriteLine($"Part 2: {results.Count - results.ToList().IndexOf(results.Single(r => r == string.Join('.', current)))}" );
    }

    public static int Next(int i, int size) => (i == size - 1) ? 0 : i+1;
}