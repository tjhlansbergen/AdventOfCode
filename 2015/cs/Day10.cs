namespace Runner;

public class Day10
{
    public static void Run()
    {
        var input = "3113322113";
        
        for (int i = 0; i < 50; i++)
        {
            char cur = ' ';
            int count = 1;
            var result = new List<string>();

            foreach (var c in input)
            {
                if (c == cur)
                {
                    count++;
                }
                else
                {
                    if (cur != ' ')
                    {
                        result.Add(count.ToString());
                        result.Add(cur.ToString());
                    }

                    cur = c;
                    count = 1;
                }
            }
            result.Add(count.ToString());
            result.Add(cur.ToString());

            input = string.Concat(result);

            System.Console.WriteLine($"n: {i+1}, length: {result.Count}");
        }

        System.Console.WriteLine(input.Length);

    }
}