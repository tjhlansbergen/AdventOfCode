namespace AocRunner;

public class Day9
{
    public class Head
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Head(int x, int y)
        {
            X = X;
            Y = Y;
        }

        public void Move(string direction)
        {
            switch (direction)
            {
                case "U":
                    Y--;
                    break;
                case "R":
                    X++;
                    break;
                case "D":
                    Y++;
                    break;
                case "L":
                    X--;
                    break;
            }
        }
    }

    public class Tail
    {
        public record Visit()
        {
            public Tuple<int, int> Position { get; set; }
            public bool Stayed { get; set; }
        }

        public List<Visit> Route { get; private set; } = new List<Visit>();

        public int X => Route.Last().Position.Item1;
        public int Y => Route.Last().Position.Item2;

        public Tail(int x, int y)
        {
            Route.Add(new Visit{ Position = new Tuple<int, int>(x, y), Stayed = true });
        }

        public void Move(string direction, bool stayed = true)
        {
            int x = X, y = Y;
            switch (direction)
            {
                case "U":
                    y--;
                    break;
                case "R":
                    x++;
                    break;
                case "D":
                    y++;
                    break;
                case "L":
                    x--;
                    break;
            }
            Route.Add(new Visit{ Position = new Tuple<int, int>(x, y), Stayed = stayed });
        }
    }

    public static void Run(string input, string[] lines)
    {
        var head = new Head(0, 0);
        var tail = new Tail(0, 0);

        foreach (var line in lines)
        {
            var direction = line.Split(' ')[0];
            var distance = int.Parse(line.Split(' ')[1]);

            for (int i = 0; i < distance; i++)
            {
                head.Move(direction);
                System.Console.Write($"Head: {head.X},{head.Y}");

                // there's three ways in which the tail can follow the head, 
                // 1) Head and tail collide, we do nothing
                // 1b) Head moved but tail is still close, we do nothing
                // 2) Head moves away but in a row or column of tail, we follow
                // 3) Head moves away diagonally opposed to tail

                // 1)
                if (head.X == tail.X && head.Y == tail.Y)
                {
                    System.Console.WriteLine($"\tTail: {tail.X},{tail.Y}");
                    continue;
                }

                // 1b)
                if ((head.X == tail.X && Math.Abs(head.Y - tail.Y) == 1)
                 || (head.Y == tail.Y && Math.Abs(head.X - tail.X) == 1))
                {
                    System.Console.WriteLine($"\tTail: {tail.X},{tail.Y}");
                    continue;
                }

                // 1c)
                if (Math.Abs(head.Y - tail.Y) == 1 && Math.Abs(head.X - tail.X) == 1)
                {
                    System.Console.WriteLine($"\tTail: {tail.X},{tail.Y}");
                    continue;
                }

                // 2)
                if ((head.X == tail.X && Math.Abs(head.Y - tail.Y) == 2)
                 || (head.Y == tail.Y && Math.Abs(head.X - tail.X) == 2))
                {
                    tail.Move(direction);
                    System.Console.WriteLine($"\tTail: {tail.X},{tail.Y}");
                    continue;
                }

                // 3)
                // if we ended up here
                if (Math.Abs(head.Y - tail.Y) == 2)
                {
                    tail.Move(direction, false);

                    if (head.X > tail.X) tail.Move("R");
                    else tail.Move("L");
                }
                if (Math.Abs(head.X - tail.X) == 2)
                {
                    tail.Move(direction, false);

                    if (head.Y > tail.Y) tail.Move("D");
                    else tail.Move("U");
                }

                System.Console.WriteLine($"\tTail: {tail.X},{tail.Y}");
            }

            System.Console.WriteLine($"Part 1: {tail.Route.Where(r => r.Stayed == true).Select(r => r.Position).Distinct().Count()}");
        }
    }
}