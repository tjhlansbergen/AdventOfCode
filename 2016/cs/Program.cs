using System.Reflection;

namespace AocRunner;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Day:? ");

        if (int.TryParse(Console.ReadLine(), out int day))
        {
            var inputsFile = $"../inputs/day{day}";
            if (!File.Exists(inputsFile))
            {
                Console.WriteLine("Input file for day {day} not found (at {inputsFile})");
                return;
            }

            var input = File.ReadAllText(inputsFile);
            var lines = File.ReadAllLines(inputsFile);

            RunDay(day, input, lines);
        }
    }

    private static void RunDay(int day, string input, string[] lines)
    {
        Type? type = Type.GetType($"AocRunner.Day{day}");
        
        if (type == null) 
        { 
            Console.WriteLine($"No class found with name Day{day}"); 
            return;
        }
        
        MethodInfo? method = type.GetMethod("Run", BindingFlags.Static | BindingFlags.Public);

        if (method == null) 
        { 
            Console.WriteLine($"No Run method for class with name Day{day}"); 
            return;
        }

        Console.WriteLine($"Running day {day}");
        Console.WriteLine();
        method.Invoke(null, new object[] { input, lines });
    }
}