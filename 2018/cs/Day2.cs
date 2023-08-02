namespace AocRunner;

public class Day2
{
    public static void Run(string input, string[] lines)
    {
        var part1 = lines.Count(l => Sames(l, 2)) * lines.Count(l => Sames(l, 3));
        System.Console.WriteLine($"Part 1: {part1}");


        // yikes... it aint' stupid if it works :)
        var done = false;
        foreach (var a in lines)
        {
            foreach (var b in lines)
            {
                var result = Differ(a, b);
                if (result.Item1 == 1)
                {
                    System.Console.WriteLine($"Part 2: {a.Remove(result.Item2, 1)}");
                    done = true;
                    break;
                }
            }
            
            if (done) break;
        }

        bool Sames(string input, int howMany)
        {
            return input.GroupBy(x => x).Any(gr => gr.Count() == howMany);
        }

        (int, int) Differ(string a, string b)
        {
            if (a.Length != b.Length) throw new ArgumentException();

            var c = 0;
            var i = 0;
            for(int j = 0; j < a.Length; j++)
            {
                if (a[j] != b[j]) 
                {
                    c++;
                    i = j;
                }
            }

            return (c, i);
        }
    }
}