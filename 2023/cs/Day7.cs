namespace AocRunner;

public class Day7
{
    private static readonly Dictionary<char, int> cardValues = new()
    {
        {'A', 14},
        {'K', 13},
        {'Q', 12},
        {'J', 1},
        {'T', 10},
        {'9', 9},
        {'8', 8},
        {'7', 7},
        {'6', 6},
        {'5', 5},
        {'4', 4},
        {'3', 3},
        {'2', 2},
    };

    public class Hand : IComparable<Hand>
    {
        public int Bid { get; set; }
        public string Cards { get; set; } = string.Empty;

        public int CompareTo(Hand? other)
        {
            if (other == null) throw new NullReferenceException("Other hand was null when trying to compare");

            var score = ScoreHand(this);
            var otherScore = ScoreHand(other);

            if (score != otherScore)
            {
                return (score < otherScore) ? -1 : 1;
            }
            else
            {
                for (int i = 0;  i < this.Cards.Length; i++)
                {
                    if ( cardValues[this.Cards[i]] < cardValues[other.Cards[i]] ) return -1;
                    if ( cardValues[this.Cards[i]] > cardValues[other.Cards[i]] ) return 1;
                }
            }

            return 0;
        }
    }

    public static void Run(string input, string[] lines)
    {
        var hands = lines.Select(ParseLine);

        var ordered = hands.OrderBy(h => h);

        foreach (var hand in ordered)
        {
            if (hand.Cards.Any(c => c == 'J'))
            {
                System.Console.WriteLine($"{hand.Cards}\t{ScoreHand(hand)}");
            }
        }

        var part2 = ordered.Select((hand, i) => (i+1) * hand.Bid).Sum();

        System.Console.WriteLine();
        System.Console.WriteLine($"Part 2: {part2}");

    }

    private static Hand ParseLine(string line)
    {
        return new Hand { Cards = line.Split(' ')[0], Bid = int.Parse(line.Split(' ')[1]) };
    }

    private static int ScoreHand(Hand hand)
    {
        var jCount = hand.Cards.Count(c => c == 'J');
        var groups = hand.Cards.GroupBy(c => c);
        
        // 6 = Five of a kind
        if (jCount > 0)
        {
            if (groups.Where(gr => gr.Key != 'J').Any(gr => gr.Count() == 5 - jCount)) return 6;
        }
        if (groups.Any(gr => gr.Count() == 5)) return 6;
        
        // 5 = Four of a kind
        if (jCount > 0)
        {
            if (groups.Where(gr => gr.Key != 'J').Any(gr => gr.Count() == 4 - jCount)) return 5;
        }
        if (groups.Any(gr => gr.Count() == 4)) return 5;

        // 4 = Full House
        if (jCount == 3)
        {
            return 4;
        }

        if (jCount == 2)
        {
            if (groups.Where(gr => gr.Key != 'J').Any(gr => gr.Count() >= 2)) return 4;
        }

        if (jCount == 1)
        {
            if (groups.Count() == 3) return 4;
            if (groups.Any(gr => gr.Count() == 3)) return 4;
        }

        if (groups.Any(gr => gr.Count() == 3) && groups.Any(gr => gr.Count() == 2)) 
        {
            return 4;
        }

        // 3 = Three of a kind
        if (groups.Any(gr => gr.Count() >= 3 - jCount)) 
        {
            return 3;
        }

        // 2 = Two pair
        if (groups.Count() == 3) 
        {
            return 2;
        }

        // 1 = one pair

        if (jCount == 1)
        {
            return 1;
        }

        if (groups.Any(gr => gr.Count() == 2)) 
        {
            return 1;
        }

        // 0 = High Card
        return 0;
    }

}