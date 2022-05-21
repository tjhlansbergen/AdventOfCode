namespace Runner
{
    internal static class Day3
    {
        internal static void Run()
        {
            
            var lines = File.ReadAllLines("../inputs/day3");
            var visited = new List<Tuple<int, int>>{new Tuple<int, int>(0,0)};

            int x = 0, y = 0;
            int xr = 0, yr = 0;

            var robo = false;

            foreach (char c in lines[0])
            {
                (int, int) result;

                if(robo)
                {
                    result = Move(xr, yr, c);
                    xr = result.Item1;
                    yr = result.Item2;
                }
                else
                {
                    result = Move(x, y, c);
                    x = result.Item1;
                    y = result.Item2;
                }

                visited.Add(new Tuple<int, int>(result.Item1, result.Item2));
                robo = !robo;
            }

            System.Console.WriteLine(visited.Distinct().Count());
        }

        internal static (int, int) Move(int x, int y, char c)
        {
            switch(c)
            {
                case '>':
                    x += 1;
                break;
                case '<':
                    x -= 1;
                break;
                case '^':
                    y -= 1;
                break;
                case 'v':
                    y += 1;
                break;
            }

            return (x, y);
        }
    }
}