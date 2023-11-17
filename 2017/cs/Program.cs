using System.Diagnostics;
using System.Reflection;

namespace AocRunner;

internal class Program
{
    private static void Main(string[] args)
    {
        int day;

        if (args.Length == 2 && args[0] == "prep" && int.TryParse(args[1], out day))
        {
            PrepDay(day);
            return;
        }

        if (args.Length == 1 && int.TryParse(args[0], out day))
        {
            RunDay(day);
            return;
        }

        Console.Write("Day:? ");
        if (int.TryParse(Console.ReadLine(), out day))
        {
            RunDay(day);
            return;
        }

        System.Console.WriteLine("Don't know what to do, bye");
    }

    private static bool RunDay(int day)
    {
        // get input
        var inputsFile = $"../inputs/day{day}";
        if (!File.Exists(inputsFile)) return LogAndExit($"Input file for day {day} not found (at {inputsFile})");

        var input = File.ReadAllText(inputsFile);
        var lines = File.ReadAllLines(inputsFile);

        // get class for given day
        Type? type = Type.GetType($"AocRunner.Day{day}");
        if (type == null) return LogAndExit($"No class found with name Day{day}");


        // get run method
        MethodInfo? method = type.GetMethod("Run", BindingFlags.Static | BindingFlags.Public);

        if (method == null) 
        { 
            Console.WriteLine($"No Run method for class with name Day{day}"); 
            return;
        }

        System.Console.WriteLine();
        System.Console.WriteLine("##############################");
        Console.WriteLine($"Running day {day}");
        Console.WriteLine();

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        method.Invoke(null, new object[] { input, lines });

        stopwatch.Stop();

        Console.WriteLine();
        System.Console.WriteLine($"Elapsed Time: {stopwatch.Elapsed}");
        System.Console.WriteLine("##############################");
        System.Console.WriteLine();
    }
}