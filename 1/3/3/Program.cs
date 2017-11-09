using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace _3
{
    internal class Node
    {
        public int Number { get; set; }
        public Dictionary<Node, int> Prices { get; }
        public int CurrentPrice { get; set; }
        public bool Used { get; set; }
        public Node FromNode { get; set; }

        public Node(int number)
        {
            Number = number;
            Prices = new Dictionary<Node, int>();
            CurrentPrice = int.MaxValue;
            FromNode = null;
        }

        public override bool Equals(object obj)
        {
            return Number.Equals(((Node)obj).Number);
        }

        public override string ToString()
        {
            return Number.ToString();
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var lines = File.ReadAllLines("in.txt");
            var N = int.Parse(lines[0]);
            var nodes = new List<Node>();
            for (var i = 1; i <= N; i++)
            {
                nodes.Add(new Node(i));
            }
            for (var i = 1; i <= N; i++)
            {
                if (lines[i][0] == 0) continue;
                var line = lines[i].Split().Select(int.Parse).ToArray();

                for (var j = 0; j < line.Length - 1; j += 2)
                    nodes[line[j] - 1].Prices.Add(nodes[i - 1], line[j + 1]);
            }
            var start = nodes[int.Parse(lines[lines.Length - 2]) - 1];
            start.CurrentPrice = 0;
            var end = nodes[int.Parse(lines[lines.Length - 1]) - 1];

            Djikstra(start, nodes);
            if (end.Used == false) File.WriteAllText("out.txt", "N");
            else
            {
                //var sum = 0;
                var cur = end;
                var line = new List<Node>();
                while (!cur.Equals(start))
                {
                    line.Add(cur);
                    // sum += cur.CurrentPrice;
                    cur = cur.FromNode;
                }
                line.Add(start);
                line.Reverse();
                var str = "";
                foreach (var e in line)
                {
                    str += e + " ";
                }
                File.WriteAllText("out.txt", "Y" + Environment.NewLine + str + Environment.NewLine + nodes[end.Number - 1].CurrentPrice);
            }

        }

        public static void Djikstra(Node start, List<Node> nodes)
        {
            var queue = new Queue<Node>();
            queue.Enqueue(start);
            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                foreach (var e in current.Prices.Keys)
                {

                    if (e.CurrentPrice <= current.Prices[e] + current.CurrentPrice) continue;
                    e.CurrentPrice = current.Prices[e] + current.CurrentPrice;
                    e.FromNode = current;
                }
                current.Used = true;
                foreach (var e in current.Prices.Keys)
                {
                    if (e.Used == false)
                        queue.Enqueue(e);
                }
            }
        }
    }
}
