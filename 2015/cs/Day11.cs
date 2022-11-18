namespace Runner;

public class Day11
{
    public static void Run()
    {
        var input = "hepxcrrq".ToCharArray();

        do
        {
            input = Increment(input);
            //System.Console.WriteLine(input);
        }
        while (!IsValid(input));
        System.Console.WriteLine(input);
        


        bool IsValid(char[] input)
        {
            // i, o, l
            if( new[] {'i', 'o', 'l'}.Any(l => input.Contains(l))) return false;

            // abc && aa bb
            for (int i = 0; i < input.Length - 2; i++)
            {
                if (input[i+2] - input[i+1] == 1 && input[i+1] - input[i] == 1)
                {
                    // found a series of three
                    break;
                }
                if(i == input.Length - 3) 
                {
                    return false;
                }
            }

            // aa bb
            var pairs = new List<string>();
            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == input[i+1])
                {
                    pairs.Add($"{input[i]}{input[i+1]}");
                    i++;
                }
            }
            if (pairs.Count < 2 || pairs[0] == pairs[1]) 
            {
                return false;
            }

            return true;
        }


        char[] Increment(char[] input)
        {
            // a = 97
            // z = 122

            int len = input.Length - 1;
            for (int i = len; i > -1; i--)
            {
                if (input[i] != 'z')
                {
                    input[i]++;
                    //if (i < len-1) {System.Console.WriteLine(input);}
                    return input;
                }
                else
                {
                    input[i] = 'a';
                    if (i == 0)
                    {
                        input = input.Prepend('a').ToArray();
                    }
                }
            }

            return input;
        }

    }

}