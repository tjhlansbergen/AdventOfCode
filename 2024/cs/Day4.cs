

namespace AocRunner;

public class Day4
{
    // directions:
    // 012
    // 7x3
    // 654
    const string L = "MAS";



    public static void Run(string input, string[] lines)
    {

        var count = 0;
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                count += ReadAll(x, y, lines);
            }
        }

        Console.WriteLine($"Part 1: {count}");

        count = 0;
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (ProofMas(lines, x, y))
                {
                    count++;
                }
            }
        }

        Console.WriteLine($"Part 2: {count}");

    }

    public static bool ProofMas(string[] lines, int x, int y)
    {
        if (lines[y][x] != 'A')
        {
            return false;
        }

        var lt = ReadOrDefault(x - 1, y - 1, lines);
        var rt = ReadOrDefault(x + 1, y - 1, lines);
        var lb = ReadOrDefault(x - 1, y + 1, lines);
        var rb = ReadOrDefault(x + 1, y + 1, lines);

        var a = lt == 'M' && rb == 'S' || lt == 'S' && rb == 'M';
        var b = rt == 'M' && lb == 'S' || rt == 'S' && lb == 'M';

        return a && b;
    }

    public static int ReadAll(int x, int y, string[] lines)
    {
        var count = 0;

        if (ReadOrDefault(x, y, lines) != 'X')
        {
            return count;
        }

        if (Read(x, y, -1, -1, lines)) { count++; } // 0
        if (Read(x, y,  0, -1, lines)) { count++; } // 1
        if (Read(x, y,  1, -1, lines)) { count++; } // 2
        if (Read(x, y,  1,  0, lines)) { count++; } // 3
        if (Read(x, y,  1,  1, lines)) { count++; } // 4
        if (Read(x, y,  0,  1, lines)) { count++; } // 5
        if (Read(x, y, -1,  1, lines)) { count++; } // 6
        if (Read(x, y, -1,  0, lines)) { count++; } // 7

        return count;
    }

    public static bool Read(int x, int y, int dx, int dy, string[] lines)
    {
        for (int i = 0; i < L.Length; i++)
        {
            x += dx;
            y += dy;

            if (ReadOrDefault(x, y, lines) != L[i])
            {
                return false;
            }
        }

        return true;
    }

    public static char ReadOrDefault(int x, int y, string[] lines)
    {
        try
        {
            return lines[y][x];
        }
        catch (IndexOutOfRangeException)
        {
            return '.';
        }
    }
}