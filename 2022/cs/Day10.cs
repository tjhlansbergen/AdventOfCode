namespace AocRunner;

public class Day10
{
    public static void Run(string input, string[] lines)
    {
        var cycles = new List<int>{ 1 };

        foreach (var line in lines)
        {
            if (line == "noop")
            {
                cycles.Add(cycles.Last());
            }
            else
            {
                var v = int.Parse(line.Split(' ')[1]);
                cycles.Add(cycles.Last());
                cycles.Add(cycles.Last() + v);
            }
        }

        // for (int i = 0; i < cycles.Count(); i++)
        // {
        //     System.Console.WriteLine($"{i}: {cycles[i]}");
        // }

        // System.Console.WriteLine("\n---\n");

        System.Console.WriteLine($"Part 1: {Strenghts(cycles)}");

        int Strenghts(List<int> cycles)
        {
            var strengths = new List<int>();
            for (int i = 20; i <= 220; i+=40)
            {
                strengths.Add(i*cycles[i-1]);
            }
            return strengths.Sum();
        }
    }
}