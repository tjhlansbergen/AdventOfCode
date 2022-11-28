namespace Runner;

public class Day17
{
    public static void Run()
    {
        var containers = new int [] {43,3,4,10,21,44,4,6,47,41,34,17,17,44,36,31,46,9,27,38};

        var query = Enumerable
            .Range(1, (1 << containers.Count()) - 1)
            .Select(index => containers.Where((item, idx) => ((1 << idx) & index) != 0))
            .Where(x => x.Sum() == 150);

        System.Console.WriteLine($"Part 1: {query.Count()}"); 

        var part2 = query.GroupBy(q => q.Count())
            .OrderBy(gr => gr.Key)
            .First()
            .Count();

        System.Console.WriteLine($"Part 2: {part2}"); 
   }
}