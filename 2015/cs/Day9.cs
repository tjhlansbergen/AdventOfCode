namespace Runner
{
    public class Day9
    {
        public class Node
        {
            public string Name { get; set; }
            public Dictionary<Node, int> Distances { get; set; } = new Dictionary<Node, int>();

            public Node(string name)
            {
                Name = name;
            }
        }

        public class NodeList
        {
            public List<Node> Nodes { get; set; } = new List<Node>();

            public void Add(string a, string b, int distance)
            {
                Node nodea = null, nodeb = null;

                if(Nodes.Any(n => n.Name == a))
                {
                    nodea = Nodes.Single(n => n.Name == a);
                }
                else
                {
                    nodea = new Node(a);
                    Nodes.Add(nodea);
                }

                if(Nodes.Any(n => n.Name == b))
                {
                    nodeb = Nodes.Single(n => n.Name == b);
                }
                else
                {
                    nodeb = new Node(b);
                    Nodes.Add(nodeb);
                }

                nodea.Distances.Add(nodeb, distance);
                nodeb.Distances.Add(nodea, distance);
            }
        }

        internal static void Run()
        {
            var lines = File.ReadAllLines("../inputs/day9test");

            var nodes = new NodeList();

            foreach (var line in lines)
            {
                var from = line.Split('=')[0].Split("to")[0].Trim();
                var to =  line.Split('=')[0].Split("to")[1].Trim();
                var distance = int.Parse(line.Split('=')[1]);

                nodes.Add(from, to, distance);
            }

            var test = nodes;
        }
    }
}