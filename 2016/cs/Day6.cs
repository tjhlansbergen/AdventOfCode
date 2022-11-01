namespace AocRunner;

public class Day6
{
    public static void Run(string input, string[] lines)
    {
        var columns = Transpose<char>(lines.Select(l => l.Select(c => c).ToArray()).ToArray());
        
        var mostCommon = columns.Select(c => c.GroupBy(ch => ch).OrderByDescending(g => g.Count()).First().Key);
        System.Console.WriteLine($"part 1: {string.Concat(mostCommon)}");

        var leastCommon = columns.Select(c => c.GroupBy(ch => ch).OrderBy(g => g.Count()).First().Key);
        System.Console.WriteLine($"part 2: {string.Concat(leastCommon)}");
    }

    public static T[][] Transpose<T>(T[][] matrix)
    {
        int w = matrix[0].Length, h = matrix.Length;
        T[][] result = new T[w][];

        for (int i = 0; i < w; i++)
        {
            result[i] = new T[h];
            for (int j = 0; j < h; j++)
            {
                result[i][j] = matrix[j][i];
            }
        }

        return result;
    }
}