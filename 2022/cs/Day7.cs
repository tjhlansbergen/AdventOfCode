namespace AocRunner;

public class Day7
{
    public abstract class Node
    {
        public Folder? Parent { get; private set; }

        public string Name { get; private set; }

        protected Node(string name, Folder? parent)
        {
            Parent = parent;
            Name = name;
        }

        public abstract int Size();
    }

    public class File : Node
    {
        private int _size;

        public File(string name, Folder parent, int size) : base(name, parent)
        {
            _size = size;
        }

        public override int Size() => _size;
    }

    public class Folder : Node
    {
        public List<Node> Children { get; set; } = new List<Node>();

        public Folder(string name, Folder? parent) : base(name, parent)
        {
        }

        public override int Size()
        {
            return Children.Select(c => c.Size()).Sum();
        }
    }

    public static void Run(string input, string[] lines)
    {
        Folder? currentDir = new Folder("/", null);
        var filesystem = new List<Node>{ currentDir };
        
        foreach (var line in lines)
        {
            if (currentDir == null) throw new NullReferenceException("Current dir was null");

            if (line == "$ ls") continue;
            if (line == "$ cd /") continue;

            if (line.StartsWith("$ cd"))
            {
                if (line == "$ cd ..")
                {
                    currentDir = currentDir.Parent;
                }
                else
                {
                    currentDir = currentDir.Children.Single(c => c is Folder && c.Name == line.Replace("$ cd ", string.Empty)) as Folder;
                }

                continue;
            }

            // if we got here we have a file or directory listing
            if (line.StartsWith("dir"))
            {
                var folder = new Folder(line.Replace("dir ", string.Empty), currentDir);
                currentDir.Children.Add(folder);
                filesystem.Add(folder);
            }
            else
            {
                var parts = line.Split(' ');
                var file = new File(parts[1], currentDir, int.Parse(parts[0]));
                currentDir.Children.Add(file);
                filesystem.Add(file);
            }
        }

        var part1 = filesystem.Where(n => n is Folder && n.Size() <= 100000).Select(n => n.Size()).Sum();
        System.Console.WriteLine($"Part 1: {part1}");

        var spaceNeeded = 30000000 - (70000000 - filesystem.Single(n => n is Folder && n.Name == "/").Size());
        var part2 = filesystem.Where(n => n is Folder && n.Size() >= spaceNeeded)
                              .OrderBy(n => n.Size())
                              .First()
                              .Size();

        System.Console.WriteLine($"Part 2: {part2}");
    }
}