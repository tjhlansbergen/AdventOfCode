namespace AocRunner;

public class Day4
{
    public class Shift
    {
        public int Guard { get; set; }
        public string? Date { get; set; } = null;
        public List<int> Minutes = new ();

        public Shift(int guard)
        {
            Guard = guard;
        }
    }

    public static void Run(string input, string[] lines)
    {
        var ordered = lines.OrderBy(l => l);
        var shifts = ParseShifts(ordered);

        //part 1
        var sleepyestGuard = shifts.GroupBy(s => s.Guard)
                                .MaxBy(gr => gr.Sum(gr => gr.Minutes.Count))?
                                .Key;

        var sleepyestMinute = Enumerable.Range(0, 59)
                                        .Select(m => new {m, c = shifts.Count(s => s.Guard == sleepyestGuard && s.Minutes.Contains(m))})
                                        .MaxBy(mc => mc.c)?
                                        .m;
        
        System.Console.WriteLine($"Part 1: {sleepyestGuard * sleepyestMinute}");
    }

    private static List<Shift> ParseShifts(IEnumerable<string> lines)
    {
        var result = new List<Shift>();

        var currentGuard = -1;
        var sleeps = -1;

        foreach (var line in lines)
        {
            var s = line.Split(']');
            if (s[1].StartsWith(" Guard"))
            {
                currentGuard = int.Parse(s[1].Split('#')[1].Split(' ')[0]);
                result.Add(new Shift(currentGuard));
            }
            else
            {
                // set date if needed
                if (result.Last().Date == null)
                {
                    result.Last().Date = s[0].Replace('[', ' ').Trim().Split(' ')[0];
                }

                if (s[1] == " falls asleep")
                {
                    sleeps = int.Parse(s[0].Split(':')[1]);
                }
                else
                {
                    int wakes = int.Parse(s[0].Split(':')[1]);
                    result.Last().Minutes.AddRange(Enumerable.Range(sleeps, wakes - sleeps));
                }
            }
        }

        return result;
    }
}