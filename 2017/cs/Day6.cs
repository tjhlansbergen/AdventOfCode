namespace AocRunner;

public class Day6
{
    public static void Run(string input, string[] lines)
    {
        // NON-OPTIMIZED! :)

        var blocks = input.Split('\t').Select(s => int.Parse(s)).ToList();
        var size = blocks.Count;
        
        var results = new List<List<int>> { blocks };

        do {
            var last = new List<int>(results.Last());

            var maxValue = last.Max();
            var maxBlock = last.First(block => block == maxValue);
            var maxBlockIndex = last.IndexOf(maxBlock);

            last[maxBlockIndex] = 0;
            int i = maxBlockIndex;

            for (int c = 0; c < maxValue; c++)
            {
                i = Next(i, last.Count);
                last[i]++;
            }

            results.Add(last);
        } while (results.Count == results.Select(r => string.Join('.', r)).Distinct().Count());

        System.Console.WriteLine($"Part 1: {results.Count - 1}" );

        var final = string.Join('.', results.Last());
        var first = results.IndexOf(results.First(r => string.Join('.', r) == final));

        System.Console.WriteLine($"Part 2: {results.Count - 1 - first}" );
    }

    public static int Next(int i, int size) => (i == size - 1) ? 0 : i+1;
}