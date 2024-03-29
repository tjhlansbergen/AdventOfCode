namespace AocRunner;

public class Day9
{
    public class Circle
    {
        private LinkedList<int> values = new();
        private LinkedListNode<int> current;

        public Circle()
        {
            values.AddFirst(0);
            current = values.First!;
        }

        public int Insert(int marble)
        {
            if (marble % 23 == 0)
            {
                var score = marble;
                score += CounterClockWise(7);
                return score;
            }

            current = values.AddAfter(current.Next ?? values.First!, marble);
            return 0;
        }

        public IEnumerable<int> Enumerate()
        {
            return values;
        }

        private int CounterClockWise(int count)
        {
            // move ccw
            for (int i = 0; i < count; i++)
            {
                current = current.Previous ?? values.Last!;
            }
            // make note of value
            var result = current.Value;
            // make note of item
            var remove = current;
            // reset current
            current = current.Next ?? values.First!;
            // remove item
            values.Remove(remove);
            // return value
            return result;
        }
    }

    public static void Run(string input, string[] lines)
    {
        System.Console.WriteLine($"Part 1: {RunOne(input, 1)}"); 
        System.Console.WriteLine($"Part 2: {RunOne(input, 100)}"); 
    }

    private static long RunOne(string input, int multiplyer)
    {
        var splits = input.Split(' ');
        var rounds = int.Parse(splits[6].Trim()) * multiplyer;
        var playerCount = int.Parse(splits[0].Trim());
        var players = Enumerable.Range(1, playerCount).ToDictionary(k => k, v => (long)0);
        var circle = new Circle();

        for (int i = 1; i <= rounds; i++)
        {
            players[((i-1) % playerCount) + 1] += circle.Insert(i);
        }

        return players.Values.Max();
    }
}