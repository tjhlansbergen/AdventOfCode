namespace AocRunner;

public class Day11
{
    public static void Run(string input, string[] lines)
    {
        var floors = Enumerable.Range(0, lines.Length).Select(i => new {Number = i+1, Floor = ParseLine(lines[i]) }).ToDictionary(x => x.Number, x => x.Floor);
        var container = new Container { Elevator = new Elevator(), Floors = floors };
        PrintContainer(container);

        // microchips may not be together with generators other than their own

        // get possible moves
        // pick move that get the most stuff 'up' 
        // continue until everything is on floor 4


    }

    private static void GetMoves(Container container)
    {

    }

    public class Unit
    {
        public required string Substance { get; set; }
        public required string Type { get; set; }
    }

    public class Floor
    {
        public List<Unit> Units { get; set; } = new List<Unit>();
    }

    public class Elevator
    {
        public int Floor { get; set; } = 1;
        public List<Unit> Units { get; set; } = new List<Unit>();
    }

    public class Container
    {
        public Dictionary<int, Floor> Floors { get; set; } = new Dictionary<int, Floor>();
        public required Elevator Elevator { get; set; }
    }

    private static Floor ParseLine(string line)
    {
        var split = line.Split("contains")[1];

        if (split.Trim() == "nothing relevant.")
        {
            return new Floor();
        }

        var clean = split.Replace(",", " ").Replace(".", " ").Replace("and", " ");
        var splits = clean.Split("a ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var units = splits.Select(s => new Unit {Substance = s.Split(' ')[0][..1].ToUpper(), Type = s.Split(' ')[1][..1].ToUpper() });

        return new Floor { Units = units.ToList() };
    }

    private static void PrintContainer(Container container)
    {
        foreach (var kvp in container.Floors.OrderByDescending(f => f.Key))
        {
            System.Console.Write($"F{kvp.Key}");
            if (container.Elevator.Floor == kvp.Key)
                System.Console.Write(" E");
            else
                System.Console.Write(" .");
            foreach (var unit in kvp.Value.Units)
            {
                System.Console.Write($" {unit.Substance}{unit.Type}");
            }
            System.Console.WriteLine();
        }
    }
}