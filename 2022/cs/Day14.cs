namespace AocRunner;

public class Day14
{
    public record Coord
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public static void Run(string input, string[] lines)
    {
        var rock = CreateRock(lines);
        var abyss = rock.Keys.Select(c => c.Y).Max();
        //PrintRock(rock);

        // part 1

        bool stop = false;
        var count = 0;

        while (!stop)
        {
            bool fall = true;
            var last = new Coord(500, 1);
            Coord next;
            while (fall)
            {
                next = Step(last, rock);
                if (next == last) fall = false;
                else last = next;

                //PrintRock(rock);

                if (next.Y == abyss)
                {
                    fall = false;
                    stop = true;
                    System.Console.WriteLine($"Part 1: {count}");
                }
            }
            count++;
        }

        //PrintRock(rock);

        // part 2

        var floor = abyss + 2;
        stop = false;

        // find and remove last sand from part 1
        var pt1 = rock.Single(s => s.Key.Y == abyss && s.Value == 'o');
        rock.Remove(pt1.Key);

        // and continue, but with floor

        while (!stop)
        {
            bool fall = true;
            var last = new Coord(500, 0);
            Coord next;
            while (fall)
            {
                if (last.Y == floor - 2)
                {
                    fall = false;
                }

                next = Step(last, rock);
                if (next == last) fall = false;
                else last = next;

                //PrintRock(rock);

                if (next.Y == 0)
                {
                    fall = false;
                    stop = true;
                    System.Console.WriteLine($"Part 2: {count}");
                }
            }
            count++;
        }

        //PrintRock(rock);
    }

    public static Coord Step(Coord origin, Dictionary<Coord, char> rock)
    {
        // down
        var down = new Coord(origin.X, origin.Y + 1);
        if (!rock.ContainsKey(down))
        {
            rock.Add(down, 'o');
            rock.Remove(origin);
            return down;
        }

        // left 
        var left = new Coord(origin.X - 1, origin.Y + 1);
        if (!rock.ContainsKey(left))
        {
            rock.Add(left, 'o');
            rock.Remove(origin);
            return left;
        }

        // right
        var right = new Coord(origin.X + 1, origin.Y + 1);
        if (!rock.ContainsKey(right))
        {
            rock.Add(right, 'o');
            rock.Remove(origin);
            return right;
        }

        return origin;
    }

    public static Dictionary<Coord, char> CreateRock(string[] lines)
    {
        var rock = new Dictionary<Coord, char>();

        foreach (var line in lines)
        {
            var coords = line.Split(" -> ").Select(s => new Coord(int.Parse(s.Split(',')[0]), int.Parse(s.Split(',')[1]))).ToArray();

            for (int i = 0; i < coords.Count() - 1; i++)
            {
                var from = coords[i];
                var to = coords[i + 1];

                int len;
                if (from.X == to.X) len = Math.Abs(from.Y - to.Y) + 1;
                else len = Math.Abs(from.X - to.X) + 1;

                for (int j = 0; j < len; j++)
                {
                    if (from.X == to.X && from.Y < to.Y) rock.TryAdd(new Coord(from.X, from.Y + j), '#');  // down
                    if (from.X == to.X && from.Y > to.Y) rock.TryAdd(new Coord(from.X, from.Y - j), '#');  // up
                    if (from.Y == to.Y && from.X < to.X) rock.TryAdd(new Coord(from.X + j, from.Y), '#');  // right
                    if (from.Y == to.Y && from.X > to.X) rock.TryAdd(new Coord(from.X - j, from.Y), '#');  // left
                }
            }

        }

        rock.Add(new Coord(500, 0), '+');
        return rock;
    }

    public static void PrintRock(Dictionary<Coord, char> rock)
    {
        var left = rock.Keys.Select(c => c.X).Min();
        var right = rock.Keys.Select(c => c.X).Max();
        var top = rock.Keys.Select(c => c.Y).Min();
        var bottom = rock.Keys.Select(c => c.Y).Max();

        System.Console.WriteLine();

        for (int y = top; y <= bottom; y++)
        {
            for (int x = left; x <= right; x++)
            {
                var c = new Coord(x, y);    // will be compared by value
                if (rock.ContainsKey(c)) System.Console.Write(rock[c]);
                else System.Console.Write('.');
            }
            System.Console.WriteLine();
        }

        System.Console.WriteLine();

    }
}