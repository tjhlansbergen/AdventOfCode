namespace AocRunner;

public class Day9
{
    public static void Run(string input, string[] lines)
    {
        Part1(input);
        Part2(input);
    }

    public static void Part1(string input)
    {
        int count = 0;
        bool marking = false;
        string marker = string.Empty;

        for (int i = 0; i < input.Length; i++)
        {
            if (marking)
            {
                if (input[i] == ')')
                {
                    // stop marker
                    marking = false;
                    var parsedMarker = Parse(marker);
                    marker = string.Empty;
                    i += parsedMarker.Item1;
                    count += parsedMarker.Item1 * parsedMarker.Item2;
                }
                else
                {
                    marker += input[i];
                }
            }
            else
            {
                if (input[i] == '(')
                {
                    // start marker
                    marking = true;
                }
                else
                {
                    count++;
                }
            }
        }

        System.Console.WriteLine($"Part 1: {count}");


    }

    public static (int, int) Parse(string marker)
    {
        var parts = marker.Split('x');
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }

    public static void Part2(string input)
    {
        long count = 0;     // <-- damn you int.MaxValue!
        bool marking = false;
        string marker = string.Empty;

        var markerRepo = new Markers();

        for (int i = 0; i < input.Length; i++)
        {
            markerRepo.Tick();

            if (marking)
            {
                if (input[i] == ')')
                {
                    // stop marker
                    marking = false;
                    var parsedMarker = Parse(marker);
                    marker = string.Empty;
                    markerRepo.Add(parsedMarker.Item2, parsedMarker.Item1);
                }
                else
                {
                    marker += input[i];
                }
            }
            else
            {
                if (input[i] == '(')
                {
                    // start marker
                    marking = true;
                }
                else
                {
                    count += markerRepo.CombinedFactor;
                }
            }
        }

        System.Console.WriteLine($"Part 2: {count}");
    }

    public class Markers
    {
        public class Marker
        {
            public int Factor { get; set; }
            public int Counter { get; set; }

            public Marker(int factor, int counter)
            {
                Factor = factor;
                Counter = counter;
            }
        }

        private List<Marker> _markers = new List<Marker>();

        public void Add(int factor, int counter)
        {
            _markers.Add(new Marker(factor, counter + 1));
        }

        public void Tick()
        {
            foreach (var m in _markers)
            {
                m.Counter -= 1;
            }

            _markers = _markers.Where(m => m.Counter > 0).ToList();
        }

        public int CombinedFactor => _markers.Select(m => m.Factor).Aggregate(1, (acc, val) => acc * val);
    }
}