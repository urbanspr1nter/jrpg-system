namespace Jrpg.MenuSystem
{
    public class TilePoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public TilePoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
