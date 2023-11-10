namespace AocRunner;

public class Day7
{
    public class Prog
    {
        public int? Weight { get; set; }
        public Prog? Parent { get; set; }
        public List<Prog> Children { get; set; } = new List<Prog>();
    }

    public static void Run(string input, string[] lines)
    {
        var progs = Parse(lines);

        System.Console.WriteLine($"Part 1: {progs.Single(p => p.Value.Parent == null).Key}");
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