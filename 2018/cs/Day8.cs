namespace AocRunner;

public class Day8
{
    private static int _cumulativeMeta = 0;
    private static int _reader = 0;
    private static int[]? _ints;

    public static void Run(string input, string[] lines)
    {
        _ints = input.Split(' ').Select(x => int.Parse(x)).ToArray();
        var part2 = ParseNode();

        System.Console.WriteLine($"Part 1: {_cumulativeMeta}");
        System.Console.WriteLine($"Part 2: {part2}");
    }

    // it works, it's fast, yet I don't like it
    public static int ParseNode()
    {
        var kidCount = _ints![_reader];
        var metaCount = _ints[_reader+1];
        var value = 0;

        _reader += 2;

        var kidValues = new List<int>();
        for (int r = 0; r < kidCount; r++)
        {
            kidValues.Add(ParseNode());
        }

        var metas = new List<int>();
        for (int m = 0; m < metaCount; m++)
        {
            metas.Add(_ints[_reader]);
            _reader++;
        }

        foreach (var meta in metas)
        {
            _cumulativeMeta += meta;

            if (kidCount == 0) 
            {
                value += meta;
            }
            else
            {
                if (meta != 0 && meta <= kidValues.Count)
                {
                    value += kidValues[meta-1];
                }
            }
        }

        return value;
    }
}