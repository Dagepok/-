using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _1036
{
    internal class Program
    {
        private static readonly SortedDictionary<int, SortedDictionary<int, int>> Dict = new SortedDictionary<int, SortedDictionary<int, int>>();
        private static void Main(string[] args)
        {
            //var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
            //var n = a[0];
            //var s = a[1] / 2;// a[1] % 2 == 0 ? a[1] / 2 : -1;
            Dict.Add(0, new SortedDictionary<int, int>());
            var n = 1000;
            for (var i = 0; i < n; i++)
                Dict[0].Add(i, 1);
            for (; n > 0; n--)
                for (var s = 1000; s > 0; s--)
                {
                    var count = Math.Pow(D(n, s), 2);
                }
            //var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
            //var b = a[0];
            //var c = a[1] / 2;
            //if (a[1] % 2 == 0)
            //{
            //    Console.WriteLine(Math.Pow(Dict[500][50], 2));

            //}
            //else Console.WriteLine(0);
            var str = new StringBuilder();
            str.Append("        ");
            for (var i = 0; i < n; i++)
                str.Append(i + "          ");
            str.Append('\n');
            str.Append('\n');
            foreach (var k in Dict)
            {
                str.Append(k.Key + "           ");
                foreach (var j in k.Value)
                {
                    if (j.Value != 0)
                        str.Append(k.Key + "," + j.Key + ":" + j.Value + "          ");
                    else str.Append(k.Key + "," + j.Key + ":" + "           ");
                }
                str.Append('\n');
            }
            File.WriteAllText("a.txt", str.ToString());
            // Console.WriteLine(count);
        }

        private static int D(int n, int k)
        {
            if (Dict.ContainsKey(k) && Dict[k].ContainsKey(n))
                return Dict[k][n];
            if (k == 0) return Dict[k][n];
            if (k < 0 || n <= 0) return 0;
            var sum = 0;
            for (var j = 0; j <= 9; j++)
            {
                sum += D(n - 1, k - j);
            }
            dictAdd(n, k, sum);
            return sum;
        }

        private static void dictAdd(int n, int k, int value)
        {
            if (Dict.ContainsKey(k))
            {
                if (!Dict[k].ContainsKey(n))
                    Dict[k].Add(n, value);
                else return;
            }
            else Dict.Add(k, new SortedDictionary<int, int> { { n, value } });
        }
    }
}

