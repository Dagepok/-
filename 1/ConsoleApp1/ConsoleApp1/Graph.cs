using System.Collections.Generic;
using System.Globalization;

namespace ConsoleApp1
{
    public class Node
    {
        public int Number { get; }
        public List<Node> Nodes { get; set; }
        public int Color { get; set; }

        public Node(int number)
        {
            Number = number;
            Nodes = new List<Node>();
            Color = 0;
        }

        public override string ToString()
        {
            return (Number+1).ToString();
        }
    }
}