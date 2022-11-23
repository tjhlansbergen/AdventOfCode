namespace Runner;

public class Day13
{
    private static int max = 0;

    public static void Run()
    {
        var lines = File.ReadAllLines("../inputs/day13").Select(l => l.Replace(".", string.Empty));
        var persons = new Persons();

        foreach (var line in lines)
        {
            var splits = line.Split(' ');
            persons.AddOrUpdate(splits[0], splits[10], (splits[2] == "lose") ? 0 - int.Parse(splits[3]) : int.Parse(splits[3]));
        }

        // we van pick any person to start, no need to do them all since its a circle
        var visited = new List<Person>();
        Visit(persons.All.First(), visited, 0, persons.All.First());
        System.Console.WriteLine($"Part 1: {max}");

        // add me
        var existingPersons = new List<Person>(persons.All);
        foreach (var p in existingPersons)
        {
            persons.AddOrUpdate("me", p.Name, 0);
        }
        var me = persons.All.Single(p => p.Name == "me");
        foreach (var p in persons.All)
        {
            p.Scores.Add(me, 0);
        }

        // and go again
        max = 0;
        visited = new List<Person>();
        Visit(persons.All.First(), visited, 0, persons.All.First());

        System.Console.WriteLine($"Part 2: {max}");
    }

    public static void Visit(Person p, List<Person> visited, int count, Person first)
    {
        visited.Add(p);

        var toVisit = p.Scores.Where(s => !visited.Contains(s.Key));

        if (toVisit.Any())
        {
            foreach (var next in toVisit)
            {
                var combinedScore = p.Scores[next.Key] + next.Key.Scores[p];
                Visit(next.Key, visited, count + combinedScore, first);
            }
        }
        else
        {
            var final = count + p.Scores[first] + first.Scores[p];
            if (final > max) { max = final; }
        }

        visited.Remove(p);
    }

    public class Persons
    {
        public List<Person> All { get; private set; } = new List<Person>();

        public void AddOrUpdate(string name, string neighbour, int score)
        {
            if (!All.Any(p => p.Name == neighbour))
            {
                All.Add(new Person { Name = neighbour });
            }

            var n = All.Single(p => p.Name == neighbour);

            if (All.Any(p => p.Name == name))
            {
                All.First(p => p.Name == name).Scores[n] = score;
            }
            else
            {
                All.Add(new Person { Name = name, Scores = new Dictionary<Person, int>() { { n, score } } });
            }
        }
    }

    public class Person
    {
        public string? Name { get; set; }
        public Dictionary<Person, int> Scores { get; set; } = new Dictionary<Person, int>();
    }
}