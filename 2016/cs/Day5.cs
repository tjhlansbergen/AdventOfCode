namespace AocRunner;

public class Day5
{
    public static void Run(string input, string[] lines)
    {

        // part 1
        System.Console.WriteLine("\nPart 1: ");
        var sixthChars = Enumerable.Range(0, int.MaxValue)
            .Select(i => NextHash(input, i))
            .Where(h => h.StartsWith("00000"))
            .Select(h => h[5]).Take(8);
        System.Console.WriteLine(string.Concat(sixthChars));



        // part 2
        var index = 0;
        var hash = string.Empty;
        var result = new List<Char> { '_', '_', '_', '_', '_', '_', '_', '_' };

        System.Console.WriteLine("\nPart 2: ");

        do
        {
            hash = NextHash(input, index);

            if (hash.StartsWith("00000") && new[] { '0', '1', '2', '3', '4', '5', '6', '7' }.Contains(hash[5]) && result[hash[5] - '0'] == '_')
            {
                result[hash[5] - '0'] = hash[6];
                System.Console.WriteLine(string.Concat(result));
            }

            index++;
        } while (result.Contains('_'));


        // helpers

        string NextHash(string input, int index)
        {
            return GenerateMD5(input + index.ToString());
        }

        string GenerateMD5(string input)
        {

            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);
            }
        }
    }
}