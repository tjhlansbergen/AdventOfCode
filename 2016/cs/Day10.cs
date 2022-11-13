namespace AocRunner;

public class Day10
{
    public static void Run(string input, string[] lines)
    {
        var bots = new List<Bot>();
        var outputs = new Dictionary<int, List<int>>{
            {0, new List<int>()},
            {1, new List<int>()},
            {2, new List<int>()},
        };



        foreach (var line in lines.Where(l => l.StartsWith("bot")))
        {
            bots.Add(ParseBot(line));
        }

        foreach (var line in lines.Where(l => l.StartsWith("value")))
        {
            var valueTo = ParseValue(line);
            bots.First(b => b.Id == valueTo.Item2).Chips.Add(valueTo.Item1);
        }

        while (bots.Any(b => b.Chips.Count() > 1))
        {
            var bot = bots.First(b => b.Chips.Count() > 1);
            var low = bot.Chips.Min();
            var high = bot.Chips.Max();

            if (low == 17 && high == 61)
            {
                System.Console.WriteLine($"Part 1: {bot.Id}");
                //break;
            }

            // take chips
            bot.Chips = bot.Chips
                .Where(c => c != low)
                .Where(c => c != high)
                .ToList();

            // give chips
            if (bot.LowIsOutput)
            { 
                if (bot.Low < 3)
                {
                    outputs[bot.Low].Add(low);
                }
            }
            else
            { bots.Single(b => b.Id == bot.Low).Chips.Add(low); }
            
            
            if (bot.HighIsOutput)
            {
                if (bot.High < 3)
                {
                    outputs[bot.High].Add(high);
                }
            } 
            else
            { bots.Single(b => b.Id == bot.High).Chips.Add(high); }
        }

        var part2 = outputs.Select(o => o.Value.First()).Aggregate(1, (acc, val) => acc * val);
        System.Console.WriteLine($"Part 1: {part2}");

        (int, int) ParseValue(string line)
        {
            var parts = line.Split(' ');
            return (int.Parse(parts[1]), int.Parse(parts[5]));
        }

        Bot ParseBot(string line)
        {
            var parts = line.Split(' ');

            if (parts[5] == "output")
            {
                return new Bot(int.Parse(parts[1]), int.Parse(parts[6]), int.Parse(parts[11]), true, false);
            }

            if (parts[10] == "output")
            {
                return new Bot(int.Parse(parts[1]), int.Parse(parts[6]), int.Parse(parts[11]), false, true);
            }

            return new Bot(int.Parse(parts[1]), int.Parse(parts[6]), int.Parse(parts[11]), false, false);
        }
    }

    record Bot
    {

        public int Id { get; init; }
        public int Low { get; init; }
        public int High { get; init; }

        public bool LowIsOutput { get; init; }
        public bool HighIsOutput { get; init; }

        public List<int> Chips { get; set; } = new List<int>();

        public Bot(int id, int low, int high, bool lowIsOutput, bool highIsOutput)
        {
            Id = id;
            Low = low;
            High = high;
            LowIsOutput = lowIsOutput;
            HighIsOutput = highIsOutput;
        }
    }
}