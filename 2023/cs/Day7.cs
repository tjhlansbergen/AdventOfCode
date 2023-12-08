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
        var test = ScoreHand(new Hand { Cards = "23284"});

        var hands = lines.Select(ParseLine);

        var ordered = hands.OrderBy(h => h);

        var part1 = ordered.Select((hand, i) => (i+1) * hand.Bid).Sum();;

        System.Console.WriteLine($"Part 1: {part1}");

    }

    private static Hand ParseLine(string line)
    {
        return new Hand { Cards = line.Split(' ')[0], Bid = int.Parse(line.Split(' ')[1]) };
    }

    private static int ScoreHand(Hand hand)
    {
        
        // 6 = Five of a kind
        if (hand.Cards.Distinct().Count() == 1) 
        {
            return 6;
        }

        var groups = hand.Cards.GroupBy(c => c);
        
        // 5 = Four of a kind
        if (groups.Any(gr => gr.Count() == 4)) 
        {
            return 5;
        }
        
        // 4 = Full House
        if (groups.Any(gr => gr.Count() == 3) && groups.Any(gr => gr.Count() == 2)) 
        {
            return 4;
        }

        // 3 = Three of a kind
        if (groups.Any(gr => gr.Count() == 3)) 
        {
            return 3;
        }

        // 2 = Two pair
        if (groups.Count() == 3) 
        {
            return 2;
        }

        // 1 = one pair
        if (hand.Cards.Distinct().Count() == 4) 
        {
            return 1;
        }

        // 0 = High Card
        return 0;
    }

}