using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1

{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var lines = File.ReadAllLines("in.txt");
            var N = int.Parse(lines[0]);
            var M = int.Parse(lines[1]);
            var map = new Point[N, M];
            for (var y = 0; y < N; y++)
            {
                var str = lines[y + 2].Split();
                for (var x = 0; x < M; x++)
                    map[y, x] = new Point(x, y, int.Parse(str[x]));
            }
            var strX = lines[lines.Length - 2].Split();
            var strY = lines[lines.Length - 1].Split();
            BFS(map, int.Parse(strX[1]) - 1, int.Parse(strX[0]) - 1, int.Parse(strY[1]) - 1, int.Parse(strY[0]) - 1);
        }

        private static void BFS(Point[,] map, int startX, int startY, int endX, int endY)
        {
            var queue = new Queue<Point>();
            queue.Enqueue(map[startY, startX]);
            var path1 = new Dictionary<Point, Point> { { map[startY, startX], null } };
            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                if (point.X == endX && point.Y == endY)
                    PathFound(path1, point);
                if (point.WasUsed || point.State == 1) continue;
                point.WasUsed = true;
                
                if (point.Y + 1 < map.GetLength(0) - 1) newPoint(map[point.Y + 1, point.X], point, queue, path1);
                if (point.Y - 1 >= 0) newPoint(map[point.Y - 1, point.X], point, queue, path1);
                if (point.X - 1 >= 0) newPoint(map[point.Y, point.X - 1], point, queue, path1);
                if (point.X + 1 < map.GetLength(1) - 1) newPoint(map[point.Y, point.X + 1], point, queue, path1);

            }
            File.WriteAllText("out.txt", "N");
        }

        private static void PathFound(Dictionary<Point, Point> path1, Point end)
        {

            var path = new List<Point>();
            var curPoint = end;
            while (curPoint != null)
            {
                path.Add(curPoint);
                curPoint = path1[curPoint];
            }
            path.Reverse();
            var str = path.Aggregate("", (current, e) => current + (e.ToString() + Environment.NewLine));
            File.WriteAllText("out.txt", "Y" + Environment.NewLine + str);
            Environment.Exit(0);
        }

        private static void newPoint(Point newPoint, Point point, Queue<Point> queue, Dictionary<Point, Point> path1)
        {

            if (path1.ContainsKey(newPoint)) return;
            queue.Enqueue(newPoint);
            path1.Add(newPoint, point);
        }
    }
}
