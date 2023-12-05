namespace AocRunner;

public class Day4
{
    struct Card
    {
        public int Winners { get; set; }
        public int Copies { get; set; }
    }

    public static void Run(string input, string[] lines)
    {
        var cards = lines.Select(line => ParseLine(line));
        var part1 = cards.Select(card => Math.Floor(Math.Pow(2, card.Last().Count(i => card.First().Contains(i)) - 1))).Sum();

        System.Console.WriteLine($"Part 1: {part1}");


        // part 2
        // create a linked list of scored cards (do the scoring beforehand, that won't change anymore)
        var deck = new LinkedList<Card>(cards.Select(card => card.Last().Count(i => card.First().Contains(i))).Select(winners => new Card { Winners = winners, Copies = 1 }));
        var cursor = deck.First;

        // visit each original once and count copies
        for (int i = 0; i < deck.Count; i++)
        {
            for (int j = 0; j < cursor!.Value.Copies; j++)
            {
                var next = cursor!.Next;
                for (int k = 0; k < cursor!.Value.Winners; k++)
                {
                    next!.ValueRef.Copies++;
                    next = next.Next;
                }
            }
            cursor = cursor!.Next;
        }

        System.Console.WriteLine($"Part 2: {deck.Sum(card => card.Copies)}");
    }

    private static IEnumerable<IEnumerable<int>> ParseLine(string line)
    {
        return line.Split(':', StringSplitOptions.TrimEntries)[1]
                        .Split('|', StringSplitOptions.TrimEntries)
                        .Select(part => part.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)));

    }
}