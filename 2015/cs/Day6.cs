namespace Runner
{
    public class Day6
    {
        public class Move
        {
            public string? Action { get; set; }
            public Tuple<int, int>? From { get; set; }
            public Tuple<int, int>? To { get; set; }
        }

        internal static void Run()
        {
            var lines = File.ReadAllLines("../inputs/day6");
            var grid = GetGrid(1000);
            grid = Moves(grid, lines);

            System.Console.WriteLine(grid.Values.Sum(v => v));
        }

        internal static Dictionary<Tuple<int, int>, int> GetGrid(int size)
        {
            var grid = new Dictionary<Tuple<int, int>, int>();

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    grid.Add(new Tuple<int, int>(x, y), 0);
                }
            }

            return grid;
        }

        internal static Dictionary<Tuple<int, int>, int> Moves(Dictionary<Tuple<int, int>, int> grid, string[] moves)
        {
            int count = 0;

            foreach (string line in moves)
            {
                var move = ParseMove(line);

                count++;
                System.Console.WriteLine($"{count} {move.Action}");


                for (int y = move.From!.Item2; y <= move.To!.Item2; y++)
                {
                    for (int x = move.From.Item1; x <= move.To.Item1; x++)
                    {
                        switch (move.Action)
                        {
                            case "on":
                                grid[new Tuple<int, int>(x,y)] += 1;
                                break;
                            case "off":
                                if(grid[new Tuple<int, int>(x,y)] > 0) { grid[new Tuple<int, int>(x,y)] -= 1;}
                                break;
                            case "toggle":
                                grid[new Tuple<int, int>(x,y)] += 2;
                                break;
                        }
                    }
                }
            }

            return grid;
        }

        internal static Move ParseMove(string move)
        {
            var result = new Move();

            if (move.StartsWith("turn on "))
            {
                result.Action = "on";
                move = move.Replace("turn on ", "");
            }
            else if (move.StartsWith("turn off "))
            {
                result.Action = "off";
                move = move.Replace("turn off ", "");
            }
            else if (move.StartsWith("toggle "))
            {
                result.Action = "toggle";
                move = move.Replace("toggle ", "");
            }

            var parts = move.Split(" through ");
            result.From = new Tuple<int, int>(int.Parse(parts[0].Split(",")[0]), int.Parse(parts[0].Split(",")[1]));
            result.To = new Tuple<int, int>(int.Parse(parts[1].Split(",")[0]), int.Parse(parts[1].Split(",")[1]));

            return result;
        }
    }
}