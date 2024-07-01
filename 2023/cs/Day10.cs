using System.ComponentModel;

namespace AocRunner;

public enum Direction
{
    NORTH,
    SOUTH,
    WEST,
    EAST
}

public struct Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Day10
{
    public static void Run(string input, string[] lines)
    {
        var s = GetStart(lines);
        var d = Direction.SOUTH;
        var count = 0;

        
        // first move
        s.Y += 1;
        count += 1;

        do {

            var move = GetMove(lines[s.Y][s.X], d);
            
            s.X += move.Item1.X;
            s.Y += move.Item1.Y;
            d = move.Item2;
            count++;

        } while (lines[s.Y][s.X] != 'S');

        System.Console.WriteLine($"Part 1: {count / 2}");
        
    }

    private static (Position, Direction) GetMove(char mv, Direction dir)
    {
        switch (mv)
        {
            case '|':
                if (dir == Direction.NORTH)
                    return (new (0, -1), dir);
                else if (dir == Direction.SOUTH)
                    return (new (0, 1), dir);
                else
                    throw new InvalidEnumArgumentException();
            case '-':
                if (dir == Direction.WEST)
                    return (new (-1, 0), dir);
                else if (dir == Direction.EAST)
                    return (new (1, 0), dir);
                else
                    throw new InvalidEnumArgumentException();
            case 'L':
                if (dir == Direction.WEST)
                    return (new (0, -1), Direction.NORTH);
                else if (dir == Direction.SOUTH)
                    return (new (1, 0), Direction.EAST);
                else
                    throw new InvalidEnumArgumentException();
            case 'J':
                if (dir == Direction.SOUTH)
                    return (new (-1, 0), Direction.WEST);
                else if (dir == Direction.EAST)
                    return (new (0, -1), Direction.NORTH);
                else
                    throw new InvalidEnumArgumentException();
            case '7':
                if (dir == Direction.EAST)
                    return (new (0, 1), Direction.SOUTH);
                else if (dir == Direction.NORTH)
                    return (new (-1, 0), Direction.WEST);
                else
                    throw new InvalidEnumArgumentException();
            case 'F':
                if (dir == Direction.WEST)
                    return (new (0, 1), Direction.SOUTH);
                else if (dir == Direction.NORTH)
                    return (new (1, 0), Direction.EAST);
                else
                    throw new InvalidEnumArgumentException();
            default:
                throw new InvalidEnumArgumentException();
        }
    }

    private static Position GetStart(string[] lines)
    {
        for (int yy = 0; yy < lines.Length; yy++)
        {
            for (int xx = 0; xx < lines[0].Length; xx++)
            {
                if (lines[yy][xx] == 'S')
                {
                    return new (xx, yy);
                }
            }
        }

        throw new InvalidEnumArgumentException();
    }
}