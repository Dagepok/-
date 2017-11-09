using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1136
{
    public class Parlamus
    {
        public Parlamus Parent;
        public Parlamus Left;
        public Parlamus Right;
        public int Value;

        public Parlamus(Parlamus parent, int value)
        {
            Parent = parent;
            Value = value;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var N = int.Parse(Console.ReadLine());
            var data = new int[N];
            for (var i = 0; i < N; i++)
                data[i] = int.Parse(Console.ReadLine());
            data = data.Reverse().ToArray();
            var head = new Parlamus(null, data[0]);
            foreach (var e in data)
            {
                if (e == head.Value) continue;
                CheckHead(e, head);
            }
            var result = new List<int>();
            Sort(head, result);
            foreach (var e in result)
                Console.WriteLine(e);

        }

        private static void Sort(Parlamus head , List<int> result )
        {
            if (head.Right != null)
                Sort(head.Right, result);
            if (head.Left != null)
                Sort(head.Left, result);
            result.Add(head.Value);
        }
        private static void CheckHead(int number, Parlamus head)
        {
            if (number > head.Value)
            {
                if (head.Right != null)
                    CheckHead(number, head.Right);
                else head.Right = new Parlamus(head, number);
            }
            if (number < head.Value)
            {
                if (head.Left != null)
                    CheckHead(number, head.Left);
                else head.Left = new Parlamus(head, number);
            }
        }
    }
}
