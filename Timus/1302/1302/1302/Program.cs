using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1302
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var m = a[0];
            var n = a[1];
            if (m > n)
            {
                var z = m;
                m = n;
                n = z;
            }
            var r1 = GetRow(m);
            var r2 = GetRow(n);
            m -= (r1 - 1) * (r1 - 1) + r1; 
            n -= (r2 - 1) * (r2 - 1) + r2; 
            var ans = 0;
            while (r1 < r2)
            {
                if ((r1 - m) % 2 != 0)
                {
                    ++r1;
                    ++ans;
                }
                else
                {
                    if (m > n)
                    {
                        --m;
                        ++ans;
                    }
                    else if (m < n)
                    {
                        ++m;
                        ++ans;
                    }
                    else
                    {
                        while (r1 < r2)
                        {
                            ++r1;
                            ans += 3;

                            if (r1 >= r2) continue;
                            ++r1;
                            ++ans;
                        }
                    }
                }
            }
            if (m < n) ans += n - m;
            else ans += m - n;
            Console.WriteLine(ans);
        }


        public static int GetRow(int n)
        {
            for (var i = 0; i <= 31623; ++i)
                if (n <= i * i) return i;
            return -1;
        }
    }
}
