namespace AocRunner;

public class Day7
{
    public class Prog
    {
        public int? Weight { get; set; }
        public Prog? Parent { get; set; }
        public List<Prog> Children { get; set; } = new List<Prog>();

        private int? combinedWeight = null;

        public int CombinedWeight()
        {
            if (combinedWeight == null)
            {
                combinedWeight = Children.Select(c => c.CombinedWeight()).Sum() + Weight!.Value;
            }
            
            return combinedWeight.Value;
        }
    }

    public static void Run(string input, string[] lines)
    {
        var progs = Parse(lines);

        System.Console.WriteLine($"Part 1: {progs.Single(p => p.Value.Parent == null).Key}");

        var discs = progs.Values.Where(p => p.Children.Select(c => c.CombinedWeight()).Distinct().Count() > 1);
        
        // this is magic, we are getting a whole branch of faulty disc and are looking for the one furthest
        // from the root ('fixing' that fixes the whole branch)
        // this one can be found because it's children will not be any of the others
        var disc = discs.Single(d => !d.Children.Any(c => discs.Contains(c)));
        var tower = disc.Children.GroupBy(c => c.CombinedWeight()).Where(gr => gr.Count() == 1).Single();
        var diff = tower.Key - disc.Children.First(c => c.CombinedWeight() != tower.Key).CombinedWeight();
        var part2 = tower.Select(t => t).Single().Weight - diff;

        System.Console.WriteLine($"Part 2: {part2}");
    }

    public static Dictionary<string, Prog> Parse(string[] lines)
    {
        var result = new Dictionary<string, Prog>();

        foreach (var line in lines)
        {
            var left = string.Empty; var right = string.Empty;

            if (line.Contains("->"))
            {
                left = line.Split("->")[0].Trim();
                right = line.Split("->")[1].Trim();
            }
            else
            {
                left = line;
            }

            var name = left.Split(' ')[0];
            var weight = int.Parse(left.Split(' ')[1].Replace('(', ' ').Replace(')', ' ').Trim());
            Prog parent;

            if (result.ContainsKey(name))
            {
                parent = result[name];
                result[name].Weight = weight;
            }    
            else
            {
                parent = new Prog { Weight = weight };
                result.Add(name, parent);
            }

            if (right != string.Empty)
            {
                foreach(var childname in right.Split(", "))
                {
                    Prog child;

                    if (result.ContainsKey(childname))
                    {
                        child = result[childname];
                        child.Parent = parent;
                    }
                    else
                    {
                        child = new Prog { Parent = parent };
                        result.Add(childname, child);
                    }

                    parent.Children.Add(child);
                }
            }
        }

        return result;
    }
}