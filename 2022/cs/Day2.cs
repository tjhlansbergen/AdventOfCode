namespace AocRunner;

public class Day2
{
    public static void Run(string input, string[] lines)
    {
        // 1 for Rock, 2 for Paper, and 3 for Scissors
        // X for Rock, Y for Paper, and Z for Scissors
        // A for Rock, B for Paper, and C for Scissors
        
        var scores = new Dictionary<char, int> {
            { 'A', 1},
            { 'B', 2},
            { 'C', 3},
            { 'X', 1},
            { 'Y', 2},
            { 'Z', 3},
        };

        int Part1(char opp, char me)
        {
            //// 0 if you lost, 3 if the round was a draw, and 6 if you won

            var score = scores[me];
            if (scores[me] == scores[opp]) score += 3; // draw
            if (scores[me] == 1 && scores[opp] == 3) score += 6;    // rock over scissors
            if (scores[me] == 2 && scores[opp] == 1) score += 6;    // paper over rock
            if (scores[me] == 3 && scores[opp] == 2) score += 6;    // scissors over paper
            return score;
        }

        int Part2(char opp, char me)
        {
            // X means you need to lose, Y means you need to end the round in a draw, and Z means you need to win

            switch (me)
            {
                case 'X':
                    return (scores[opp] - 1) == 0 ? 3 : scores[opp] - 1; // loose
                case 'Y':
                    return scores[opp] + 3; // draw
                case 'Z':
                    return ((scores[opp] + 1) == 4 ? 1 : scores[opp] + 1) + 6;   // win
            }

            throw new InvalidOperationException(me.ToString());
        }

        System.Console.WriteLine($"Part 1: {lines.Select(l => Part1(l[0], l[2])).Sum()}");
        System.Console.WriteLine($"Part 2: {lines.Select(l => Part2(l[0], l[2])).Sum()}");
    }
}