using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var nodes = CreateGraph();
            CheckForBipartite(nodes);
        }

        private static void CheckForBipartite(List<Node> nodes)
        {
            DFS(nodes[0], 1);
            if (nodes.Any(e => !CheckColor(e)))
            {
                File.WriteAllText("out.txt", "N");
                return;
            }
            var first = new List<Node>();
            var second = new List<Node>();
            var firstColor = nodes[0].Color;
            foreach (var e in nodes)
            {
                if (e.Color == 1) first.Add(e);
                else second.Add(e);
            }
            var str1 = string.Concat(first);
            var str2 = string.Concat(second);

            File.WriteAllText("out.txt", "Y" + Environment.NewLine + str1 + "0" + Environment.NewLine + str2);
        }
        private static List<Node> CreateGraph()
        {
            var lines = File.ReadAllLines("in.txt");
            var nodes = new List<Node>();
            var N = int.Parse(lines[0]);
            for (var i = 0; i < N; i++)
                nodes.Add(new Node(i));
            for (var i = 0; i < N; i++)
            {
                var curNodes = lines[i + 1].Split().Select(int.Parse).ToList();
                for (var j = 0; j < N; j++)
                {
                    if (curNodes[j] == 1) nodes[i].Nodes.Add(nodes[j]);
                }
            }
            return nodes;
        }

        public static bool CheckColor(Node node)
        {
            var curColor = node.Color;
            return node.Nodes.All(e => e.Color != node.Color);
        }

        public static void DFS(Node node, int color)
        {
            if (node.Color == 0)
            {
                node.Color = color;
                color = color == 1 ? 2 : 1;
                foreach (var e in node.Nodes)
                {
                    DFS(e, color);
                }
            }
            else if (node.Color == color)
            {
                return;
            }

        }
    }

}




