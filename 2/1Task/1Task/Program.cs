﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _1Task
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var usedNodes = new List<int>();
            var usedEdges = new List<(int, int)>();
            var matrix = GetEdgesMatrix(out var n);
            var isUsedMatrix = new bool[n, n];
            for (var i = 0; i < n; i++)
            {
                var min = 32767;
                var minEdge = (-1, -1);
                for (var j = 0; j < n; j++)
                {
                    if (matrix[i, j] >= min) continue;

                    min = matrix[i, j];
                    minEdge = (i, j);
                }
                if (minEdge.Item1 == -1) continue;
                usedEdges.Add(minEdge);
                usedNodes.Add(minEdge.Item1);
                usedNodes.Add(minEdge.Item2);
                isUsedMatrix[minEdge.Item1, minEdge.Item2] = true;
                isUsedMatrix[minEdge.Item2, minEdge.Item1] = true;
                break;
            }

            while (usedNodes.Count != n)
            {
                var min = 32767;
                var minEdge = (-1, -1);
                foreach (var node in usedNodes)
                    for (var i = 0; i < n; i++)
                    {
                        if (matrix[node, i] >= min || isUsedMatrix[node, i]) continue;
                        if (usedNodes.Contains(node) && usedNodes.Contains(i)) continue;
                            min = matrix[node, i];
                        minEdge = (node, i);
                    }
                if (minEdge.Item1 == -1) continue;
                isUsedMatrix[minEdge.Item1, minEdge.Item2] = true;
                isUsedMatrix[minEdge.Item2, minEdge.Item1] = true;
                usedEdges.Add(minEdge);
                usedNodes.Add(minEdge.Item2);

            }

            var result = new List<int>[n];
            for (var i = 0; i < n; i++)
            {
                result[i] = new List<int>();
                result[i].AddRange(usedEdges.Where(x => x.Item1 == i).Select(x => x.Item2));
                result[i].AddRange(usedEdges.Where(x => x.Item2 == i && !result[i].Contains(x.Item1)).Select(x => x.Item1));
            }
            var price = 0;
            for (var i = 0; i < n; i++)
                price += result[i].Sum(e => matrix[i, e]);
            var lines = (from line in result select string.Join(" ", line.Select(x => ++x).OrderBy(x => x)) into str select str.Length == 0 ? "0" : str + " 0" into str select str).ToList();
            lines.Add((price / 2).ToString());
            File.WriteAllLines("out.txt", lines);
        }
        private static int[,] GetEdgesMatrix(out int n)
        {
            var lines = File.ReadAllLines("in.txt");
            var intLines = lines.Select(x => x.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray())
                .ToArray();
            n = intLines[0][0];
            var edges = new int[n, n];
            for (var i = 1; i < n + 1; i++)
                for (var j = 0; j < n; j++)
                    edges[i - 1, j] = intLines[i][j];
            return edges;
        }
    }
}

