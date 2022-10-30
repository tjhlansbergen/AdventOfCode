namespace AocRunner;

public class Day4
{
    public static void Run(string input, string[] lines)
    {
        int sum = 0;

        foreach (var line in lines)
        {
            var parts = line.Replace("-", "").Split(']')[0].Split('[');
            var name = parts[0].Substring(0, parts[0].Length - 3);
            var sector = int.Parse(parts[0].Substring(parts[0].Length - 3, 3));
            var checksum = parts[1];

            if (IsValid(name, checksum)) sum += sector;
        }

        System.Console.WriteLine($"Part 1: {sum}");

        bool IsValid(string name, string checksum)
        {
            // calculate the full checksum
            var orderedName = string.Concat(name.GroupBy(c => c).OrderByDescending(g => g.Count()).ThenBy(g => g.Key).Select(g => g.Key));

            // compare
            return orderedName.Substring(0, 5) == checksum;
        }
    }


}