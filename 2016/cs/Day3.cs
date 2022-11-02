namespace AocRunner;

public class Day3
{
    public static void Run(string input, string[] lines)
    {
        // part 1
        System.Console.WriteLine(lines.Count(l =>
            Possible(l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(c => int.Parse(c)).ToArray())));

        // part 2
        List<int> a = new List<int>(), b = new List<int>(), c = new List<int>();
        foreach (var line in lines)
        {
            var splits = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(c => int.Parse(c)).ToArray();
            a.Add(splits[0]);
            b.Add(splits[1]);
            c.Add(splits[2]);
        }
        System.Console.WriteLine(a.Concat(b).Concat(c).Chunk(3).Count(c => Possible(c)));

        // helper

        bool Possible(int[] sides)
        {
            int a = sides[0], b = sides[1], c = sides[2];
            if (a + b <= c) return false;
            if (a + c <= b) return false;
            if (b + c <= a) return false;
            return true;
        }
    }
}