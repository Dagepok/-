namespace ConsoleApp1
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int State { get; set; }
        public bool WasUsed { get; set; }

        public Point(int x, int y, int state)
        {
            X = x;
            Y = y;
            WasUsed = false;
            State = state;
        }

        public override string ToString()
        {
            return (Y + 1) + " " + (X + 1);
        }
    }
}