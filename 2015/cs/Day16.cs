namespace Runner;

public class Day16
{
    public static void Run()
    {
        var Mfcsam = new Dictionary<string, int> {
            { "children", 3 },
            { "cats", 7} ,
            { "samoyeds", 2 },
            { "pomeranians", 3 },
            { "akitas", 0 },
            { "vizslas", 0 },
            { "goldfish", 5 },
            { "trees", 3 },
            { "cars", 2 },
            { "perfumes", 1}};

        var lines = File.ReadAllLines("../inputs/day16").Select(l => l.Replace(":", "").Replace(",", ""));

        foreach (var line in lines)
        {
            var splits = line.Split(' ');
            if (Mfcsam[splits[2]] == int.Parse(splits[3])
             && Mfcsam[splits[4]] == int.Parse(splits[5])
             && Mfcsam[splits[6]] == int.Parse(splits[7]))
            {
                System.Console.WriteLine($"Part 1: {line}");
            }
        }

        foreach (var line in lines)
        {
            var splits = line.Split(' ');
            if (Retroencabulator(Mfcsam, splits[2], int.Parse(splits[3]))
             && Retroencabulator(Mfcsam, splits[4], int.Parse(splits[5]))
             && Retroencabulator(Mfcsam, splits[6], int.Parse(splits[7])))
            {
                System.Console.WriteLine($"Part 2: {line}");
            }
        }

        bool Retroencabulator(Dictionary<string, int> d, string s, int i)
        {
            if (s == "cats" || s == "trees")
            {
                return i > d[s];
            }

            if (s == "pomeranians" || s == "goldfish")
            {
                return i < d[s];
            }

            return i == d[s];
        }
    }
}