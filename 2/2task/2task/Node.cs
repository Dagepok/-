using System.Collections.Generic;

namespace _2task
{
    public enum Part
    {
        X, Y, S, T
    }

    public class Node
    {
        public Part Part { get; }
        public Node(int number, Part part, params Edge[] edges)
        {
            Number = number;
            Part = part;
            Edges = new List<Edge>();
            Edges.AddRange(edges);


        }
        public Node Pair { get; set; }
        public List<Edge> Edges { get; }
        public int Number { get; }
        public override string ToString()
        {
            return Number.ToString();
            //   return ((Number + 1) / 2).ToString();
        }

        public override int GetHashCode()
        {
            return Number;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Node))
                return false;
            return ((Node)obj).Number == Number && Part == ((Node)obj).Part;
        }
    }

    public class Edge
    {
        public Edge(Node nodeX, Node nodeY, bool isFict = false)
        {
            NodeY = nodeY;
            NodeX = nodeX;
            IsFict = isFict;
            NodeX.Edges.Add(this);
            NodeY.Edges.Add(this);
        }
        public bool IsUsed { get; set; }
        public bool IsFict { get; }
        public Node NodeX { get; }
        public Node NodeY { get; }
        public override string ToString()
        {
            return NodeX + "-->" + NodeY;
        }
    }

}