namespace AocRunner;

public class Day8
{
    public static void Run(string input, string[] lines)
    {
        var display = new Display();
        display.Print();
        System.Console.ReadLine();
        display.Rect(10, 3);
        display.Print();
        Console.ReadLine();

        while (true)
        {
            display.RotateColumn(1, 1);
            display.Print();
            System.Console.ReadLine();
        }
    }

    public class Display
    {
        private bool[,] _grid = new bool[50, 6];
        private int _width => _grid.GetLength(0);
        private int _height => _grid.GetLength(1);

        public void Rect(int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    _grid[j, i] = true;
                }
            }
        }

        public void RotateRow(int row, int by) => _rotate(row, by, true);
        public void RotateColumn(int row, int by) => _rotate(row, by, false);
        
        private void _rotate(int which, int by, bool row)
        {
            by = (row) ? by % _width : by % _height;
            int length = (row) ? _width : _height;

            bool turnover = (row) ? _grid[length -1, which] : _grid[which, length -1];

            for (int i = length - 1; i > 0; i--)
            {
                if (row)
                {
                    
                    _grid[i, which] = _grid[i - by > -1 ? i - by : _width - by + i, which];
                    
                }
                else
                {
                    
                    _grid[which, i] = _grid[which, i - by > -1 ? i - by : _height - by + i];
            
                }

            }

            if (row) { turnover = _grid[which,0]; } else { _grid[which,0] = turnover; }
        }

        public void Print()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            System.Console.WriteLine();
            for (int i = 0; i < _width + 2; i++) { System.Console.Write("+"); }
            System.Console.WriteLine();

            for (int i = 0; i < _height; i++)
            {
                System.Console.Write("+");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                for (int j = 0; j < _width; j++)
                {
                    if (_grid[j, i]) { Console.Write("#"); }
                    else { Console.Write(" "); }
                }
                Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write("+");
                System.Console.WriteLine();
            }

            for (int i = 0; i < _width + 2; i++) { System.Console.Write("+"); }
            System.Console.WriteLine();
            System.Console.WriteLine();
        }
    }
}