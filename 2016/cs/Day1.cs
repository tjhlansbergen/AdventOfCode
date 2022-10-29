namespace AocRunner;

public class Day1
{
    public static void Run(string input, string[] lines)
    {
        var moves = input.Split(',').Select(m => m.Trim()).ToArray();
        int x = 0, y = 0;
        int direction = 0; // 0, 90, 180, 270       

        foreach (var move in moves)
        {
            var turn = move.Substring(0, 1);
            var steps = int.Parse(move.Substring(1, move.Length - 1));


            direction += turn == "R" ? 90 : -90;
            if (direction == 360) direction = 0;
            if (direction == -90) direction = 270;

            switch (direction)
            {
                case 0:
                    y += steps;
                    break;
                case 90:
                    x += steps;
                    break;
                case 180:
                    y -= steps;
                    break;
                case 270:
                    x -= steps;
                    break;
                default:
                    throw new InvalidOperationException($"Invalid direction: {direction}");
            }
        }

        System.Console.WriteLine($"part 1: {Math.Abs(x) + Math.Abs(y)}");
    }
}