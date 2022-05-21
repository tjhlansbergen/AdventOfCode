namespace Runner
{
    public class Day4
    {

        internal static void Run()
        {
            var input = "yzbqklnj";
            var count = 0;
            var result = string.Empty;

            while(true)
            {
                result = CreateMD5($"{input}{count}");
                if(result.StartsWith("000000")) break;
                count++;
            }

            System.Console.WriteLine($"{count} ({result})");
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);
            }
        }
    }
}