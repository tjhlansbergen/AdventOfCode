namespace AocRunner;

public class Day8
{
    public static void Run(string input, string[] lines)
    {
        var display = new Display();
        display.Print();
        
        foreach (var line in lines)
        {
            var parts = line.Replace("x=", "").Replace("y=", "").Split(' ');

            if (parts[0] == "rect")
            {
                var dimensions = parts[1].Split('x').Select(d => int.Parse(d)).ToArray();
                display.Rect(dimensions[0], dimensions[1]);
            }
            else
            {
                if(parts[1] == "row")
                {
                    display.RotateRow(int.Parse(parts[2]), int.Parse(parts[4]));
                }
                else
                {
                    display.RotateColumn(int.Parse(parts[2]), int.Parse(parts[4]));
                }
            }

            display.Print();
            Thread.Sleep(100);
            //System.Console.ReadLine();
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
        public void RotateColumn(int column, int by) => _rotate(column, by, false);
        
        private void _rotate(int which, int by, bool row)
        {
            by = (row) ? by % _width : by % _height;
            int length = (row) ? _width : _height;

            var gridCopy = (bool[,])_grid.Clone();

            for (int i = 0; i < length; i++)
            {
                if (row) { _grid[i, which] = gridCopy[i - by > -1 ? i - by : _width - by + i, which]; }
                else { _grid[which, i] = gridCopy[which, i - by > -1 ? i - by : _height - by + i]; }
            }
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
            System.Console.WriteLine(_grid.Cast<bool>().Count(p => p == true));
        }
    }
}