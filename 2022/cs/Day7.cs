namespace AocRunner;

public class Day7
{
    public abstract class Node
    {
        public Node Parent { get; private set; }

        public string Name { get; private set; }

        protected Node(string name, Node parent)
        {
            Parent = parent;
            Name = name;
        }
    }

    public class File : Node
    {
        public int Size { get; set; }

        public File(string name, Node parent) : base(name, parent)
        {
        }
    }

    public class Folder : Node
    {
        public List<Node> Children { get; set; } = new List<Node>();

        public Folder(string name, Node parent) : base(name, parent)
        {
        }
    }

    public static void Run(string input, string[] lines)
    {
        var filesystem = new List<Node>();
        var workingDir = new Stack<string>();

        foreach (var line in lines)
        {
            if (line.StartsWith("$ cd"))
            {
                if (line == "$ cd ..")
                {
                    workingDir.Pop();
                }
                else
                {
                    workingDir.Push(line.Replace("$ cd ", string.Empty));
                }

                continue;
            }

            if (line == "$ ls") continue;

            // if we got here we have a file or directory listing
            if (line.StartsWith("dir"))
            {
                filesystem.Add(new Folder(line.Replace("dir ", string.Empty), filesystem.Single(n => n.Name == workingDir.Peek())));
            }
            
        }

    }
}