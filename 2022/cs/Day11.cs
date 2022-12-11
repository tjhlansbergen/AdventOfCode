namespace AocRunner;

public class Day11
{
    public class Monkey
    {
        public int Id { get; set; }
        public List<long> Items { get; set; } = new List<long>();
        public Func<long, long>? Operation { get; set; }
        public long Test { get; set; }
        public int TestTrue { get; set; }
        public int TestFalse { get; set; }
        public long Count { get; set; }

    }

    public static void Run(string input, string[] lines)
    {
        var monkeys = new List<Monkey>();

        foreach (var m in lines.Chunk(7)) monkeys.Add(Parse(m));

        for (int r = 0; r < 20; r++)
        {
            for (int i = 0; i < monkeys.Count; i++)
            {
                Play(monkeys, i, -1);
            }
        }

        foreach (var m in monkeys)
        {
            System.Console.WriteLine($"Monkey {m.Id}: {m.Count}");
        }

        var part1 = monkeys.OrderByDescending(m => m.Count).Take(2).Select(m => m.Count).Aggregate((c, x) => c * x);
        System.Console.WriteLine($"Part 1: {part1}");

        // -------------------------------
        System.Console.WriteLine();
        monkeys = new List<Monkey>();
        foreach (var m in lines.Chunk(7)) monkeys.Add(Parse(m));

        var div = monkeys.Select(m => m.Test).Aggregate((c, x) => c * x);

        for (int r = 0; r < 10000; r++)
        {
            for (int i = 0; i < monkeys.Count; i++)
            {
                Play(monkeys, i, div);
            }
        }

        foreach (var m in monkeys)
        {
            System.Console.WriteLine($"Monkey {m.Id}: {m.Count}");
        }

        var part2 = monkeys.OrderByDescending(m => m.Count).Take(2).Select(m => m.Count).Aggregate((c, x) => c * x);
        System.Console.WriteLine($"Part 2: {part2}");

        void Play(List<Monkey> monkeys, int i, long div)
        {
            var monkey = monkeys.Single(m => m.Id == i);

            foreach (var item in monkey.Items)
            {
                monkey.Count++;
                if (monkey.Operation == null) throw new ArgumentNullException($"Operation for monkey {i} was null");
                long newItem;
                if (div == -1) newItem = monkey.Operation.Invoke(item) / 3;
                else newItem = monkey.Operation.Invoke(item) / 1 % div;
                
                if (newItem % monkey.Test == 0) monkeys[monkey.TestTrue].Items.Add(newItem);
                else monkeys[monkey.TestFalse].Items.Add(newItem);
            }

            monkey.Items.Clear();
        }


        Monkey Parse(string[] m)
        {
            var result = new Monkey();

            result.Id = int.Parse(m[0].Split(' ')[1].Replace(":", string.Empty));
            result.Items = m[1].Split("items:")[1].Split(',').Select(i => long.Parse(i)).ToList();

            result.Test = int.Parse(m[3].Replace("Test: divisible by ", string.Empty));
            result.TestTrue = int.Parse(m[4].Replace("If true: throw to monkey ", string.Empty));
            result.TestFalse = int.Parse(m[5].Replace("If false: throw to monkey ", string.Empty));


            var parts = m[2].Split("= old ")[1].Split(' ');

            if (parts[0] == "+")
            {
                if (parts[1] == "old") result.Operation = x => x + x;
                else result.Operation = x => x + long.Parse(parts[1]);
            }

            if (parts[0] == "*")
            {
                if (parts[1] == "old") result.Operation = x => x * x;
                else result.Operation = x => x * long.Parse(parts[1]);
            }

            return result;
        }
    }
}