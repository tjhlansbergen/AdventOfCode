using System.Collections.Immutable;
using System.Net;

namespace AocRunner;

public class Day8
{
    public class Instruction
    {
        public string OperationRegister { get; set; }
        public int OperationAmount { get; set; }
        public Func<int,int,int> Operation { get; set; }
        
        public string ConditionRegister { get; set; }
        public int ConditionAmount { get; set; }
        public Func<int,int,bool> Condition { get; set; }

    }

    public static void Run(string input, string[] lines)
    {
        var instructions = lines.Select(l => Parse(l));
        var register = new Dictionary<string, int>();
        var max = 0;

        foreach (var i in instructions)
        {
            if (!register.ContainsKey(i.OperationRegister)) register.Add(i.OperationRegister, 0);
            if (!register.ContainsKey(i.ConditionRegister)) register.Add(i.ConditionRegister, 0);

            if (i.Condition(register[i.ConditionRegister], i.ConditionAmount))
            {
                register[i.OperationRegister] = i.Operation(register[i.OperationRegister], i.OperationAmount);
                max = Math.Max(register.Values.Max(), max);
            }
        }

        System.Console.WriteLine($"Part 1: {register.Values.Max()}");
        System.Console.WriteLine($"Part 2: {max}");
    }

    private static Instruction Parse(string line)
    {
        var result = new Instruction();

        var left = line.Split("if")[0].Trim();
        var right = line.Split("if")[1].Trim();

        var lefts = left.Split(' ');
        result.OperationRegister = lefts[0];
        result.OperationAmount = int.Parse(lefts[2]);
        result.Operation = lefts[1] == "inc" ? (a,b) => a + b : (a,b) => a - b;


        var rights = right.Split(' ');
        result.ConditionRegister = rights[0];
        result.ConditionAmount = int.Parse(rights[2]);
        
        switch (rights[1])
        {
            case ">":
                result.Condition = (a, b) =>  a > b;
                break;
            case "<":
                result.Condition = (a, b) =>  a < b;
                break;
            case ">=":
                result.Condition = (a, b) =>  a >= b;
                break;
            case "<=":
                result.Condition = (a, b) =>  a <= b;
                break;
            case "==":
                result.Condition = (a, b) =>  a == b;
                break;
            case "!=":
                result.Condition = (a, b) =>  a != b;
                break;
        }

        return result;
    }
}