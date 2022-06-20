namespace Runner
{
    public class Day7
    {
        public enum Method
        {
            AND,
            OR,
            LSHIFT,
            RSHIFT,
            NOT,
            NONE,
        }

        public class Gate
        {
            public string[] IN { get; set; }
            public string OUT { get; set; }
            public Method Method { get; set; }
        }

        private static IEnumerable<Gate> _gates;
        private static Dictionary<string, string> _wires = new Dictionary<string, string>();

        internal static void Run()
        {
            var lines = File.ReadAllLines("../inputs/day7");

            _gates = lines.Select(l => Parse(l)).ToList();

            var a = Get("a");
            System.Console.WriteLine(a);

            // fart two
            _wires = new Dictionary<string, string>();
            _gates = lines.Select(l => Parse(l)).ToList();
            _wires["b"] = a;

            var a2 = Get("a");
            System.Console.WriteLine(a2);

        }

        public static string Get(string wire)
        {

            if(_wires.ContainsKey(wire))
            {
                return _wires[wire];
            }

            // numerical, short-circuit and echo it back
            if (int.TryParse(wire, out _)) return wire;

            // get the gate
            var gate = _gates.Single(g => g.OUT == wire);
            var result = string.Empty;

            switch (gate.Method)
            {
                case Method.NONE:
                    result = Get(gate.IN[0]);
                    break;
                case Method.NOT:
                    result = ((ushort)~ushort.Parse(Get(gate.IN[0]))).ToString();
                    break;
                case Method.AND:
                    result = (int.Parse(Get(gate.IN[0])) & 
                            int.Parse(Get(gate.IN[1])))
                            .ToString();
                    break;
                case Method.OR:
                    result = (int.Parse(Get(gate.IN[0])) | 
                            int.Parse(Get(gate.IN[1])))
                            .ToString();
                    break;
                case Method.LSHIFT:
                    result = (int.Parse(Get(gate.IN[0])) << int.Parse(gate.IN[1]))
                            .ToString();
                    break;
                case Method.RSHIFT:
                    result = (int.Parse(Get(gate.IN[0])) >> int.Parse(gate.IN[1]))
                            .ToString();
                    break;
                default:
                    return result;
            }

            if(int.TryParse(result, out _))
            {
                _wires.Add(wire, result);
            }

            return result;
        }

        public static Gate Parse(string line)
        {
            // no input validation whatsoever...

            var result = new Gate();

            var parts = line.Split(" -> ");
            result.OUT = parts[1];

            parts = parts[0].Split(' ');
            switch (parts.Count())
            {
                case 1:
                    result.Method = Method.NONE;
                    result.IN = new[] { parts[0] };
                    if(int.TryParse(parts[0], out _)) _wires.Add(result.OUT, parts[0]);
                    break;
                case 2:
                    result.Method = Method.NOT;
                    result.IN = new[] { parts[1] };
                    break;
                case 3:
                    result.Method = Enum.Parse<Method>(parts[1]);
                    result.IN = new[] { parts[0], parts[2] };
                    break;
            }

            return result;
        }

    }
}