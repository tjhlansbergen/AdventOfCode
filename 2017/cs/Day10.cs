namespace AocRunner;

public class Day10
{
    public static void Run(string input, string[] lines)
    {
        var strng = new LinkedList<int>(Enumerable.Range(0, 256));
        var lengths1 = input.Split(',').Select(i => int.Parse(i.Trim()));
        var lengths2 = StringToAscii(input);
        lengths2 = lengths2.Concat(new[] { 17, 31, 73, 47, 23 });

        var current = strng.First;
        var skip = 0;

        // part 1
        Round(strng, lengths1, ref current, ref skip);
        System.Console.WriteLine($"Part 1: {strng!.First!.Value * strng.First.Next!.Value}");

        //reset, part 2
        strng = new LinkedList<int>(Enumerable.Range(0, 256));
        current = strng.First;
        skip = 0;

        for (int x = 0; x < 64; x++)
        {
            Round(strng, lengths2, ref current, ref skip);
        }

        var dense = strng.Chunk(16).Select(chunk => Xor(chunk));
        var hex = string.Join(string.Empty, dense.Select(x => Hex(x)));

        System.Console.WriteLine($"Part 2: {hex}");
    }

    private static void Round(LinkedList<int> strng, IEnumerable<int> lengths1, ref LinkedListNode<int>? current, ref int skip)
    {
        foreach (var length in lengths1)
        {
            var reversedPart = new LinkedList<int>();
            var start = current;

            for (int i = 0; i < length; i++)
            {
                reversedPart.AddFirst(current!.Value);
                current = current.Next ?? strng.First;
            }

            current = start;

            foreach (var item in reversedPart)
            {
                current!.Value = item;
                current = current.Next ?? strng.First;
            }

            for (int i = 0; i < skip; i++) { current = current!.Next ?? strng.First; }

            skip++;
        }
    }

    private static IEnumerable<int> StringToAscii(string input)
    {
        foreach (var c in input)
        {
            yield return (int)c;
        }
    }

    private static int Xor(int[] arr)
    {
        return arr.Aggregate((a, b) => a ^ b);
    }

    private static string Hex(int i)
    {
        return i.ToString("x2");
    }
}