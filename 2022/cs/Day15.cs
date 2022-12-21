namespace AocRunner;

public class Day15
{
    public record Beacon
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Beacon(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public record Sensor
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Span { get; private set; }

        //public List<Tuple<int, int>> Shadow { get; private set; } = new List<Tuple<int, int>>();

        public Sensor(int x, int y, Beacon beacon)
        {
            X = x;
            Y = y;
            Span = Math.Abs(X - beacon.X) + Math.Abs(Y - beacon.Y);

            // int r = Span;
            // for (int yy = Y - Span; yy <= Y + Span; yy++)
            // {
            //     for (int xx = X - Span + r; xx <= X + Span - r; xx++)
            //     {
            //         Shadow.Add(Tuple.Create(xx,yy));
            //     }

            //     if (yy < Y) r--;
            //     else r++;
            // }
        }

        public bool Blocks(int x, int y) => Math.Abs(X - x) + Math.Abs(Y - y) <= Span;

    }

    public static void Run(string input, string[] lines)
    {
        var beacons = new List<Beacon>();
        var sensors = new List<Sensor>();

        foreach (var line in lines)
        {
            var parts = line.Replace("x=", string.Empty).Replace("y=", string.Empty).Split(':');
            var beaconParts = parts[1].Replace(" closest beacon is at ", string.Empty).Split(',');
            var sensorParts = parts[0].Replace("Sensor at ", string.Empty).Split(',');
            var beacon = new Beacon(int.Parse(beaconParts[0]), int.Parse(beaconParts[1]));
            var sensor = new Sensor(int.Parse(sensorParts[0]), int.Parse(sensorParts[1]), beacon);
            beacons.Add(beacon);
            sensors.Add(sensor);
        }

        var left = sensors.Select(b => b.X - b.Span).Min();
        var right = sensors.Select(b => b.X + b.Span).Max();

        var y = 2000000;
        var count = 0;

        for (int x = left; x <= right; x++)
        {
            if (beacons.Any(b => b.X == x && b.Y == y))
            {
                continue;
            }
            if (sensors.Any(s => s.Blocks(x,y)))
            {
                count++;
            }
        }

        System.Console.WriteLine($"Part 1: {count}");

        // for (int j = 0; j <= 4000000; j++)
        // {
        //     for (int i = 0; i <= 4000000; i++)
        //     {
        //         if (beacons.Any(b => b.X == i && b.Y == j))
        //         {
        //             continue;
        //         }
        //         if (sensors.Any(s => s.Blocks(i, j)))
        //         {
        //             continue;
        //         }

        //         System.Console.WriteLine($"x: {i}, y: {j}, {(i*4000000)+j}");
        //         break;
        //     }
        // }
    }
}
