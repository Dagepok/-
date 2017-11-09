using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace _2task
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var E = GetGraph(out var X, out var Y);
            var S = new Node(-1, Part.S);
            foreach (var x in X)
            {
                new Edge(S, x);
            }
            var T = new Node(-2, Part.T);
            BFS(S);
            DFS(Y);
            if (X.Any(x => x.Pair == null))
                File.WriteAllText("out.txt", $"N\r\n{X.First(x => x.Pair == null)}");
            else File.WriteAllText("out.txt", "Y\r\n" + string.Join(" ", X.Select(x => x.Pair.ToString())));
        }

        private static void BFS(Node S)
        {
            var queue = new Queue<Node>();

            queue.Enqueue(S);
            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                if (current.Part == Part.X)
                    foreach (var edge in current.Edges)
                    {
                        if (current.Equals(edge.NodeY) || edge.NodeY.Part != Part.Y || edge.NodeY.Pair != null)
                            continue;
                        if (current.Pair != null) continue;
                        queue.Enqueue(edge.NodeY);
                        current.Pair = edge.NodeY;
                        edge.NodeY.Pair = current;
                        edge.IsUsed = true;
                    }
                if (current.Part != Part.Y && current.Part != Part.S) continue;
                foreach (var edge in current.Edges)
                {
                    if (current.Equals(edge.NodeY) || edge.NodeY.Part == Part.X && edge.NodeX.Part == Part.Y ||
                        edge.NodeY.Pair != null) continue;
                    queue.Enqueue(edge.NodeY);
                }
            }
        }

        private static void DFS(List<Node> Y)
        {
            foreach (var node in Y.Where(y => y.Pair == null))
            {
                var path = new List<Node>();
                var stack = new Stack<Node>();
                stack.Push(node);
                var current = stack.Peek();
                while (stack.Count != 0)
                {
                    current = stack.Pop();

                    if (path.Count > 0 && path.Last().Number % 2 == current.Number % 2)
                    {
                        break;
                    }
                    path.Add(current);
                    if (current.Part == Part.X && current.Pair == null)
                    {
                        ReverseEdges(path);
                        break;
                    }

                    foreach (var edge in current.Edges)
                    {

                        if (current.Part == Part.Y)
                        {
                            if (edge.IsUsed) continue;

                            var nod = edge.NodeY.Part == Part.X ? edge.NodeY : edge.NodeX;
                            stack.Push(nod);
                        }

                        if (current.Part == Part.X)
                        {
                            if (!edge.IsUsed) continue;
                            var nod = edge.NodeY.Part == Part.X ? edge.NodeX : edge.NodeY;
                            stack.Push(nod);
                        }



                    }
                }

            }
        }

        private static void ReverseEdges(List<Node> path)
        {
            for (var i = 0; i < path.Count - 1; i++)
            {
                var node = path[i];
                var nextNode = path[i + 1];
                var edge = node.Edges.First(x => Equals(x.NodeY, nextNode) || Equals(x.NodeX, nextNode));
                if (Equals(node.Pair, nextNode))
                    edge.IsUsed = false;
                else
                {
                    node.Pair = nextNode;
                    nextNode.Pair = node;
                    edge.IsUsed = true;
                    i++;
                }
            }
        }


        private static List<Edge> GetGraph(out List<Node> X, out List<Node> Y)
        {
            X = new List<Node>();
            Y = new List<Node>();
            var E = new List<Edge>();
            var lines = File.ReadAllLines("in.txt");
            var k = int.Parse(lines[0].Split()[0]);
            var l = int.Parse(lines[0].Split()[1]);
            var xmultiple = -1;
            for (var x = 1; x <= k; x++)
            {
                xmultiple += 2;
                var xNode = new Node(xmultiple, Part.X);
                X.Add(xNode);
                var yMultiple = 0;
                var line = lines[x].Split().Select(int.Parse).ToArray();
                for (var y = 2; y < l + 2; y++)
                {
                    yMultiple += 2;
                    var yNode = Y.FirstOrDefault(node => node.Number == yMultiple);
                    if (yNode == null)
                    {
                        yNode = new Node(yMultiple, Part.Y);
                        Y.Add(yNode);
                    }
                    if (line[y - 2] == 0) continue;
                    E.Add(new Edge(xNode, yNode));
                }
            }
            return E;
        }
    }
}
