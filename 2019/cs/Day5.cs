
namespace AocRunner;

public class Day5
{
    public static void Run(string input, string[] lines)
    {
        var intcode = input.Split(',').Select(c => int.Parse(c)).ToList();
        var machine = new Machine(intcode, new Queue<int>([1]));
        
        var result = machine.Process();
    }
}

public class Machine
{
    private int _position;
    private List<int> _intcode;
    private Queue<int> _inputs;

    public Machine(List<int> intcode, Queue<int> inputs)
    {
        // init
        _position = 0;
        _intcode = intcode;
        _inputs = inputs;
    }

    public IEnumerable<int> Process()
    {
        var outputs = new List<int>();


        while (true)
        {
            var opcode = Opcode();
            var mode = ParameterModes();
            int progress;

            if (opcode == 99) break;

            switch (opcode)
            {
                case 1:
                    _intcode[_intcode[_position + 3]] =
                        GetParameter(0, mode) +
                        GetParameter(1, mode);
                    progress = 4;
                    break;
                case 2:
                    _intcode[_intcode[_position + 3]] =
                        GetParameter(0, mode) *
                        GetParameter(1, mode);
                    progress = 4;
                    break;
                case 3:
                    _intcode[_intcode[_position + 1]] = _inputs.Dequeue();
                    progress = 2;
                    break;
                case 4:
                    var output = GetParameter(0, mode);
                    System.Console.WriteLine($"{output}, (position: {_position})");
                    outputs.Add(output);
                    progress = 2;
                    break;
                default:
                    throw new ArgumentException("Unknown opcode");
            }

            _position += progress;
        }

        return outputs;

    }

    private int Opcode()
    {
        return int.Parse(string.Join(string.Empty, _intcode[_position].ToString().TakeLast(2)));
    }

    private int[] ParameterModes()
    {
        return _intcode[_position]
            .ToString()
            .Reverse()
            .Skip(2)
            .Select(i => i - '0')
            .ToArray();
    }

    private int GetParameter(int index, int[] mode)
    {
        var positional = mode.Length <= index || mode[index] == 0;

        return positional
            ? _intcode[_intcode[_position + index + 1]]
            : _intcode[_position + index + 1];

    }
}