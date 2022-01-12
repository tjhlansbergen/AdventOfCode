namespace Runner
{
    internal static class Day1
    {
        internal static void Run()
        {
            // part one
            var line = File.ReadAllLines("../inputs/day1")[0];
            Console.WriteLine(line.Count(c => c == '(') - line.Count(c => c == ')'));
            
            // part two
            var floor = 0;
            var index = 0;
            foreach (char c in line)
            {
                index += 1;
                floor += c == '(' ? 1 : -1;
                if (floor == -1) break; 
            }

            System.Console.WriteLine(index);
        }
    }
}