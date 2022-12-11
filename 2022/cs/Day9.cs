namespace AocRunner;

public class Day9
{
    public interface INode
    {
        int X { get; }
        int Y { get; }

        void Move(string direction, bool stayed);
    }

    public class Head : INode
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Head(int x, int y)
        {
            X = X;
            Y = Y;
        }

        public void Move(string direction, bool _)
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

    public class Tail : INode
    {
        public record Visit()
        {
            public Tuple<int, int> Position { get; set; } = Tuple.Create(0,0);
            public bool Stayed { get; set; }
        }

        public List<Visit> Route { get; private set; } = new List<Visit>();

        public int X => Route.Last().Position.Item1;
        public int Y => Route.Last().Position.Item2;

        public Tail(int x, int y)
        {
            Route.Add(new Visit { Position = new Tuple<int, int>(x, y), Stayed = true });
        }

        public void Move(string direction, bool stayed)
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
            Route.Add(new Visit { Position = new Tuple<int, int>(x, y), Stayed = stayed });
        }
    }

    public static void Run(string input, string[] lines)
    {
        var head = new Head(0, 0);
        var tails = new List<Tail>();
        for (int i = 0; i < 9; i++) { tails.Add(new Tail(0, 0)); }

        foreach (var line in lines)
        {
            var direction = line.Split(' ')[0];
            var distance = int.Parse(line.Split(' ')[1]);

            for (int d = 0; d < distance; d++)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (i == 0) Turn(direction, head, tails[i], i);
                    else Turn(direction, tails[i - 1], tails[i], i);
                }
            }

        }

        System.Console.WriteLine($"Part 1: {tails.First().Route.Where(r => r.Stayed == true).Select(r => r.Position).Distinct().Count()}");
        System.Console.WriteLine($"Part 2: {tails.Last().Route.Where(r => r.Stayed == true).Select(r => r.Position).Distinct().Count()}");
    }

    public static void Turn(string direction, INode head, INode tail, int t)
    {
        // this is a mess and needs refactoring

        if (t == 0) head.Move(direction, false);
        
        // there's three ways in which the tail can follow the head, 
        // 1) Head and tail collide, we do nothing
        // 1b&c) Head moved but tail is still close, we do nothing
        // 2) Head moves away but in a row or column of tail, we follow
        // 3) Head moves away diagonally opposed to tail

        // 1)
        if (head.X == tail.X && head.Y == tail.Y)
        {
            return;
        }

        // 1b)
        if ((head.X == tail.X && Math.Abs(head.Y - tail.Y) == 1)
         || (head.Y == tail.Y && Math.Abs(head.X - tail.X) == 1))
        {
            return;
        }

        // 1c)
        if (Math.Abs(head.Y - tail.Y) == 1 && Math.Abs(head.X - tail.X) == 1)
        {
            return;
        }

        // 2a)
        if (head.X == tail.X && Math.Abs(head.Y - tail.Y) == 2)
        {
            if (head.Y > tail.Y) tail.Move("D", true);
            else tail.Move("U", true);
            return;
        }

        // 2b)
        if (head.Y == tail.Y && Math.Abs(head.X - tail.X) == 2)
        {
            if (head.X > tail.X) tail.Move("R", true);
            else tail.Move("L", true);
            return;
        }

        // 3)
        // if we ended up here
        if (Math.Abs(head.Y - tail.Y) == 2)
        {
            if (head.Y > tail.Y) tail.Move("D", false);
            else tail.Move("U", false);

            if (head.X > tail.X) tail.Move("R", true);
            else tail.Move("L", true);
        }
        if (Math.Abs(head.X - tail.X) == 2)
        {
            if (head.X > tail.X) tail.Move("R", false);
            else tail.Move("L", false);

            if (head.Y > tail.Y) tail.Move("D", true);
            else tail.Move("U", true);
        }
    }
}