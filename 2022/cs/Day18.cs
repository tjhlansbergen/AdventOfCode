namespace AocRunner;

public class Day18
{
    public static void Run(string input, string[] lines)
    {
        // gets a count of all sides of the cubes that are unique
        // (and therefor not shared with another cube)
        var part1 = lines.Select(l => l.Split(',').Select(p => int.Parse(p)).ToArray())
             .Select(a => Cubenize(a))
             .SelectMany(cubes => cubes)
             .Select(plane => string.Join('-', plane))
             .GroupBy(plane => plane)
             .Where(group => group.Count() == 1)
             .Count();

        System.Console.WriteLine(part1);


        List<int[]> Cubenize(int[] p)
        {
            var result = new List<int[]>();
            result.Add(new[] {p[0], p[1], p[2], p[0]+1, p[1]+1, p[2]}); // front
            result.Add(new[] {p[0], p[1], p[2]+1, p[0]+1, p[1]+1, p[2]+1}); // back
            result.Add(new[] {p[0], p[1]+1, p[2], p[0]+1, p[1]+1, p[2]+1}); // top
            result.Add(new[] {p[0], p[1], p[2], p[0]+1, p[1], p[2]+1}); // bottom
            result.Add(new[] {p[0], p[1], p[2], p[0], p[1]+1, p[2]+1}); // left
            result.Add(new[] {p[0]+1, p[1], p[2], p[0]+1, p[1]+1, p[2]+1}); // right

            return result;
        }
    }
}
