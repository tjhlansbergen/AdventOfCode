namespace Runner
{
    public class Day9
    {
        private static int _finalCost = 0;
        private static readonly bool log = true;

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

                if (Nodes.Any(n => n.Name == a))
                {
                    nodea = Nodes.Single(n => n.Name == a);
                }
                else
                {
                    nodea = new Node(a);
                    Nodes.Add(nodea);
                }

                if (Nodes.Any(n => n.Name == b))
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
            var lines = File.ReadAllLines("../inputs/day9");

            var nodes = new NodeList();

            foreach (var line in lines)
            {
                var from = line.Split('=')[0].Split("to")[0].Trim();
                var to = line.Split('=')[0].Split("to")[1].Trim();
                var distance = int.Parse(line.Split('=')[1]);

                nodes.Add(from, to, distance);
            }

            foreach (var node in nodes.Nodes)
            {

                var visited = new List<Node>();
                Visit(node, visited, null, 0);
                if (log) System.Console.WriteLine();

            }

            if (log) System.Console.WriteLine();
            System.Console.WriteLine(_finalCost);
        }

        internal static void Visit(Node node, List<Node> visited, Node prevNode, int cost)
        {
            if (log) System.Console.Write($"{node.Name} - ");

            // mark as visited
            visited.Add(node);



            var nextNode = node.Distances.Where(d => !visited.Contains(d.Key)).OrderByDescending(d => d.Value).FirstOrDefault();

            if(!nextNode.Equals(default(KeyValuePair<Node,int>)))
            {
                    Visit(nextNode.Key, visited, node, cost + nextNode.Value);
            }
    
            if (prevNode == null)
            {
                if (log) System.Console.Write($"{node.Name}! - ");
            }


            if (!node.Distances.Keys.Where(d => !visited.Contains(d)).Any())
            {

                visited.Remove(node);
                visited.Remove(prevNode);

                if (log) System.Console.WriteLine(cost);
                if (cost > _finalCost) _finalCost = cost;
                cost = 0;
            }


        }
    }
}