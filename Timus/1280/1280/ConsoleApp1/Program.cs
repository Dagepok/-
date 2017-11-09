using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var N = a[0];
            var M = a[1];
            var edges = new Dictionary<int, List<int>>();
            for (var i = 1; i <= N; i++)
            {
                edges.Add(i, new List<int>());
            }
            for (var i = 1; i <= M; i++)
            {
                a = Console.ReadLine().Split().Select(int.Parse).ToArray();
                edges[a[0]].Add(a[1]);
            }
            var line = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var no = false;
            for (var i = N; i > 0; i--)
            {
                if (no) break;
                for (var j = 0; j < i - 1; j++)
                {
                    if (edges[i].Contains(line[j]))
                    {
                        Console.WriteLine("NO");
                        no = true;
                        break;
                    }
                    //Environment.Exit(0);
                }

            }
            if (!no)
                Console.WriteLine("YES");
        }
    }
}
