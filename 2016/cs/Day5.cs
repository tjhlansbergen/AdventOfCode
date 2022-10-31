namespace AocRunner;

public class Day5
{
    public static void Run(string input, string[] lines)
    {
        var index = 0;
        string hash = string.Empty, passcode = string.Empty;

        var result = new List<char>();

        // part 1
        System.Console.WriteLine("\nPart 1: ");
        do
        {
            passcode = input + index.ToString();
            hash = GenerateMD5(passcode);

            if (hash.StartsWith("00000"))
            {
                result.Add(hash[5]);
                System.Console.WriteLine(string.Concat(result));
            }

            index++;
        } while (result.Count() < 8);


        // part 2
        index = 0;
        hash = string.Empty;
        passcode = string.Empty;
        result = new List<Char> { '_', '_', '_', '_', '_', '_', '_', '_' };

        System.Console.WriteLine("\nPart 2: ");
        do
        {
            passcode = input + index.ToString();
            hash = GenerateMD5(passcode);

            if (hash.StartsWith("00000") && new[] { '0', '1', '2', '3', '4', '5', '6', '7' }.Contains(hash[5]) && result[hash[5] - '0'] == '_')
            {
                result[hash[5] - '0'] = hash[6];
                System.Console.WriteLine(string.Concat(result));
            }

            index++;
        } while (result.Contains('_'));
    }

    public static string GenerateMD5(string input)
    {

        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}