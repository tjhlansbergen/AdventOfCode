namespace AocRunner;

public class Day4
{
    public class Shift
    {
        public int Id { get; set; }
        public Dictionary<int, int> Hours { get; set; }

        public Guard(int id)
        {
            Id = id;
            Hours = new Dictionary<int, int>();
            for (int i = 0; i < 60; i++) Hours[i] = 0;
        }
    }

    public static void Run(string input, string[] lines)
    {
        var ordered = lines.OrderBy(l => l);
        var guards = new List<Guard>();
        Guard? currentGuard = null;

        foreach (var line in ordered)
        {
            var s = line.Split(']');
            if (s[1].StartsWith(" Guard")) ParseGuard(line);
            else ParseSleep(line);
        }

        void ParseGuard(string line)
        {
            var id = int.Parse(line.Split('#')[1].Split(" begins")[0]);
            if (guards.Select(g => g.Id).Contains(id)) 
            {
                currentGuard = guards.Single(g => g.Id == id);
            }
            else
            {
                currentGuard = new Guard(id); 
                guards.Add(currentGuard);
            } 
        }

        void ParseSleep(string line)
        {
            
        }

    }
}