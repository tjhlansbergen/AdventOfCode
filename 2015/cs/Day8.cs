using System.Text.RegularExpressions;
namespace Runner
{
    public class Day8
    {
        
     
        internal static void Run()
        {
            var words = File.ReadAllLines("../inputs/day8");
            Console.Out.WriteLine(words.Sum(w => w.Length - Regex.Replace(w.Trim('"').Replace("\\\"", "A").Replace("\\\\", "B"), "\\\\x[a-f0-9]{2}", "C").Length));
            Console.Out.WriteLine(words.Sum(w => w.Replace("\\", "AA").Replace("\"", "BB").Length + 2 - w.Length));

        }

       
    }
}