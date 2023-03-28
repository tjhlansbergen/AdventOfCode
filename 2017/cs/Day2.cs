namespace AocRunner;

public class Day2
{
    public static void Run(string input, string[] lines)
    {
        var pt1 = lines.Select(l => l.Split().Select(c => int.Parse(c)))
                        .Sum(l => l.Max() - l.Min());

        var desc = lines.Select(l => l.Split().Select(c => int.Parse(c)).OrderByDescending(x => x).ToArray());

        var lineResult = new List<int>();
        foreach (var line in desc)
        {
            for (int i = 0; i < line.Count() - 1; i++)
            {
                for (int j = i + 1; j < line.Count(); j++)
                {
                    if (line[i] % line[j] == 0) 
                    {
                        lineResult.Add(line[i] / line[j]);
                        break;
                    }
                }
            }
        }
        
        System.Console.WriteLine($"Part 1: {pt1}");
        System.Console.WriteLine($"Part 2: {lineResult.Sum()}");
    }
}