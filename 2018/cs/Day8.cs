namespace AocRunner;

public class Day8
{
    private static int _metaCount = 0;
    private static int[]? _ints;

    public static void Run(string input, string[] lines)
    {
        _ints = input.Split(' ').Select(x => int.Parse(x)).ToArray();
        ParseNode(0);
        System.Console.WriteLine($"Part 1: {_metaCount}");
    }

    public static int ParseNode(int reader)
    {
        var kids = _ints![reader];
        var meta = _ints[reader+1];

        reader += 2;

        for (int r = 0; r < kids; r++)
        {
            reader = ParseNode(reader);
        }

        for (int m = 0; m < meta; m++)
        {
            _metaCount += _ints[reader];
            reader++;
        }

        return reader;
    }

}