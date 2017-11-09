using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _1036
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var n = a[0];
            var s = (int)a[1] / 2;
            var res = 0;
            BigInteger z = 10;
            z = BigInteger.Pow(z, n);
            for (var i = BigInteger.Zero; i.CompareTo(z) == -1; i++)

            {
                if (CheckSum(i, s))
                    res++;
            }
        Console.WriteLine(res * res);
        }

        private static bool CheckSum(BigInteger number, int s)
        {
            BigInteger sum = 0;
            while (number != 0)
            {
                sum += number % 10;
                if (sum > s) return false;
                number = number / 10;
            }
            return sum == s;
        }

    }
}
