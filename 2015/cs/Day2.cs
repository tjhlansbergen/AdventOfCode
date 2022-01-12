namespace Runner
{
    internal static class Day2
    {
        internal static void Run()
        {
            
            var lines = File.ReadAllLines("../inputs/day2");
            var presents = lines.Select(line => line.Split("x"))
                .Select(s => new List<int>{Int32.Parse(s[0]), Int32.Parse(s[1]), Int32.Parse(s[2])});

            // part one
            var sides = presents.Select(p => new List<int>{2*p[0]*p[1], 2*p[1]*p[2], 2*p[2]*p[0]});
            var feet = sides.Select(s => s.Sum() + s.Min() / 2);
            System.Console.WriteLine(feet.Sum());

            // part two
            var ribbons = presents.Select(p => p.OrderBy(x => x).Take(2).Sum()*2 + p.Aggregate((t, n) => t * n));
            System.Console.WriteLine(ribbons.Sum());
         }
    }
}