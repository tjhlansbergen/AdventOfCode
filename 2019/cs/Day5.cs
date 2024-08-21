namespace AocRunner;

public class Day5
{
    public static void Run(string input, string[] lines)
    {
        var intcode = input.Split(',').Select(c => int.Parse(c)).ToList();
        var result = Process(intcode, [1]);
    }

    public static IEnumerable<int> Process(List<int> intcode, IEnumerable<int> inputs)
    {
        var position = 0;
        var outputs = new List<int>();


        while (true)
        {
            var opcode = Opcode(intcode[position]);
            var param = ParameterModes(intcode[position]);
            int progress;

            if (opcode == 99) break;

            switch (opcode)
            {
                case 1:
                    intcode[intcode[position + 3]] = 
                        (paramModeSafe(0, param) == 0 ? intcode[intcode[position + 1]] : intcode[position + 1]) + 
                        (paramModeSafe(1, param) == 0 ? intcode[intcode[position + 2]] : intcode[position + 2]);
                    progress = 4;
                    break;
                case 2:
                    intcode[intcode[position + 3]] = 
                        (paramModeSafe(0, param) == 0 ? intcode[intcode[position + 1]] : intcode[position + 1]) * 
                        (paramModeSafe(1, param) == 0 ? intcode[intcode[position + 2]] : intcode[position + 2]);
                    progress = 4;
                    break;
                case 3:
                    intcode[intcode[position + 1]] = inputs.First();
                    inputs = inputs.Skip(1);
                    progress = 2;
                    break;
                case 4:
                    var output = paramModeSafe(0, param) == 0 ? intcode[intcode[position + 1]] : intcode[position + 1];
                    System.Console.WriteLine($"{output}, (position: {position})");
                    outputs.Add(output);
                    progress = 2;
                    break;
                default:
                    throw new ArgumentException("Unknown opcode");
            }           

            position += progress;
        }

        return outputs;

    }



    private static int Opcode(int instruction)
    {
        return int.Parse(string.Join(string.Empty, instruction.ToString().TakeLast(2)));
    }

    private static int[] ParameterModes(int instruction)
    {
        return instruction
            .ToString()
            .Reverse()
            .Skip(2)
            .Select(i => i - '0')
            .ToArray();
    }

    private static int paramModeSafe(int index, int[] param)
    {
        return param.Length > index 
            ? param[index] 
            : 0;
    }

}