namespace AocRunner;

public class Day1
{
    public static void Run(string input, string[] lines)
    {
        var moves = input.Split(',').Select(m => m.Trim()).ToArray();
        var route = new List<Tuple<int, int>> { new Tuple<int, int>(0, 0) };
        int direction = 0; // 0, 90, 180, 270       

        foreach (var move in moves)
        {
            var turn = move.Substring(0, 1);
            var steps = int.Parse(move.Substring(1, move.Length - 1));

            direction += turn == "R" ? 90 : -90;
            if (direction == 360) direction = 0;
            if (direction == -90) direction = 270;

            for (int i = 0; i < steps; i++)
            {
                switch (direction)
                {
                    case 0:
                        route.Add(new Tuple<int, int>(route.Last().Item1, route.Last().Item2 + 1));
                        break;
                    case 90:
                        route.Add(new Tuple<int, int>(route.Last().Item1 + 1, route.Last().Item2));
                        break;
                    case 180:
                        route.Add(new Tuple<int, int>(route.Last().Item1, route.Last().Item2 - 1));
                        break;
                    case 270:
                        route.Add(new Tuple<int, int>(route.Last().Item1 - 1, route.Last().Item2));
                        break;
                    default:
                        throw new InvalidOperationException($"Invalid direction: {direction}");
                }
            }
        }

        // part 1
        System.Console.WriteLine($"part 1: {Math.Abs(route.Last().Item1) + Math.Abs(route.Last().Item2)}");

        // part 2
        var pos = route.Select((r, i) => new { r, i }).First(r => route.GetRange(0, r.i).Contains(r.r)).r;
        System.Console.WriteLine($"part 2: {Math.Abs(pos.Item1) + Math.Abs(pos.Item2)}");
    }
}