namespace AocRunner;

public class Day18
{
    public record struct Coord
    {
        public required int X { get; set; }
        public required int Y { get; set; }


        public Coord LeftTop => new() { X = X - 1, Y = Y - 1 };
        public Coord MiddleTop => new() { X = X, Y = Y - 1 };
        public Coord RightTop => new() { X = X + 1, Y = Y - 1 };
        public Coord RightMiddle => new() { X = X + 1, Y = Y };
        public Coord RightBottom => new() { X = X + 1, Y = Y + 1 };
        public Coord MiddleBottom => new() { X = X, Y = Y + 1 };
        public Coord LeftBottom => new() { X = X - 1, Y = Y + 1 };
        public Coord LeftMiddle => new() { X = X - 1, Y = Y };
    }

    public class Grid
    {

        public required int Width { get; set; }
        public required int Height { get; set; }

        public Dictionary<Coord, bool> Items = new();

        public bool GetState(Coord coord, bool part2 = false)
        {
            if (part2)
            {
                // short circuit the corners for pt 2
                if (coord.X == 0 && coord.Y == 0
                 || coord.X == Width - 1 && coord.Y == 0
                 || coord.X == Width - 1 && coord.Y == Height - 1
                 || coord.X == 0 && coord.Y == Height - 1)
                    return true;
            }

            if (Items[coord] == true)
            {
                var n = FindNeighbours(coord);
                return n == 2 || n == 3;
            }
            else
            {
                return FindNeighbours(coord) == 3;
            }
        }

        private int FindNeighbours(Coord coord)
        {
            var count = 0;

            if (WithinBounds(coord.LeftTop) && Items[coord.LeftTop]) count++;
            if (WithinBounds(coord.MiddleTop) && Items[coord.MiddleTop]) count++;
            if (WithinBounds(coord.RightTop) && Items[coord.RightTop]) count++;
            if (WithinBounds(coord.RightMiddle) && Items[coord.RightMiddle]) count++;
            if (WithinBounds(coord.RightBottom) && Items[coord.RightBottom]) count++;
            if (WithinBounds(coord.MiddleBottom) && Items[coord.MiddleBottom]) count++;
            if (WithinBounds(coord.LeftBottom) && Items[coord.LeftBottom]) count++;
            if (WithinBounds(coord.LeftMiddle) && Items[coord.LeftMiddle]) count++;

            return count;
        }


        private bool WithinBounds(Coord coord)
        {
            return coord.X > -1
                && coord.Y > -1
                && coord.X < Width
                && coord.Y < Height;
        }

        public void Print()
        {
            System.Console.WriteLine();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    System.Console.Write(Items[new Coord { X = x, Y = y }] ? 'X' : '.');
                }
                System.Console.WriteLine();
            }

            System.Console.WriteLine();
        }
    }

    public static void Run(string input, string[] lines)
    {
        var grid = Parse(lines);
        var count = 100;

        for (int i = 0; i < count; i++)
        {
            var newGrid = new Dictionary<Coord, bool>();

            foreach (var item in grid.Items)
            {
                newGrid.Add(item.Key, grid.GetState(item.Key));
            }

            grid.Items = newGrid;
        }

        System.Console.WriteLine($"Part 1: {grid.Items.Count(i => i.Value == true)}");

        grid = Parse(lines);
        count = 100;

        for (int i = 0; i < count; i++)
        {
            var newGrid = new Dictionary<Coord, bool>();

            foreach (var item in grid.Items)
            {
                newGrid.Add(item.Key, grid.GetState(item.Key, true));
            }

            grid.Items = newGrid;
        }

        System.Console.WriteLine($"Part 2: {grid.Items.Count(i => i.Value == true)}");
    }

    private static Grid Parse(string[] lines)
    {
        var result = new Dictionary<Coord, bool>();

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[0].Length; x++)
            {
                result.Add(new Coord { X = x, Y = y }, lines[y][x] == '#');
            }
        }

        return new Grid
        {
            Height = lines.Length,
            Width = lines[0].Length,
            Items = result,
        };
    }
}