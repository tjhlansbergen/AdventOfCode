namespace Runner;

public static class Extensions
{
    public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> sequence)
    {
        if (sequence == null)
        {
            yield break;
        }

        var list = sequence.ToList();

        if (!list.Any())
        {
            yield return Enumerable.Empty<T>();
        }
        else
        {
            var startingElementIndex = 0;

            foreach (var startingElement in list)
            {
                var index = startingElementIndex;
                var remainingItems = list.Where((e, i) => i != index);

                foreach (var permutationOfRemainder in remainingItems.Permute())
                {
                    yield return permutationOfRemainder.Prepend(startingElement);
                }

                startingElementIndex++;
            }
        }
    }
}

public class Day15
{


    public class Ingredient
    {
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; } = 0;
        public int Durability { get; set; } = 0;
        public int Flavor { get; set; } = 0;
        public int Texture { get; set; } = 0;
        public int Calories { get; set; } = 0;
        public int Amount { get; set; } = 0;
    }

    private static List<List<int>> combinations = new List<List<int>>();


    public static void Run()
    {

        var lines = File.ReadAllLines("../inputs/day15");
        var ingredients = lines.Select(l => l.Replace(":", ""))
                                .Select(l => l.Replace(",", ""))
                                .Select(l => l.Split(' '))
                                .Select(s => new Ingredient { Name = s[0], Capacity = P(s[2]), Durability = P(s[4]), Flavor = P(s[6]), Texture = P(s[8]), Calories = P(s[10]) })
                                .ToArray();

        var endResult = 0;
        var length = ingredients.Count();
        GenerateCombinations(length, 100, 0, 1, 0, new int[length]);

        foreach (var combination in combinations)
        {
            foreach (var permutation in combination.Permute<int>())
            {
                var amounts = permutation.ToArray();
                for (int i = 0; i < amounts.Length; i++)
                {
                    ingredients[i].Amount = amounts[i];
                }
                var result = BakeCookie(ingredients);

                //System.Console.Write(String.Join(",", amounts));
                //System.Console.WriteLine($"\t{result}");
                if (result > endResult) { endResult = result; }
            }

        }

        System.Console.WriteLine(endResult);

        int BakeCookie(IEnumerable<Ingredient> i)
        {
            if (i.Sum(i => i.Amount) != 100) { throw new ArgumentException("Ingredients do not add up!"); }

            var c = new Ingredient();
            c.Capacity = i.Select(i => i.Capacity * i.Amount).Sum();
            if (c.Capacity < 0) return 0;
            c.Durability = i.Select(i => i.Durability * i.Amount).Sum();
            if (c.Durability < 0) return 0;
            c.Flavor = i.Select(i => i.Flavor * i.Amount).Sum();
            if (c.Flavor < 0) return 0;
            c.Texture = i.Select(i => i.Texture * i.Amount).Sum();
            if (c.Texture < 0) return 0;
            c.Calories = i.Select(i => i.Calories * i.Amount).Sum();
            if (c.Calories != 500) return 0;

            return (c.Capacity * c.Durability * c.Flavor * c.Texture);
        }


        int P(string s)
        {
            return int.Parse(s);
        }

        void GenerateCombinations(int length, int target, int k, int last, int sum, int[] a)
        {

            if (k == length - 1)
            {

                a[k] = target - sum;
                combinations.Add(new List<int>(a));

            }
            else
            {

                for (int i = last; i < target - sum - i + 1; i++)
                {

                    a[k] = i;
                    GenerateCombinations(length, target, k + 1, i, sum + i, a);

                }

            }

        }

        IEnumerable<IEnumerable<int>> PermuteCombinations(IEnumerable<int> sequence)
        {
            if (sequence == null)
            {
                yield break;
            }

            var list = sequence.ToList();

            var startingElementIndex = 0;

            foreach (var startingElement in list)
            {
                var index = startingElementIndex;
                var remainingItems = list.Where((e, i) => i != index);

                foreach (var permutationOfRemainder in PermuteCombinations(remainingItems))
                {
                    yield return permutationOfRemainder.Prepend(startingElement);
                }

                startingElementIndex++;
            }

        }

    }


}