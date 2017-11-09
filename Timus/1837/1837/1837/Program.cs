using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1837
{
    class Program
    {
        static void Main(string[] args)
        {
            var N = int.Parse(Console.ReadLine());
            var numbers = new SortedDictionary<string, int>();
            var names = new Dictionary<string, List<string>>();
            for (var i = 0; i < N; i++)
            {
                var line = Console.ReadLine().Split();
                foreach (var e in line)
                {
                    if (!names.ContainsKey(e)) { names.Add(e, new List<string>()); numbers.Add(e, int.MaxValue); }
                    foreach (var s in line.Where(x => x != e).Select(x =>
                    {
                        names[e].Add(x);
                        return x;
                    })) ;
                }
            }
            if (!numbers.ContainsKey("Isenbaev"))
            { Without(numbers); return; }

            numbers["Isenbaev"] = 0;
            Do(names, numbers, 1, "Isenbaev");
            foreach (var e in numbers)
            {
                if (e.Value == int.MaxValue)
                    Console.WriteLine(e.Key + " undefined");
                else Console.WriteLine(e.Key + " " + e.Value);
            }
        }

        public static void Do(Dictionary<string, List<string>> names,
            SortedDictionary<string, int> numbers, int price, string name)
        {
            foreach (var nam in names[name])
            {
                if (numbers[nam] <= price) continue;
                numbers[nam] = price;
                Do(names, numbers, price + 1, nam);
            }
        }

        public static void Without(SortedDictionary<string, int> numbers)
        {
            foreach (var e in numbers)
            {
                Console.WriteLine(e.Key + " undefined");
            }
        }
    }
}

