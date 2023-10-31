using System;

namespace AocRunner;

public class Day7
{
    public class Worker
    {
        public int Id { get; set; }
        public char Work { get; set; } = '.';
        public int Working { get; set; } = 0;

        public bool Idle() => Work == '.';
        public bool Done() => Working == (int)Work - 4;

        public void Clear()
        {
            Work = '.';
            Working = 0;
        }
    }

    public static void Run(string input, string[] lines)
    {
        var steps = GetSteps(lines);
        PartOne(steps);

        steps = GetSteps(lines);
        PartTwo(steps, 5);

    }


    private static void PartTwo(Dictionary<char, List<char>> steps, int workerCount)
    {
        var workers = Enumerable.Range(1, workerCount).Select(w => new Worker { Id = w }).ToList();
        var result = new List<char>();
        var second = -1;

        while (result.Count != steps.Count)
        {
            //System.Console.WriteLine($"{second}\t{workers[0].Work}\t{workers[1].Work}\t{string.Join(' ', result)}");

            // find workers that are done
            var done = workers.OrderBy(w => w.Id).Where(w => w.Done()).ToList();
            // add to results
            result.AddRange(done.Select(w => w.Work));
            // for each step that is done,
            // mark steps
            // and clear worker
            foreach(var item in done)
            {
                foreach (var key in steps.Keys)
                {
                    steps[key].Remove(item.Work);
                }

                item.Clear();
            }
            

            // find the next steps
            var processing = workers.Where(w => !w.Idle()).Select(w => w.Work);
            var nexts = steps.Where(s => !processing.Contains(s.Key) && !result.Contains(s.Key) && !s.Value.Any())
            .Select(s => s.Key)
            .Order();
            // for each next step find a worker
            foreach (var item in nexts)
            {
                var worker = workers
                    .OrderBy(w => w.Id)
                    .FirstOrDefault(w => w.Idle());

                if (worker != null) worker.Work = item;
            }

            foreach (var worker in workers.Where(w => !w.Idle()))
            {
                worker.Working++;
            }
            second++;
        }

        System.Console.WriteLine($"Part 2: {second}");
    }


    private static void PartOne(Dictionary<char, List<char>> steps)
    {
        var result = new List<char>();

        System.Console.Write("Part 1: ");

        while (result.Count != steps.Count)
        {
            var next = steps.Where(s => !result.Contains(s.Key) && !s.Value.Any())
            .Select(s => s.Key)
            .Min();

            result.Add(next);
            foreach (var key in steps.Keys)
            {
                steps[key].Remove(next);
            }

            System.Console.Write(next);

        }

        System.Console.WriteLine();
    }

    private static Dictionary<char, List<char>> GetSteps(string[] lines)
    {
        var steps = new Dictionary<char, List<char>>();

        foreach (var line in lines)
        {
            var required = line[5];
            var step = line[36];

            if (steps.ContainsKey(step))
                steps[step].Add(required);
            else
                steps.Add(step, new List<char> { required });

            if (!steps.ContainsKey(required))
                steps.Add(required, new List<char>());
        }

        return steps;
    }
}