namespace AocRunner;

public class Day3
{
    public static void Run(string input, string[] lines)
    {
        var fabric = new Dictionary<Tuple<int,int>, List<int>>();
        var parsedLines = lines.Select(l => Parse(l));

        foreach (var line in parsedLines)
        {
            for (int y = line.bounds[0]; y < line.bounds[2]; y++)
            {
                for (int x = line.bounds[1]; x < line.bounds[3]; x++)
                {
                    AddOrUpdate(fabric, new (x,y), line.id);
                }
            }
        }

        var part1 = fabric.Count(f => f.Value.Count() > 1);
        System.Console.WriteLine($"Part 1: {part1}");

        var ids = parsedLines.Select(p => p.id).ToDictionary(f => f, x => true);
        foreach (var f in fabric.Where(f => f.Value.Count() > 1).SelectMany(f => f.Value))
        {
            ids[f] = false;
        }

        var part2 = ids.Single(i => i.Value == true).Key;
        System.Console.WriteLine($"Part 2: {part2}");

        (int id, int[] bounds) Parse(string line)
        {
            var s = int.Parse(line.Split('@')[0].Trim().Replace('#', ' '));
            var p = line.Split('@')[1].Split(':')[0].Split(',').Select(x => int.Parse(x.Trim())).ToArray();
            var b = line.Split('@')[1].Split(':')[1].Split('x').Select(x => int.Parse(x.Trim())).ToArray();
            b[0] += p[0];
            b[1] += p[1];
            return (s, p.Concat(b).ToArray());
        }

        void AddOrUpdate(Dictionary<Tuple<int,int>, List<int>> dict, Tuple<int, int> key, int value)
        {
            if (dict.ContainsKey(key)) dict[key].Add(value);
            else dict.Add(key, new List<int>{ value });
        }
    }
}
