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

        public Sensor(int x, int y, Beacon beacon)
        {
            X = x;
            Y = y;
            Span = Math.Abs(X - beacon.X) + Math.Abs(Y - beacon.Y);
        }

        public bool Blocks(int x, int y, out int skip)
        {
            skip = 0;
            var yy = Math.Abs(Y - y);
            if (Math.Abs(X - x) + yy <= Span)
            {
                skip = X + Span - yy - x;
                return true;
            }

            return false;
        }
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
            if (sensors.Any(s => s.Blocks(x,y, out _)))
            {
                count++;
            }
        }

        System.Console.WriteLine($"Part 1: {count}");

        var brk = false;
        long part2 = 0;

        for (int j = 0; j <= 4000000; j++)
        {
            for (int i = 0; i <= 4000000; i++)
            {
                if (beacons.Any(b => b.X == i && b.Y == j))
                {
                    continue;
                }
                int skip = 0;
                if (sensors.Any(s => s.Blocks(i, j, out skip)))
                {
                    i+=skip;
                    continue;
                }

                part2 = ((long)i * 4000000) + j;
                brk = true;
                break;
            }

            if (brk) break;
        }

        System.Console.WriteLine($"Part 2: {part2}");
    }
}
