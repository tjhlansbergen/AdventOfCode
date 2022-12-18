namespace AocRunner;

public class Day12
{
    public static void Run(string input, string[] lines)
    {
        Tuple<int, int> start = Tuple.Create(-1,-1), end = Tuple.Create(-1,-1);
        int width = lines[0].Length;
        int height = lines.Length;
        var grid = new int[height, width];

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[0].Length; x++)
            {
                grid[y,x] = lines[y][x];

                if (lines[y][x] == 'S')
                {
                    start = Tuple.Create(x, y);
                    grid[y,x] = 'a';
                } 
                if (lines[y][x] == 'E')
                {
                    end = Tuple.Create(x, y);
                    grid[y,x] = 'z';
                } 
            }
        }

        var visited = new List<Tuple<int, int>>();
        var next = new Queue<Tuple<Tuple<int, int>, int>>();

        next.Enqueue(Tuple.Create(start, 0));

        // part 1, climbing up
        while (next.Any())
        {
            var curPos = next.Dequeue();

            if (curPos.Item1.Item1 == end.Item1 && curPos.Item1.Item2 == end.Item2)
            {
                System.Console.WriteLine($"Part 1: {curPos.Item2}");
                break;
            }

            if (visited.Contains(curPos.Item1)) continue;
            visited.Add(curPos.Item1);

            int x = curPos.Item1.Item1, y = curPos.Item1.Item2;
            var curHeight = grid[y,x];
            var step = curPos.Item2 + 1;
            //System.Console.WriteLine($"Step: {step}, queue: {next.Count}");

            if (y > 0 && grid[y-1,x] <= curHeight + 1) next.Enqueue(Tuple.Create(Tuple.Create(x,y-1), step));
            if (y < height - 1 && grid[y+1,x] <= curHeight + 1) next.Enqueue(Tuple.Create(Tuple.Create(x,y+1), step));
            if (x > 0 && grid[y,x-1] <= curHeight + 1) next.Enqueue(Tuple.Create(Tuple.Create(x-1,y), step));
            if (x < width - 1 && grid[y,x+1] <= curHeight + 1) next.Enqueue(Tuple.Create(Tuple.Create(x+1,y), step));

        }

        // part 2, we need the a closest to the top, therefor climb down
        visited.Clear();
        next.Clear();

        next.Enqueue(Tuple.Create(end, 0));

        while (next.Any())
        {
            var curPos = next.Dequeue();
            int x = curPos.Item1.Item1, y = curPos.Item1.Item2;
            var curHeight = grid[y,x];

            if (curHeight == 'a')
            {
                System.Console.WriteLine($"Part 2: {curPos.Item2}, position {curPos.Item1}");
                break;
            }

            if (visited.Contains(curPos.Item1)) continue;
            visited.Add(curPos.Item1);
            
            var step = curPos.Item2 + 1;

            // this is the opposite of part 1
            if (y > 0 && grid[y-1,x] >= curHeight - 1) next.Enqueue(Tuple.Create(Tuple.Create(x,y-1), step));
            if (y < height - 1 && grid[y+1,x] >= curHeight - 1) next.Enqueue(Tuple.Create(Tuple.Create(x,y+1), step));
            if (x > 0 && grid[y,x-1] >= curHeight - 1) next.Enqueue(Tuple.Create(Tuple.Create(x-1,y), step));
            if (x < width - 1 && grid[y,x+1] >= curHeight - 1) next.Enqueue(Tuple.Create(Tuple.Create(x+1,y), step));

        }
    }
}