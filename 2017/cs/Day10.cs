namespace AocRunner;

public class Day10
{
    public static void Run(string input, string[] lines)
    {
        var strng = new LinkedList<int>(Enumerable.Range(0, 256));
        var lengths = input.Split(',').Select(i => int.Parse(i.Trim()));

        var current = strng.First;
        var skip = 0;

        //System.Console.WriteLine(string.Join(',', strng));

        foreach (var length in lengths)
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

            //System.Console.WriteLine(string.Join(',', strng));
        }

        System.Console.WriteLine($"Part 1: {strng!.First!.Value * strng.First.Next!.Value }");
    }
}