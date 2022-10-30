namespace AocRunner;

public class Day4
{
    public static void Run(string input, string[] lines)
    {
        int sum = 0;

        foreach (var line in lines)
        {
            var parts = line.Split(']')[0].Split('[');
            var name = parts[0].Substring(0, parts[0].Length - 3);
            var sector = int.Parse(parts[0].Substring(parts[0].Length - 3, 3));
            var checksum = parts[1];

            if (IsValid(name, checksum)) 
            {
                // pt 1
                sum += sector;  

                // pt 2
                var decodedChars = new List<char>();
                int move = sector % 26;

                foreach (var c in name)
                {
                    if (c == '-') 
                    {
                        decodedChars.Add(' ');
                        continue;
                    }

                    int ascii = (byte)c;
                    int result = ascii + move;
                    if (result > 122) result -= 26;

                    decodedChars.Add((char)result);
                }

                if (string.Concat(decodedChars).Contains("northpole object"))
                {
                    System.Console.WriteLine($"Part 2: {sector}");
                    break;
                }
            }
        }

        System.Console.WriteLine($"Part 1: {sum}");

        bool IsValid(string name, string checksum)
        {
            // calculate the full checksum
            var orderedName = string.Concat(name.Replace("-", "").GroupBy(c => c).OrderByDescending(g => g.Count()).ThenBy(g => g.Key).Select(g => g.Key));

            // compare
            return orderedName.Substring(0, 5) == checksum;
        }
    }


}