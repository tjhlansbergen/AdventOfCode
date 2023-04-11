namespace AocRunner;

public class Day3
{
    private struct Coord
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public static void Run(string input, string[] lines)
    {
        Part1(input);
        Part2(input);
    }

    private static void Part2(string input)
    {
        var grid = new Dictionary<Coord, long> { { new Coord(0,0), 1} };

        long last = 1;
        var intput = int.Parse(input);

        int x = 0, y = 0;
        int direction = 1;
        int steps = 1;
        int count = 0;

        while (last < intput)
        {
            Move(ref x, ref y, ref direction, ref steps, ref count);
            var position = new Coord(x,y);
            last = Neighbours(grid, position);
            grid.Add(position, last);
        }

        System.Console.WriteLine($"Part 2: {last}");
    }

    private static long Neighbours(Dictionary<Coord, long> dict, Coord coord)
    {
        long total = 0;

        total += TryGet(dict, new Coord(coord.X, coord.Y - 1)); // N
        total += TryGet(dict, new Coord(coord.X + 1, coord.Y - 1)); // NE
        total += TryGet(dict, new Coord(coord.X + 1, coord.Y)); // E
        total += TryGet(dict, new Coord(coord.X + 1, coord.Y + 1)); // SE
        total += TryGet(dict, new Coord(coord.X, coord.Y + 1)); // S
        total += TryGet(dict, new Coord(coord.X - 1, coord.Y + 1)); // SW
        total += TryGet(dict, new Coord(coord.X - 1, coord.Y)); // W
        total += TryGet(dict, new Coord(coord.X - 1, coord.Y - 1)); // NW
    
        return total;

        long TryGet(Dictionary<Coord, long> d, Coord c)
        {
            return d.ContainsKey(c) ? dict[c] : 0;
        }
    }

    private static void Part1(string input)
    {
        int x = 0, y = 0;
        int direction = 1;
        int steps = 1;
        int count = 0;

        for (int i = 1; i < int.Parse(input); i++)
        {
            Move(ref x, ref y, ref direction, ref steps, ref count);
        }

        System.Console.WriteLine($"Part 1: {Math.Abs(x) + Math.Abs(y)}");
    }

    private static void Move(ref int x, ref int y, ref int direction, ref int steps, ref int count)
    {
        // move
        switch (direction)
        {
            case 1: // right 
                x += 1;
                break;
            case 2: // up
                y -= 1;
                break;
            case 3: // left
                x -= 1;
                break;
            case 4: // down
                y += 1;
                break;
        }

        count++;
        if (count == steps)
        {
            direction = (direction == 4) ? direction = 1 : direction = direction + 1;
            if (direction % 2 != 0) steps++;

            count = 0;
        }
    }
}