namespace AocRunner;

public class Day2
{
    public static void Run(string input, string[] lines)
    {
        Part1(lines);
        Part2(lines);
    }

    private static void Part1(string[] lines)
    {
        var pos = 5;
        System.Console.Write("Part 1: ");
        foreach (var line in lines)
        {
            pos = Line1(pos, line);
            System.Console.Write(pos);
        }
        System.Console.WriteLine();
    }

    private static int Line1(int pos, string moves)
    {
        foreach (var move in moves)
        {
            switch (move)
            {
                case 'U':
                    if (pos > 3) pos -= 3;
                    break;
                case 'D':
                    if (pos < 7) pos += 3;
                    break;
                case 'R':
                    if (!new[] { 3, 6, 9 }.Contains(pos)) pos++;
                    break;
                case 'L':
                    if (!new[] { 1, 4, 7 }.Contains(pos)) pos--;
                    break;
                default:
                    throw new InvalidOperationException($"Illegal move: {move}");
            }
        }
        return pos;
    }

    private static void Part2(string[] lines)
    {
        var map = new Dictionary<Tuple<int, int>, char?>();

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                map.Add(Tuple.Create(j, i), null);
            }
        }

        map[Tuple.Create(2, 0)] = '1';
        map[Tuple.Create(1, 1)] = '2';
        map[Tuple.Create(2, 1)] = '3';
        map[Tuple.Create(3, 1)] = '4';
        map[Tuple.Create(0, 2)] = '5';
        map[Tuple.Create(1, 2)] = '6';
        map[Tuple.Create(2, 2)] = '7';
        map[Tuple.Create(3, 2)] = '8';
        map[Tuple.Create(4, 2)] = '9';
        map[Tuple.Create(1, 3)] = 'A';
        map[Tuple.Create(2, 3)] = 'B';
        map[Tuple.Create(3, 3)] = 'C';
        map[Tuple.Create(2, 4)] = 'D';

        var pos = Tuple.Create(0,2); // 5

        System.Console.Write("Part 2: ");
        foreach (var line in lines)
        {
            pos = Line2(pos, line, map);
            System.Console.Write(map[pos]);
        }
        System.Console.WriteLine();

    }

    private static Tuple<int, int> Line2(Tuple<int, int> pos, string moves, Dictionary<Tuple<int, int>, char?> map)
    {
        Tuple<int, int> next;

        foreach (var move in moves)
        {
            switch (move)
            {
                case 'U':
                    next = Tuple.Create(pos.Item1, pos.Item2 - 1);
                    break;
                case 'D':
                    next = Tuple.Create(pos.Item1, pos.Item2 + 1);
                    break;
                case 'R':
                    next = Tuple.Create(pos.Item1 + 1, pos.Item2);
                    break;
                case 'L':
                    next = Tuple.Create(pos.Item1 - 1, pos.Item2);
                    break;
                default:
                    throw new InvalidOperationException($"Illegal move: {move}");
            }

            if (map.ContainsKey(next) && map[next] != null)
            {
                pos = next;
            }
        }

        return pos;
    }
}