using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace _1982
{
    internal class Program
    {
        public class City
        {
            public int Number { get; set; }
            public bool IsElectrified { get; set; }

            public City(int number)
            {
                Number = number;
                IsElectrified = false;
            }
        }
        static void Main(string[] args)
        {
            var a = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var N = a[0];
            var k = a[1];

            var cities = new List<int>();
            for (var i = 0; i < N; i++)
                cities.Add(i);
            var totalCost = 0;
            var withElectrics = Console.ReadLine()
                .Split()
                .Select(x => int.Parse(x) - 1)
                .ToList();
            var prices = new int[N, N];
            for (var i = 0; i < N; i++)
            {
                var line = Console.ReadLine().Split()
                    .Select(int.Parse)
                    .ToArray();
                for (var j = 0; j < N; j++)
                    prices[i, j] = line[j];
            }
            
            
            while (withElectrics.Count < N)
            {
                var min = int.MaxValue;
                var number = -1;
                foreach (var j in withElectrics)
                {
                    for (var i = 0; i < N; i++)
                    {
                        if (withElectrics.Contains(i))
                            continue;
                        
                        if (min <= prices[j, i]) continue;
                        min = prices[j, i];
                        number = i;
                    }

                }
                if (number == -1) continue;
                totalCost += min;
                withElectrics.Add(number);
            }
            Console.WriteLine(totalCost);
        }
    }
}
