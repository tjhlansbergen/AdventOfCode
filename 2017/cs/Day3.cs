namespace AocRunner;

public class Day3
{
    public static void Run(string input, string[] lines)
    {
        int x = 0, y = 0;
        int direction = 1;
        int steps = 1;
        int count = 0;

        for (int i = 1; i < int.Parse(input); i++)
        {
            // move
            switch (direction)
            {
                case 1: // right 
                    x += 1;
                    break;
                case 2: // up
                    y -= 1;
                    break;
                case 3: // left
                    x -= 1;
                    break;
                case 4: // down
                    y += 1;
                    break;
            }
            
            count++;
            if (count == steps) 
            {
                direction = (direction == 4) ? direction = 1 : direction = direction + 1;
                if (direction % 2 != 0) steps++;

                count = 0;
            }
        }

        System.Console.WriteLine($"Part 1: {Math.Abs(x)+Math.Abs(y)}");
    }
}