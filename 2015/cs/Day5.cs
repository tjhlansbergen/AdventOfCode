namespace Runner
{
    public class Day5
    {
        internal static void Run()
        {
            var lines = File.ReadAllLines("../inputs/day5");
            var result = lines.Count(l => Pair(l) && Repeat(l));


            System.Console.WriteLine(result);
        }

        internal static bool Vowels(string input)
        {
            return input.Count(c => "aeiou".Contains(c)) >= 3;
        }

        internal static bool Twice(string input)
        {
            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == input[i+1]) return true;
            }

            return false;
        }         

        internal static bool Exclude(string input)
        {
            var excludes = new string[] {"ab", "cd", "pq", "xy"};
            foreach (var e in excludes)
            {
                if (input.Contains(e)) return false;
            }
            return true;
        }

        internal static bool Pair(string input)
        {
            var pairs = new Dictionary<string, int>();

            for (int i = 0; i < input.Length - 1; i++)
            {
                var pair = $"{input[i]}{input[i+1]}";

                if (pairs.ContainsKey(pair))
                {
                    if (pairs[pair] != i - 1)
                    {
                        return true;
                    }
                }
                else
                {
                    pairs[pair] = i;
                }
            }

            return false;
        }

        internal static bool Repeat(string input)
        {
            for (int i = 0; i < input.Length - 2; i++)
            {
                if (input[i] == input[i+2])
                {
                    return true;
                }
            }
            return false;
        }
    }
}