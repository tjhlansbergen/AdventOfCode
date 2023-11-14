namespace AocRunner;

public class Day9
{

    public static void Run(string input, string[] lines)
    {
        var result = Score(input);
        System.Console.WriteLine($"Part 1: {result.Item1}");
        System.Console.WriteLine($"Part 2: {result.Item2}");
    }

    private static (int, int) Score(string stream)
    {
        var len = stream.Length;
        var level = 0;
        var halt = false;
        var score = 0;
        var garbage = 0;

        for (int i = 0; i < len; i++)
        {
            var ch = stream[i];

            switch (ch)
            {
                case '{':
                    if (!halt) level++;
                    break;
                case '}':
                    if (!halt) 
                    {
                        score += level;
                        level--;
                    }
                    break;
                case '<':
                    if (!halt)
                    {   
                        halt = true;
                        garbage--;
                    } 
                    break;
                case '>':
                    if (halt) halt = false;
                    break;
                case '!':
                    if (halt) 
                    {
                        i++;
                        garbage--;
                    }
                    break;
            }

            if (halt) garbage++;
        }

        return (score, garbage);
    }
}